using Godot;
using System;

public partial class PauseEvent : Node
{

	PackedScene pauseScene;
	Node currentScene;

	public override void _Process(double delta) {
		
		if (Input.IsActionJustPressed("pause_game")) {
			
			// if there is no menu open, else 
			if (GetChildCount() < 1) {
				AddChild(pauseScene.Instantiate());
				GetTree().Paused = true;
			} else {
				GD.Print("more than zero");
				Node pauseSceneInstance = GetChild(0);
				pauseSceneInstance.QueueFree();
			}
			GD.Print(GetChildCount());
		}
	}

	public override void _Ready() {
		// this loads a packed scene resource, it is not an instantiated scene
		ProcessMode = Node.ProcessModeEnum.Always;
		pauseScene = GD.Load<PackedScene>("res://scene/menus/pause_menu.tscn");

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
