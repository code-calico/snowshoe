using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	[Export] PlayerMovementStats stats;

	private ShapeCast2D jumpBufferCast;

	public override void _Ready() {
		InitReferences();
		ResetCoyoteTime();
	}

	public override void _PhysicsProcess(double delta) {
		CheckInputAxes();

		// velocityMod is gone, it was used before because the 'Velocity' member setter only accepted Vector2 instances.
		
		// usage examples now for the current player class:
		// Velocity = new Vector2(overwritedValueX, Velocity.Y)
		// Velocity = new Vector2(Velocity.X, Velocity.Y + additiveValueX) | this could also be any operation or expression as long as it evaluates to a float

		if (HoldingLeftOrRight()) {
			AccelerateHorizontal();
		} else {
			DecelerateHorizontal();
		}
		
		if (IsAirborneState()) { 
			ApplyGravity(delta);
			SetTargetVariablesAir();

			if (CoyoteJumpAvailable()) {
				CoyoteTimeProcess(delta); 

				if (JumpRequested()) {
					Jump();
				}
			}

			if (JumpBufferRequested()) {
				SetJumpBuffer();
			}

			if (JumpCutRequested()) {
				JumpCut();
			}
		} 

		if (IsGroundedState()) {
			SetTargetVariablesGround();
			
			if (JustLanded()) {
				ResetCoyoteTime();
			}
			
			if (JumpRequested() || JumpBufferAvailable()) {
				Jump();
			}
		}

		if (Input.IsActionPressed("protag_pounce")) {
			Engine.TimeScale = 0.01;
		} else {
			Engine.TimeScale = 1.0;
		}

		ProcessPhysicsFrame();
	}

	void CheckInputAxes() {
		// grabs the input for the joystick / keyboard, returns a value to stats.inputAxisX and stats.inputAxisY

		stats.inputAxisH = Input.GetAxis("protag_move_left", "protag_move_right"); // (horizontal) stats.inputAxisX | -1 = left, 0 = no input, 1 = right
		stats.inputAxisV = Input.GetAxis("protag_move_down", "protag_move_up"); // (vertical) stats.inputAxisY | -1 = down, 0 = no input, 1 = up

		// joysticks currently allow smoothed input, meaning if the joystick is press halfway to the left, it would return -0.5
		
		// because of 2D screen space coordinates the current layout for inputAxisV may be undesirable
		// inputAxisV is currently unused | move up could be a camera pan, move down would be a crouch
	}

	void CoyoteTimeProcess(double delta)  {
		// this is currently being used in the physics process function, which means the timestep is fixed to the physics tick
		// delta = 16.66ms (60fps), this means that the window to input a jump is fixed to a specific amount of frames and not based on real time
	
		stats.coyoteTimeTick -= (float)delta;
		stats.coyoteTimeTick = Mathf.Max(0, stats.coyoteTimeTick);
	}

	void SetTargetVariablesGround() {
		stats.targetTopSpeed = stats.topGroundSpeed;
		stats.targetAccel = stats.dxGroundAccel;
		stats.targetDecel = stats.dxGroundDecel;
	}

	public bool IsAirborneState() {
		if (!IsOnFloor()) {
			stats.wasAirborneLastFrame = true;
		}  
		return !IsOnFloor();
	}
	
	void SetTargetVariablesAir() {
		stats.targetTopSpeed = stats.topAirSpeed;
		stats.targetAccel = stats.dxAirAccel;
		stats.targetDecel = stats.dxAirDecel;
	}

	void Jump() {
		Velocity = new Vector2(Velocity.X, stats.jumpVelocity);
		stats.jumpUsed = true;
		stats.jumpBuffered = false;

	}

	void AccelerateHorizontal() {
		float clampedSpeed = Math.Clamp(Velocity.X + stats.inputAxisH * stats.targetAccel, -stats.targetTopSpeed, stats.targetTopSpeed);
		Velocity = new Vector2(clampedSpeed, Velocity.Y);
	}
	
	public bool JustLanded() {
		bool state = stats.wasAirborneLastFrame && IsOnFloor();
		
		if (state) {
			Velocity = new Vector2(Velocity.X, 0);
			stats.jumpUsed = false;
			stats.wasAirborneLastFrame = false;
		}

		return state;
	}
	
	public bool IsGroundedState() {
		bool state = IsOnFloor();
		if (state) {
			Velocity = new Vector2(Velocity.X, 0);
		}
		return state;
	}  

	bool JumpBufferAvailable() => stats.jumpBuffered && IsOnFloor();
	void SetJumpBuffer() => stats.jumpBuffered = JumpBufferRequested();
	bool JumpBufferRequested() => jumpBufferCast.IsColliding() && Input.IsActionJustPressed("protag_jump") && !IsOnFloor();
	void JumpCut() => Velocity = new Vector2(Velocity.X, Velocity.Y * stats.jumpCutPercent);
	bool JumpRequested() => Input.IsActionJustPressed("protag_jump");
	bool JumpCutRequested() => Input.IsActionJustReleased("protag_jump") && Velocity.Y < 0 && !IsOnFloor();
	bool CoyoteJumpAvailable() => stats.coyoteTimeTick > 0 && !IsOnFloor() && !stats.jumpUsed;
	void ResetCoyoteTime() => stats.coyoteTimeTick = stats.coyoteTime;
	void DecelerateHorizontal() => Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, stats.targetDecel), Velocity.Y);
	void ApplyGravity(double delta) => Velocity = new Vector2(Velocity.X, Velocity.Y + stats.gravity * (float)delta);
	bool HoldingLeftOrRight() => stats.inputAxisH != 0;
	void ProcessPhysicsFrame() => MoveAndSlide();
	
	public PlayerMovementStats GetStats() => stats;
	
	public void InitReferences() {
		jumpBufferCast = GetNode<ShapeCast2D>("JumpBuffer");
	}

}
