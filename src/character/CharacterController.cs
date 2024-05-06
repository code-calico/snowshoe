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

	public float gravity = GetDefaultGravity();

	[ExportSubgroup("Unique")]
	[Export] public float coyoteTime = 0.75f;
	public float coyoteTimeTick;

	private bool jumpUsed = false;
	private bool jumpQueued = false;

	// inputAxisV currently unused
	public float inputAxisH, inputAxisV = 0; 
	public float targetTopSpeed, targetAccel, targetDecel;	

	private RayCast2D jumpBufferCast;

	// todo: 
	// -> handle input outside of physics process

	public override void _Ready() {
		jumpBufferCast = GetNode<RayCast2D>("JumpBuffer");
		jumpBufferCast.Enabled = true;

		coyoteTimeTick = coyoteTime;
	}

	public override void _PhysicsProcess(double delta) {

		inputAxisH = Input.GetAxis("protag_move_left", "protag_move_right");

		// Technically there are no "move up" or "move down" but for the sake of uniformity, I do name them like this.
		// Move up would probably just be like a "look up and pan the camera" thing.
		// Move down would be a crouch.
		inputAxisV = Input.GetAxis("protag_move_down", "protag_move_up");
		
		
		Vector2 velocityMod = Velocity;

		if (IsAirborne()) { 
			velocityMod.Y += gravity * (float)delta; 
		} else {
			jumpUsed = false;
			coyoteTimeTick = coyoteTime;
		}
		
		if (coyoteTimeTick > 0 && !IsOnFloor() && !jumpUsed) {
			coyoteTimeTick -= (float)delta;
			coyoteTimeTick = Mathf.Max(0, coyoteTimeTick);
			if (Input.IsActionJustPressed("protag_jump")) {
				coyoteTimeTick = 0;
				velocityMod.Y = jumpVelocity;
				jumpUsed = true;
			}
		}
	
		JumpQueueCheck(); 
		if (jumpQueued && IsOnFloor()) {
			jumpQueued = false;
			velocityMod.Y = jumpVelocity;
			jumpUsed = true;
		}

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

	void SetStateVariables() {
		targetTopSpeed = IsOnFloor() ? topGroundSpeed : topAirSpeed;
		targetAccel = IsOnFloor() ? dxGroundAccel : dxAirAccel;
		targetDecel = IsOnFloor() ? dxGroundDecel : dxAirDecel;
	}	

	void JumpQueueCheck() {
		if (jumpBufferCast.IsColliding() && Input.IsActionJustPressed("protag_jump")) {
			jumpQueued = true;
		} 
	}

	public bool IsAirborne() => !IsOnFloor();
	bool HorizontalInputActive() => inputAxisH != 0;
	static float GetDefaultGravity() => ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

}
