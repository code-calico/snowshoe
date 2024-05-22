using Godot;
using System;

public partial class Splash : Control
{
	AnimationPlayer animationPlayer;
	public override void _Ready(){
		animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");

		bool skipSplash = GameSettings.ConfigRead(ConfigKeys.Gameplay.SkipSplash).AsBool();
		if (skipSplash) {
			SceneManager.Load("res://scenes/levels/dev/lvl-dev-test.tscn");
		} else {
			animationPlayer.Play("fade_in");
			animationPlayer.AnimationFinished += SplashFinished; 
		}
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (@event.IsPressed()) {
			animationPlayer.AnimationFinished -= SplashFinished;
			SceneManager.Load("res://scenes/levels/dev/lvl-dev-test.tscn");
		}
	}

	private void SplashFinished(StringName name) => SceneManager.Load("res://scenes/menus/title.tscn");

}
