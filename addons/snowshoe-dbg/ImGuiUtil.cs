using ImGuiNET;
using System;
using System.Collections;
using System.Collections.Generic;

public static class ImGuiUtil {

    public static void StringList(string[] strings) {
		ImGui.Spacing();
		foreach (string str in strings) { ImGui.Text(str); }
		ImGui.Spacing();
	}

	public static void FoldableStringList(string title, string[] strings) {
		if(ImGui.CollapsingHeader(title)) {
			ImGui.Indent(20);
			ImGui.Spacing();
			foreach (string str in strings) { ImGui.Text(str); }
			ImGui.Spacing();
			ImGui.Separator();
			ImGui.Spacing();
			ImGui.Unindent(20);
		}
	}

    public static void TopPad() {
        ImGui.Indent(20);
		ImGui.Spacing();
    }

    public static void BotPad() {
        ImGui.Spacing();
        ImGui.Unindent(20);
    }

    public static void BotPadSeparator() {
        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();
        ImGui.Unindent(20);
    }

	public static void ModifyFloat(string name, ref float value) {
		ImGui.BeginGroup();
		float input = value;
		ImGui.Text(name + ": ");
		ImGui.SameLine();
		ImGui.SetNextItemWidth(100);
		if (ImGui.InputFloat($"##{name}", ref input, 10, 50, "%.1f")) {
			value = input;
		}
		ImGui.EndGroup();
	}

    public static void ModifyFloat(string name, ref float value, float slowStep, float fastStep) {
		ImGui.BeginGroup();
		float input = value;
		ImGui.Text(name + ": ");
		ImGui.SameLine();
		ImGui.SetNextItemWidth(100);
		if (ImGui.InputFloat($"##{name}", ref input, slowStep, fastStep, "%.1f")) {
			value = input;
		}
		ImGui.EndGroup();
	}

    public static string FormatFloat(float f) => string.Format("{0:F1}", f);
    public static string FormatFloat(float f, int figures) => string.Format("{0:F" + figures.ToString() + "}", f);
} 