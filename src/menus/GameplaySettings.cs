using Godot;
using System;

public partial class GameplaySettings : Panel
{
	CheckBox skipSplash;
	Button resetConfig;

	string sectionKey = ConfigKeys.Gameplay.SectionKey;
	ConfigKey skipSplashKey = ConfigKeys.Gameplay.SkipSplash;

	public override void _Ready() {
		skipSplash = GetNode<CheckBox>("%SkipSplashSelect");
		resetConfig = GetNode<Button>("%ResetToDefault");

		skipSplash.Toggled += (bool state) => GameSettings.ConfigWrite(skipSplashKey, state);
		skipSplash.SetPressedNoSignal(GameSettings.ConfigRead(skipSplashKey).AsBool());

		resetConfig.ButtonUp += () => {
			GameSettings.ClearSection(sectionKey);
			skipSplash.SetPressedNoSignal(GameSettings.ConfigRead(skipSplashKey).AsBool());
		};
	}

}
