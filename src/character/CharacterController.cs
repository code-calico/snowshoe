using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
	[ExportCategory("Controller")]
	[ExportGroup("Basic Movement")]
	[Export] public float groundedSpeed = 300.0f;
	[Export] public float jumpVelocity = -400.0f;

	private float gravity = GetDefaultGravity();

	// inputAxisV currently unused
	float inputAxisH, inputAxisV = 0; 


	// todo: 
	// -> switch from ui binds to input maps
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

		if (CurrentlyMoving()) {
			velocityMod.X = inputAxisH * groundedSpeed;
		} else {
			velocityMod.X = Mathf.MoveToward(Velocity.X, 0, groundedSpeed);
		}

		Velocity = velocityMod;

		MoveAndSlide();
	}

	bool IsAirborne() => !IsOnFloor();
	bool AbleToJump() => Input.IsActionJustPressed("protag_jump") && IsOnFloor(); 
	bool CurrentlyMoving() => inputAxisH != 0;
	static float GetDefaultGravity() => ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

}
