using Godot;
using System;

public partial class PauseEvent : Node
{

	PackedScene optionsScene;
	Node currentScene;

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ui_cancel") && GetChildCount() < 1) {
			AddChild(optionsScene.Instantiate());
		}
	}

	public override void _Ready() {
		optionsScene = GD.Load<PackedScene>("res://scene/menus/options.tscn");

		CheckPauseEvent(GetTree().CurrentScene.SceneFilePath);
		SceneManager.Get().SceneChanged += CheckPauseEvent;
	}

	private void CheckPauseEvent(string scenePath) {
		string[] pathTokens = scenePath.Split("/");
		string parentFolder = pathTokens[3];

		bool isLevel = parentFolder == "level";
		SetProcess(isLevel);
	}
}
