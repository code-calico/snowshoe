using Godot;
using System;

public partial class PauseEvent : Node
{
	public override void _Ready() {
		CheckPauseEvent(GetTree().CurrentScene.SceneFilePath);
		SceneManager.Get().SceneChanged += CheckPauseEvent;
	}

	private void CheckPauseEvent(string scenePath) {
		string[] pathTokens = scenePath.Split("/");
		string parentFolder = pathTokens[3];

		if (parentFolder == "level") {
			GD.Print($"This scene is pausable. (scene folder variant: \"{parentFolder}\" == \"level\")");
		} else {
			GD.Print($"This scene is not pausable. (scene folder variant: \"{parentFolder}\" != \"level\")");
		}
	}
}
