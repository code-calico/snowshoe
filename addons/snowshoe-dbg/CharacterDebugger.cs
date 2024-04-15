using Godot;
using System;
using ImGuiNET;

public partial class CharacterDebugger : Node
{
	ulong instanceID = 0;
	CharacterController parent;
	[Export] private bool debugOn = false;
	[Export] private float opacity = 0.75f;

	private System.Numerics.Vector2 cheatCachePos = System.Numerics.Vector2.Zero;
	private Vector2 posOnLoad;
	private bool previewingPosition;

	public override void _Ready() {
		parent = GetParent<CharacterController>();
		posOnLoad = parent.Position;
	}

	public override void _Process(double delta) { 
		instanceID = parent.GetInstanceId();
		if (debugOn) { DebugGUI(); } 
	}

	void DebugGUI() {
		ImGui.Begin("Debug iID: " + instanceID);
		
		General();
		State();
		Adjustments();
		
		if(ImGui.CollapsingHeader("Others")) {
			ImGuiUtil.TopPad();
			Cheats();
			WindowSettings();
			ImGuiUtil.BotPad();
		}

		ImGui.End(); 
	}

	void General() {
		if (ImGui.CollapsingHeader("General")) {			
			ImGuiUtil.TopPad();
			
			string[] rocStrings = {
				"Ground Acceleration: " + ImGuiUtil.FormatFloat(parent.dxGroundAccel),
				"Ground Deceleration: " + ImGuiUtil.FormatFloat(parent.dxGroundDecel),
				"Air Acceleration: " + ImGuiUtil.FormatFloat(parent.dxAirAccel),
				"Air Deceleration: " + ImGuiUtil.FormatFloat(parent.dxGroundAccel)
			};
			ImGuiUtil.FoldableStringList("Rate of Change", rocStrings);
			
			string[] generalVarStrings = {
				$"Velocity: ({ImGuiUtil.FormatFloat(parent.Velocity.X)},{ImGuiUtil.FormatFloat(parent.Velocity.Y)})",
				"Top Air Speed: " + parent.topAirSpeed,
				"Top Ground Speed: " + parent.topGroundSpeed,
				"Jump Velocity: " + parent.jumpVelocity,
				"Gravity: " + parent.gravity,
				"Coyote Time: " + ImGuiUtil.FormatFloat(parent.coyoteTimeTick, 2) + "/" + ImGuiUtil.FormatFloat(parent.coyoteTime, 2)
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
				"Target Acceleration: " + ImGuiUtil.FormatFloat(parent.targetAccel),
				"Target Top Speed: " + ImGuiUtil.FormatFloat(parent.targetTopSpeed),
				"Target Deceleration: " + ImGuiUtil.FormatFloat(parent.targetDecel)
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
				ImGuiUtil.ModifyFloat("Top Speed", ref parent.topGroundSpeed);
				ImGuiUtil.ModifyFloat("Acceleration", ref parent.dxGroundAccel, 0.1f, 0.5f);
				ImGuiUtil.ModifyFloat("Deceleration", ref parent.dxGroundDecel, 0.1f, 0.5f);
				ImGuiUtil.BotPad();
			}

			if(ImGui.CollapsingHeader("Air")) {
				ImGuiUtil.TopPad();
				ImGuiUtil.ModifyFloat("Top Speed", ref parent.topAirSpeed);
				ImGuiUtil.ModifyFloat("Acceleration", ref parent.dxAirAccel, 0.1f, 0.5f);
				ImGuiUtil.ModifyFloat("Deceleration", ref parent.dxAirDecel, 0.1f, 0.5f);
				ImGuiUtil.BotPad();
			}
	
			ImGuiUtil.BotPad();
		}
	}

	void Cheats() {
		if (ImGui.CollapsingHeader("Cheats")) {
			ImGuiUtil.TopPad();
			
			ImGui.BeginGroup();
			ImGui.Text("Position: ");
			ImGui.SameLine();
			
			if (ImGui.SmallButton("X")) { 
				previewingPosition = false;
				cheatCachePos = System.Numerics.Vector2.Zero; 
			}
			
			ImGui.SameLine();
			ImGui.SetNextItemWidth(200);
			
			if (ImGui.InputFloat2("", ref cheatCachePos, "%.1f")) { previewingPosition = true; } 
			if (previewingPosition) { 
				parent.Velocity = Vector2.Zero;
				parent.Position = new Vector2(cheatCachePos.X, cheatCachePos.Y); 
			}
			ImGui.SameLine();
			
			if (ImGui.Button("Confirm")) { 
				previewingPosition = false; 
				parent.Position = new Vector2(cheatCachePos.X, cheatCachePos.Y);
			}

			ImGui.EndGroup();

			if (ImGui.Button("Go To Spawn")) { parent.Position = posOnLoad; }

			ImGuiUtil.BotPad();
		}
	}

	void WindowSettings() {
		if (ImGui.CollapsingHeader("Debug Window")) {
			ImGuiUtil.TopPad();
			ImGui.BeginGroup();
			ImGui.Text("Transparency: ");
			ImGui.SameLine();
			ImGui.SetNextItemWidth(75);
			ImGui.SliderFloat("", ref opacity, 0.0f, 1.0f);
			System.Numerics.Vector4 backgroundColor = new System.Numerics.Vector4(0.0f, 0.0f, 0.0f, opacity); 
			ImGui.PushStyleColor(ImGuiCol.WindowBg, backgroundColor);
			ImGui.EndGroup();
			ImGuiUtil.BotPad();
		}
	}
}
