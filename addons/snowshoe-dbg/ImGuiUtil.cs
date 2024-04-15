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
		ImGui.SetNextItemWidth(200);
		if (ImGui.InputFloat($"##{name}", ref input, 10)) {
			value = input;
		}
		ImGui.EndGroup();
	}
} 