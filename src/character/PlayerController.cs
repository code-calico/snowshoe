using Godot;
using ImGuiNET;
using System;
using System.Dynamic;

public partial class PlayerController : CharacterBody2D
{
	[Export] PlayerMovementStats stats;

	private RayCast2D jumpBufferCast;

	// todo: 
	// -> handle input outside of physics process

	public override void _Ready() {
		jumpBufferCast = GetNode<RayCast2D>("JumpBuffer");
		jumpBufferCast.Enabled = true;

		stats.coyoteTimeTick = stats.coyoteTime;
	}

	public override void _PhysicsProcess(double delta) {

		stats.inputAxisH = Input.GetAxis("protag_move_left", "protag_move_right");

		// Technically there are no "move up" or "move down" but for the sake of uniformity, I do name them like this.
		// Move up would probably just be like a "look up and pan the camera" thing.
		// Move down would be a crouch.
		stats.inputAxisV = Input.GetAxis("protag_move_down", "protag_move_up");
		
		
		Vector2 velocityMod = Velocity;

		if (IsAirborne()) { 
			velocityMod.Y += stats.gravity * (float)delta; 
		} else {
			stats.jumpUsed = false;
			stats.coyoteTimeTick = stats.coyoteTime;
		}

		if (IsAirborne() && stats.jumpUsed == true && velocityMod.Y <= 0) {
			if (Input.IsActionJustReleased("protag_jump")) {
				velocityMod.Y *= stats.jumpCutPercent;
			}
		}
		
		if (stats.coyoteTimeTick > 0 && !IsOnFloor() && !stats.jumpUsed) {
			stats.coyoteTimeTick -= (float)delta;
			stats.coyoteTimeTick = Mathf.Max(0, stats.coyoteTimeTick);
			if (Input.IsActionJustPressed("protag_jump")) {
				stats.coyoteTimeTick = 0;
				velocityMod.Y = stats.jumpVelocity;
				stats.jumpUsed = true;
			}
		}

	
		JumpQueueCheck(); 
		if (stats.jumpQueued && IsOnFloor()) {
			stats.jumpQueued = false;
			velocityMod.Y = stats.jumpVelocity;
			stats.jumpUsed = true;
			
		}

		SetStateVariables();

		if (HorizontalInputActive()) {
			velocityMod.X += stats.inputAxisH * stats.dxGroundAccel;
			stats.directionFacing = stats.inputAxisH;
		} else {
			velocityMod.X = Mathf.MoveToward(Velocity.X, 0, stats.targetDecel);
		}

		velocityMod.X = Mathf.Clamp(velocityMod.X, -stats.targetTopSpeed, stats.targetTopSpeed);

		if (Input.IsActionJustPressed("protag_pounce")) {
			if (stats.directionFacing == -1) {
				velocityMod.X += -1800f;
			}
			else {
				velocityMod.X += 1800f;
			}
		}

		Velocity = velocityMod;
		

		MoveAndSlide();
	}

	void SetStateVariables() {
		stats.targetTopSpeed = IsOnFloor() ? stats.topGroundSpeed : stats.topAirSpeed;
		stats.targetAccel = IsOnFloor() ? stats.dxGroundAccel : stats.dxAirAccel;
		stats.targetDecel = IsOnFloor() ? stats.dxGroundDecel : stats.dxAirDecel;
	}	

	void JumpQueueCheck() {
		if (jumpBufferCast.IsColliding() && Input.IsActionJustPressed("protag_jump")) {
			stats.jumpQueued = true;
		} 
	}

	public bool IsAirborne() => !IsOnFloor();
	bool HorizontalInputActive() => stats.inputAxisH != 0;
	static float GetDefaultGravity() => ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public PlayerMovementStats GetStats() => stats;
	

}
