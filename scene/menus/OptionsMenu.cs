using Godot;
using System;

public partial class OptionsMenu : Control
{
	[ExportGroup("Back Button")]
	[Export] Button back;
	[Export(PropertyHint.File, ".tscn")] string mainMenuPath;

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ui_cancel")) { Back(); }
	}

	public override void _Ready() {
		back.ButtonUp += Back;
	}

	void Back() => SceneManager.Load(mainMenuPath);
}
