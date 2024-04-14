using Godot;
using ImGuiNET;
using System;


public partial class CharacterController : CharacterBody2D
{
	[ExportCategory("Controller")]
	[ExportGroup("Basic Movement")]
	
	[ExportSubgroup("Ground")]
	[Export] public float topGroundSpeed = 300.0f; 
	[Export] public float dxGroundAccel = 10.0f;
	[Export] public float dxGroundDecel = 5.0f;

	[ExportSubgroup("Air")]
	[Export] public float topAirSpeed = 400.0f;
	[Export] public float dxAirAccel = 20.0f;
	[Export] public float dxAirDecel = 1.0f;
	[Export] public float jumpVelocity = -400.0f;

	private float gravity = GetDefaultGravity();

	[ExportSubgroup("Debug")]
	[Export] private bool debugOn = false;
	private ulong instanceID = 0;


	// inputAxisV currently unused
	float inputAxisH, inputAxisV = 0; 
	private float targetTopSpeed, targetAccel, targetDecel;

	// todo: 
	// -> handle input outside of physics process

	public override void _Ready() {
		instanceID = GetInstanceId();
	}

	public override void _Process(double delta) {
		if (debugOn) { DebugGUI(); }
	}

	public override void _PhysicsProcess(double delta) {

		inputAxisH = Input.GetAxis("protag_move_left", "protag_move_right");

		// Technically there are no "move up" or "move down" but for the sake of uniformity, I do name them like this.
		// Move up would probably just be like a "look up and pan the camera" thing.
		// Move down would be a crouch.
		inputAxisV = Input.GetAxis("protag_move_up", "protag_move_right");
		
		
		Vector2 velocityMod = Velocity;

		if (IsAirborne()) { velocityMod.Y += gravity * (float)delta; }
		if (AbleToJump()) { velocityMod.Y = jumpVelocity; }

		SetStateVariables();

		if (HorizontalInputActive()) {
			velocityMod.X += inputAxisH * dxGroundAccel;
		} else {
			velocityMod.X = Mathf.MoveToward(Velocity.X, 0, targetDecel);
		}

		velocityMod.X = Mathf.Clamp(velocityMod.X, -targetTopSpeed, targetTopSpeed);
		Velocity = velocityMod;
		
		MoveAndSlide();
	}

	void DebugGUI() {
		ImGui.Begin("Debug iID: " + instanceID);
		
		if (ImGui.CollapsingHeader("General")) {			
			ImGui.Spacing();
			ImGui.Indent(20);
			
			if(ImGui.CollapsingHeader("Rate of Change")) {
				ImGui.Indent(20);
				ImGui.Text("Ground Acceleration: " + dxGroundAccel);
				ImGui.Text("Ground Deceleration: " + dxGroundDecel);
				ImGui.Text("Air Acceleration: " + dxAirAccel);
				ImGui.Text("Air Deceleration: " + dxGroundAccel);
				ImGui.Unindent(20);
			}
			
			ImGui.Spacing();
			ImGui.Text($"Velocity: ({Velocity.X},{Velocity.Y})");
			ImGui.Text("Top Air Speed: " + topAirSpeed);
			ImGui.Text("Top Ground Speed: " + topGroundSpeed);
			ImGui.Text("Jump Velocity: " + jumpVelocity);
			ImGui.Text("Gravity: " + gravity);
			ImGui.Spacing();
			ImGui.Unindent(20);
		}		

		if (ImGui.CollapsingHeader("State")) {
			ImGui.Indent(20);
			ImGui.Text("Grounded: " + IsOnFloor());
			ImGui.Text("Airborne: " + IsAirborne());
			ImGui.Text("Target Acceleration: " + targetAccel);
			ImGui.Text("Target Top Speed: " + targetTopSpeed);
			ImGui.Text("Target Deceleration: " + targetDecel);
			ImGui.Spacing();
			ImGui.Unindent(20);
		}

		if (ImGui.CollapsingHeader("Adjustments")) {
			ImGui.Indent(20);
			ImGui.Spacing();
			if (ImGui.CollapsingHeader("Vertical")) {
				ImGui.Indent(20);
				ImGui.Spacing();
				ImGUI_ModifyFloat("Jump Velocity", ref jumpVelocity);
				ImGUI_ModifyFloat("Gravity", ref gravity);
				ImGui.Unindent(20);
			}
			ImGui.Spacing();
			if (ImGui.CollapsingHeader("Ground")) {
				ImGui.Indent(20);
				ImGui.Spacing();
				ImGUI_ModifyFloat("Top Ground Speed", ref topGroundSpeed);
				ImGUI_ModifyFloat("Ground Acceleration", ref dxGroundAccel);
				ImGUI_ModifyFloat("Ground Deceleration", ref dxGroundDecel);
				ImGui.Unindent(20);
			}
			ImGui.Spacing();
			if (ImGui.CollapsingHeader("Air")) {
				ImGui.Indent(20);
				ImGui.Spacing();
				ImGUI_ModifyFloat("Air Acceleration", ref dxAirAccel);
				ImGUI_ModifyFloat("Air Deceleration", ref dxGroundDecel);
				ImGUI_ModifyFloat("Top Air Speed", ref topAirSpeed);
				ImGui.Unindent(20);
			}
			ImGui.Spacing();
			ImGui.Unindent(20);
		}
		ImGui.End(); 
	}

	void ImGUI_ModifyFloat(string name, ref float value) {
		ImGui.BeginGroup();
		float input = value;
		ImGui.Text(name + ": ");
		ImGui.SameLine();
		ImGui.SetNextItemWidth(200);
		if (ImGui.InputFloat($"##{name}", ref input, 10)) {
			value = input;
		}
		ImGui.EndGroup();
	}

	void SetStateVariables() {
		targetTopSpeed = IsOnFloor() ? topGroundSpeed : topAirSpeed;
		targetAccel = IsOnFloor() ? dxGroundAccel : dxAirAccel;
		targetDecel = IsOnFloor() ? dxGroundDecel : dxAirDecel;
	}	

	bool IsAirborne() => !IsOnFloor();
	bool AbleToJump() => Input.IsActionJustPressed("protag_jump") && IsOnFloor(); 
	bool HorizontalInputActive() => inputAxisH != 0;
	static float GetDefaultGravity() => ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

}
