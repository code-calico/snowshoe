using Godot;
using System;
using System.Threading;


public partial class PauseEvent : Node
{
	public override void _Ready() {
		CheckPauseEvent();
		SceneManager.Get().SceneChanged += CheckPauseEvent;
	}

	private void CheckPauseEvent() {
		GD.Print("test");
		string scenePath = GetTree().CurrentScene.SceneFilePath;
		string[] pathTokens = scenePath.Split("/");
		string parentFolder = pathTokens[3];
	}
	

}
