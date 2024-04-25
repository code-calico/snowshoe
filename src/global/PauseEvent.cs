using Godot;
using System;

public partial class PauseEvent : Node
{

	PackedScene optionsScene;
	Node currentScene;

	public override void _Process(double delta) {
		// the user pressed a ui cancel event and it doesn't already have a menu instantiated
		if (Input.IsActionJustPressed("ui_cancel") && GetChildCount() < 1)
		{
			// this finally instantiates the scene and adds it to the scene tree
			AddChild(optionsScene.Instantiate());
			GetTree().Paused = !GetTree().Paused;
		}
	}

	public override void _Ready() {
		// this loads a packed scene resource, it is not an instantiated scene
		optionsScene = GD.Load<PackedScene>("res://scene/menus/options.tscn");

		CheckPauseEvent(GetTree().CurrentScene.SceneFilePath);
		SceneManager.Get().SceneChanged += CheckPauseEvent;
	}

	private void CheckPauseEvent(string scenePath) {
		string[] pathTokens = scenePath.Split("/");
		string parentFolder = pathTokens[3];

		bool isLevel = parentFolder == "level";
		
		// sets the _Process function to be enabled or disabled based on the scene type
		SetProcess(isLevel);
	}
}
