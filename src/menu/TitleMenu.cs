using Godot;
using System;

public partial class TitleMenu : Control
{
	[ExportGroup("Play Button")]
	[Export] Button play; 
	[Export(PropertyHint.File, "*.tscn")] string playScenePath;

	[ExportGroup("Options Button")]
	[Export] Button options;
	[Export(PropertyHint.File, ".tscn")] string optionsScenePath;
	PackedScene optionsScene;

	[ExportGroup("Quit Button")]
	[Export] Button quit;

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ui_cancel") && !HasNode("OptionsMenu")) {
			Quit();
		}
	}

	public override void _Ready() {
		optionsScene = GD.Load<PackedScene>(optionsScenePath);
	
		if (play != null && playScenePath != null) { play.ButtonUp += Play; } 
		if (options != null && optionsScenePath != null) { options.ButtonUp += Options; }
		if (quit != null) { quit.ButtonUp += Quit; } 
	}

	private void Play() => SceneManager.Load(playScenePath); 
	private void Options() { AddChild(optionsScene.Instantiate()); }
	private void Quit() => GetTree().Quit();

}
