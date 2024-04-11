using Godot;
using System;
using System.Diagnostics;
using System.IO;

public partial class SceneButton : Button
{
	// these exports are meant to be adjusted in the inspector please don't touch here! 
	[Export] public string folderName = "";
	[Export] public string sceneName = "";
	[Export] public bool debug = false;

	public override void _Ready() => this.ButtonUp += OnButtonUp;

	// "button_up" signal subscription
	private void OnButtonUp() {

		string workingDir = ProjectSettings.GlobalizePath($"res://scene/{folderName}");
		string finalPath = $"{workingDir}/{sceneName}.tscn";

		if (Directory.Exists(workingDir)) {
			SearchForScene(workingDir, finalPath);
		} else {
			GD.PrintErr($"Directory does not exist: '{workingDir}' ...");
		}
	}

	private void SearchForScene(string searchDir, string filePath) {
		string[] paths = Directory.GetFiles(searchDir);
		
		bool success = false;
		
		foreach(string path in paths) {
			DebugMessage($"Matching built path against '{path}' ...");
			
			if (File.Exists(filePath)) {
				DebugMessage($"Found '{filePath}'! ...");
				LoadScene(filePath);
				success = true;
				break;
			} else {
				DebugMessage("Match failure ...");
			}
		}
		
		if (!success) {
			GD.PrintErr($"File does not exist: '{filePath}' ...");
		} 
	}

	private void LoadScene(string scenePath) {
		PackedScene sceneResource = (PackedScene)ResourceLoader.Load(scenePath);
		GetTree().ChangeSceneToPacked(sceneResource);
	}


	private void DebugMessage(string message) {
		if(debug) {
			GD.Print(message);
		}
	}
}
