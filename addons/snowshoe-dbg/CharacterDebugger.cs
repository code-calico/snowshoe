using Godot;
using System;
using ImGuiNET;

public partial class CharacterDebugger : Node
{
	ulong instanceID = 0;
	CharacterController parent;
	[Export] private bool debugOn = false;

	public override void _Ready() => parent = GetParent<CharacterController>();

	public override void _Process(double delta) { 
		instanceID = parent.GetInstanceId();
		if (debugOn) { DebugGUI(); } 
	}

	void DebugGUI() {
		ImGui.Begin("Debug iID: " + instanceID);
		General();
		State();
		Adjustments();
		ImGui.End(); 
	}

	void General() {
		if (ImGui.CollapsingHeader("General")) {			
			ImGuiUtil.TopPad();
			
			string[] rocStrings = {
				"Ground Acceleration: " + parent.dxGroundAccel,
				"Ground Deceleration: " + parent.dxGroundDecel,
				"Air Acceleration: " + parent.dxAirAccel,
				"Air Deceleration: " + parent.dxGroundAccel
			};
			ImGuiUtil.FoldableStringList("Rate of Change", rocStrings);
			
			string[] generalVarStrings = {
				$"Velocity: ({parent.Velocity.X},{parent.Velocity.Y})",
				"Top Air Speed: " + parent.topAirSpeed,
				"Top Ground Speed: " + parent.topGroundSpeed,
				"Jump Velocity: " + parent.jumpVelocity,
				"Gravity: " + parent.gravity,
				"Coyote Time: " + string.Format("{0:F2}", parent.coyoteTimeTick) + "/" + string.Format("{0:F2}", parent.coyoteTime)
			};
			ImGuiUtil.StringList(generalVarStrings);
			
			ImGuiUtil.BotPad();
		}		
	}

	void State() {
		if (ImGui.CollapsingHeader("State")) {
			ImGuiUtil.TopPad();

			string[] inputStrings = {
				"Horizontal Input: " + parent.inputAxisH,
				"Vertical Input: " + parent.inputAxisV,
				"Space: " + Input.IsActionJustPressed("protag_jump")
			};
			ImGuiUtil.FoldableStringList("Input", inputStrings);

			string[] generalVarStrings = {
				"Grounded: " + parent.IsOnFloor(),
				"Airborne: " + parent.IsAirborne(),
				"Target Acceleration: " + parent.targetAccel,
				"Target Top Speed: " + parent.targetTopSpeed,
				"Target Deceleration: " + parent.targetDecel
			};
			ImGuiUtil.StringList(generalVarStrings);

			ImGuiUtil.BotPad();
		}
	}


	void Adjustments() {
		if (ImGui.CollapsingHeader("Adjustments")) {
			ImGuiUtil.TopPad();

			if(ImGui.CollapsingHeader("Vertical")) {
				ImGuiUtil.TopPad();
				ImGuiUtil.ModifyFloat("Jump Velocity", ref parent.jumpVelocity);
				ImGuiUtil.ModifyFloat("Gravity", ref parent.gravity);
				ImGuiUtil.BotPad();
			}

			if(ImGui.CollapsingHeader("Ground")) {
				ImGuiUtil.TopPad();
				ImGuiUtil.ModifyFloat("Top Ground Speed", ref parent.topGroundSpeed);
				ImGuiUtil.ModifyFloat("Ground Acceleration", ref parent.dxGroundAccel);
				ImGuiUtil.ModifyFloat("Ground Deceleration", ref parent.dxGroundDecel);
				ImGuiUtil.BotPad();
			}

			if(ImGui.CollapsingHeader("Air")) {
				ImGuiUtil.TopPad();
				ImGuiUtil.ModifyFloat("Top Air Speed", ref parent.topAirSpeed);
				ImGuiUtil.ModifyFloat("Air Acceleration", ref parent.dxAirAccel);
				ImGuiUtil.ModifyFloat("Air Deceleration", ref parent.dxAirDecel);
				ImGuiUtil.BotPad();
			}
	
			ImGuiUtil.BotPad();
		}
	}
}
