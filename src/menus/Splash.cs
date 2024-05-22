using Godot;
using System;

public partial class Splash : Control
{
	Timer timer;

	public override void _Ready(){
		timer = GetNode<Timer>("%SplashDuration");	
		timer.Timeout += SplashFinished; 
		
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (@event.IsPressed()) {
			GD.Print("test");
			timer.Timeout -= SplashFinished;
			SceneManager.Load("res://scenes/levels/dev/lvl-dev-test.tscn");
		}
	}

	private void SplashFinished() => SceneManager.Load("res://scenes/menus/title.tscn");

}
