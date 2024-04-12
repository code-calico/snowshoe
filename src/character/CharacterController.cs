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
		inputAxisH = Input.GetAxis("ui_left", "ui_right"); 

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
	bool AbleToJump() => Input.IsActionJustPressed("ui_accept") && IsOnFloor(); 
	bool CurrentlyMoving() => inputAxisH != 0;
	static float GetDefaultGravity() => ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

}
