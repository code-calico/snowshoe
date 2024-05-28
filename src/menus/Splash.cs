using Godot;
using System;

public partial class Splash : Control
{
	AnimationPlayer animationPlayer;
	bool skipped = false;

	public override void _Ready(){
		animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");

		bool skipSplash = GameSettings.ConfigRead(ConfigKeys.Gameplay.SkipSplash).AsBool();
		if (skipSplash) {
			SceneManager.Load(SceneFiles.Levels.DEV_TESTING);
		} else {
			animationPlayer.Play("fade_in");
			animationPlayer.AnimationFinished += SplashFinished; 
		}
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (@event.IsPressed() && !skipped) {
			skipped = true;
			animationPlayer.AnimationFinished -= SplashFinished;
			SceneManager.Load(SceneFiles.Menus.MAIN);
		}
	}

	private void SplashFinished(StringName name) => SceneManager.Load(SceneFiles.Menus.MAIN);

}
