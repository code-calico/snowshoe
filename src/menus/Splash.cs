using Godot;
using System;

public partial class Splash : Control
{
	AnimationPlayer animationPlayer;
	bool skipSplash;

	public override void _Ready(){
		animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");

		skipSplash = GameSettings.ConfigRead(ConfigKeys.Gameplay.SkipSplash).AsBool();
		if (skipSplash) {
			SceneManager.Load(SceneFiles.Levels.DEV_TESTING);
		} else {
			animationPlayer.Play("fade_in");
			animationPlayer.AnimationFinished += SplashFinished; 
		}
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (@event.IsPressed() && !skipSplash) {
			animationPlayer.AnimationFinished -= SplashFinished;
			SceneManager.Load(SceneFiles.Menus.MAIN);
		}
	}

	private void SplashFinished(StringName name) => SceneManager.Load(SceneFiles.Menus.MAIN);

}
