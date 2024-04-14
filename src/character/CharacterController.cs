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

	[ExportSubgroup("Debug")]
	[Export] private bool debugOn = false;

	private float gravity = GetDefaultGravity();

	// inputAxisV currently unused
	float inputAxisH, inputAxisV = 0; 
	private float targetTopSpeed, targetAccel, targetDecel;
  
	// todo: 
	// -> handle input outside of physics process

	public override void _PhysicsProcess(double delta)
	{
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

		if (debugOn) { GD.Print("Current Velocity: " + Velocity); }
		
		MoveAndSlide();
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
