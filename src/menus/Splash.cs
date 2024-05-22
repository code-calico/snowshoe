using Godot;
using System;

public partial class Splash : Control
{
	Timer timer;

	public override void _Ready(){
		timer = GetNode<Timer>("%SplashDuration");	
		timer.Timeout += () => SceneManager.Load("res://scenes/menus/title.tscn");
	}


}
