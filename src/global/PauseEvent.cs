using Godot;
using System;

public partial class PauseEvent : Node
{

	PackedScene optionsScene;
	Node currentScene;

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			// check if the user has a menu open
			if (GetChildCount() < 1)
			{
				AddChild(optionsScene.Instantiate());
				GetTree().Paused = true;
			}
		}
		//GetTree().Paused = !GetTree().Paused;
		GD.Print(GetTree().Paused);
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
