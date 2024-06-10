using Godot;
using System;

public partial class Killzone : Area2D
{
	public override void _Ready() {
		BodyEntered += (Node2D body) => {
			if (body.Name == "Player") {
				SceneManager.Load(GetTree().CurrentScene.SceneFilePath);
			}
		};
	}
}



