using Godot;
using System;
using ImGuiNET;

public partial class CharacterDebugger : Node
{
	int instanceID = 0;
	CharacterController parent;
	[Export] private bool debugOn = false;

	public override void _Ready() => parent = GetParent<CharacterController>();

	public override void _Process(double delta) { if (debugOn) { DebugGUI(); } }

	void DebugGUI() {
		ImGui.Begin("Debug iID: " + instanceID);
		
		if (ImGui.CollapsingHeader("General")) {			
			ImGui.Spacing();
			ImGui.Indent(20);
			
			if(ImGui.CollapsingHeader("Rate of Change")) {
				ImGui.Indent(20);
				ImGui.Spacing();
				ImGui.Text("Ground Acceleration: " + parent.dxGroundAccel);
				ImGui.Text("Ground Deceleration: " + parent.dxGroundDecel);
				ImGui.Text("Air Acceleration: " + parent.dxAirAccel);
				ImGui.Text("Air Deceleration: " + parent.dxGroundAccel);
				ImGui.Spacing();
				ImGui.Separator();
				ImGui.Spacing();
				ImGui.Unindent(20);
			}
			
			ImGui.Spacing();
			ImGui.Text($"Velocity: ({parent.Velocity.X},{parent.Velocity.Y})");
			ImGui.Text("Top Air Speed: " + parent.topAirSpeed);
			ImGui.Text("Top Ground Speed: " + parent.topGroundSpeed);
			ImGui.Text("Jump Velocity: " + parent.jumpVelocity);
			ImGui.Text("Gravity: " + parent.gravity);
			ImGui.Text("Coyote Time: " + string.Format("{0:F2}", parent.coyoteTimeTick) + "/" + string.Format("{0:F2}", parent.coyoteTime));
			ImGui.Spacing();
			ImGui.Unindent(20);
		}		

		if (ImGui.CollapsingHeader("State")) {
			ImGui.Indent(20);
			ImGui.Spacing();

			if(ImGui.CollapsingHeader("Input")) {
				ImGui.Indent(20);
				ImGui.Spacing();
				ImGui.Text("Horizontal Input: " + parent.inputAxisH);
				ImGui.Text("Vertical Input: " + parent.inputAxisV);
				ImGui.Text("Space: " + Input.IsActionJustPressed("protag_jump"));
				ImGui.Spacing();
				ImGui.Separator();
				ImGui.Spacing();
				ImGui.Unindent(20);
			}

			ImGui.Text("Grounded: " + parent.IsOnFloor());
			ImGui.Text("Airborne: " + parent.IsAirborne());
			ImGui.Text("Target Acceleration: " + parent.targetAccel);
			ImGui.Text("Target Top Speed: " + parent.targetTopSpeed);
			ImGui.Text("Target Deceleration: " + parent.targetDecel);
			ImGui.Spacing();
			ImGui.Unindent(20);
		}

		if (ImGui.CollapsingHeader("Adjustments")) {
			ImGui.Indent(20);
			ImGui.Spacing();
			if (ImGui.CollapsingHeader("Vertical")) {
				ImGui.Indent(20);
				ImGui.Spacing();
				ImGUI_ModifyFloat("Jump Velocity", ref parent.jumpVelocity);
				ImGUI_ModifyFloat("Gravity", ref parent.gravity);
				ImGui.Unindent(20);
			}
			ImGui.Spacing();
			if (ImGui.CollapsingHeader("Ground")) {
				ImGui.Indent(20);
				ImGui.Spacing();
				ImGUI_ModifyFloat("Top Ground Speed", ref parent.topGroundSpeed);
				ImGUI_ModifyFloat("Ground Acceleration", ref parent.dxGroundAccel);
				ImGUI_ModifyFloat("Ground Deceleration", ref parent.dxGroundDecel);
				ImGui.Unindent(20);
			}
			ImGui.Spacing();
			if (ImGui.CollapsingHeader("Air")) {
				ImGui.Indent(20);
				ImGui.Spacing();
				ImGUI_ModifyFloat("Top Air Speed", ref parent.topAirSpeed);
				ImGUI_ModifyFloat("Air Acceleration", ref parent.dxAirAccel);
				ImGUI_ModifyFloat("Air Deceleration", ref parent.dxGroundDecel);
				ImGui.Unindent(20);
			}
			ImGui.Spacing();
			ImGui.Unindent(20);
		}
		ImGui.End(); 
	}

	void ImGUI_ModifyFloat(string name, ref float value) {
		ImGui.BeginGroup();
		float input = value;
		ImGui.Text(name + ": ");
		ImGui.SameLine();
		ImGui.SetNextItemWidth(200);
		if (ImGui.InputFloat($"##{name}", ref input, 10)) {
			value = input;
		}
		ImGui.EndGroup();
	}

}
