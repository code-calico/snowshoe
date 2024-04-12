using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
	[ExportCategory("Controller")]
	[ExportGroup("Basic Movement")]
	
	[Export] public float topGroundSpeed = 300.0f; 
	[Export] public float dxGroundAccel = 10.0f;
	[Export] public float dxGroundDecel = 5.0f;

	[Export] public float topAirSpeed = 300.0f;
	[Export] public float dxAirAccel = 10.0f;
	[Export] public float dxAirDecel = 5.0f;

	[Export] public float jumpVelocity = -400.0f;

	private float gravity = GetDefaultGravity();

	// inputAxisV currently unused
	float inputAxisH, inputAxisV = 0; 


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

		if (IsOnFloor() && CurrentlyMoving()) {
			velocityMod.X += inputAxisH * dxGroundAccel;
			Velocity = new Vector2(Mathf.Clamp(Velocity.X, -topGroundSpeed, topGroundSpeed), Velocity.Y);
			GD.Print(Velocity);
		} else {
			// velocityMod.X = Mathf.MoveToward(Velocity.X, 0, dxGroundDecel);
		}

		if (IsAirborne() && CurrentlyMoving()) {
			velocityMod.X += inputAxisH * dxAirAccel;
			Velocity = new Vector2(Mathf.Clamp(Velocity.X, -topAirSpeed, topAirSpeed), Velocity.Y);
			GD.Print(Velocity);
		} else {
			// velocityMod.X = Mathf.MoveToward(Velocity.X, 0, dxAirDecel);
		}


		Velocity = velocityMod;

		MoveAndSlide();
	}

	bool IsAirborne() => !IsOnFloor();
	bool AbleToJump() => Input.IsActionJustPressed("protag_jump") && IsOnFloor(); 
	bool CurrentlyMoving() => inputAxisH != 0;
	static float GetDefaultGravity() => ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

}
