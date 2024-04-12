using Godot;
using System;
using System.Diagnostics;
using System.IO;

public partial class SceneButton : Button
{	

	// set the scene variable in the inspector rather than in code please !
	[Export(PropertyHint.File, "*.tscn")] 
	public string scene = "";

	public override void _Ready() => this.ButtonUp += OnButtonUp;

	// "button_up" signal subscription
	private void OnButtonUp() {

		string pathToScene = ProjectSettings.GlobalizePath(scene);
		PackedScene sceneResource = (PackedScene)ResourceLoader.Load(pathToScene);
		GetTree().ChangeSceneToPacked(sceneResource);
	}
}
