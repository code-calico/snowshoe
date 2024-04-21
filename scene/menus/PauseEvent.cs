using Godot;
using System;
using System.Threading;


public partial class PauseEvent : Node
{

	public override void _EnterTree()
	{
		GD.Print("switch");

		string test = GetTree().CurrentScene.SceneFilePath;
		string[] testSpilt = test.Split("/");
		string folderSceneIsIn = testSpilt[3];
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
