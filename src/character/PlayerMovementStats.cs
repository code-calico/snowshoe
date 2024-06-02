using System.ComponentModel;
using Godot;

public partial class PlayerMovementStats : Resource
{
    [ExportCategory("Player Movement Stats")]

	[ExportGroup("Basic Movement")]
	[ExportSubgroup("Ground")]
	[Export] public float topGroundSpeed = 300.0f; 
	[Description("Sets the horizontal ground acceleration.")] 
	[Export] public float dxGroundAccel = 10.0f;
	[Description("Sets the horizontal ground deceleration.")] 
	[Export] public float dxGroundDecel = 5.0f;

	[ExportSubgroup("Air")]
	[Export] public float topAirSpeed = 400.0f;
	
	[Description("Sets the horizontal air acceleration.")] 
	[Export] public float dxAirAccel = 20.0f;
	[Description("Sets the horizontal air deceleration.")]
	[Export] public float dxAirDecel = 1.0f;
	[Export] public float jumpVelocity = -400.0f;
	[Export(PropertyHint.Range, "0.0, 1.0")] public float jumpCutPercent = 0.5f;
    [Export] public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    [ExportSubgroup("Unique")]
	[Export] public float coyoteTime = 0.05f;
	
	// [Export] public float jumpBufferThreshold;
    
	public float coyoteTimeTick;

    public float inputAxisH, inputAxisV = 0; 
    public float targetTopSpeed, targetAccel, targetDecel;

    public bool jumpUsed = false;
	public bool jumpBuffered = false;

	public bool wasAirborneLastFrame = false;

	//-1 means facing left, 1 means facing right (not implemented)
	// public float directionFacing = 1;
    
}
