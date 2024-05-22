using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class VideoSettings : Panel
{
	OptionButton resolutionSelector;
	OptionButton fullscreenSelector;
	OptionButton vsyncSelector;
	SpinBox fpsSelector;
	Button resetButton;

	string sectionKey = ConfigKeys.Video.SectionKey;
	ConfigKey resolutionKey = ConfigKeys.Video.Resolution;
	ConfigKey fullscreenKey = ConfigKeys.Video.FullscreenMode;
	ConfigKey vsyncKey = ConfigKeys.Video.VSyncMode;
	ConfigKey maxFPSKey = ConfigKeys.Video.MaxFPS;

	public override void _Ready() {
		InitReferences();
			
		InitOptionsButton(resolutionSelector, ResolutionOptions.GetStringArray(), GameSettings.ConfigRead(resolutionKey).AsInt32());
		InitOptionsButton(fullscreenSelector, FullscreenOptions.GetStringArray(), GameSettings.ConfigRead(fullscreenKey).AsInt32());
		InitOptionsButton(vsyncSelector, VSyncOptions.GetStringArray(), GameSettings.ConfigRead(vsyncKey).AsInt32());
		fpsSelector.Value = GameSettings.ConfigRead(maxFPSKey).AsInt32();
		
		InitSubscriptions();
	}
	
	void InitSubscriptions() {
		resolutionSelector.ItemSelected += (long idx) => {
			Vector2I resolution = ResolutionOptions.GetArray()[(int)idx];
			DisplayServer.WindowSetSize(resolution);
			GameSettings.ConfigWrite(resolutionKey, (int)idx);
		};

		fullscreenSelector.ItemSelected += (long idx) => { 
			DisplayServer.WindowMode fullscreenMode = FullscreenOptions.GetArray()[(int)idx]; 
			DisplayServer.WindowSetMode(fullscreenMode);
			GameSettings.ConfigWrite(fullscreenKey, (int)idx);
		};

		vsyncSelector.ItemSelected += (long idx) => { 
			DisplayServer.VSyncMode vsyncMode = VSyncOptions.GetArray()[(int)idx];
			DisplayServer.WindowSetVsyncMode(vsyncMode);
			GameSettings.ConfigWrite(vsyncKey, (int)idx);
		};

		fpsSelector.ValueChanged += (double val) => { 
			Engine.MaxFps = (int)val; 
			GameSettings.ConfigWrite(maxFPSKey, (int)val);
		};

		resetButton.ButtonUp += () => {
			GameSettings.ClearSection(sectionKey);
			GameSettings.ApplyVideoSettings();

			resolutionSelector.Select(resolutionKey.GetDefault().AsInt32());
			fullscreenSelector.Select(fullscreenKey.GetDefault().AsInt32());
			vsyncSelector.Select(vsyncKey.GetDefault().AsInt32());
			fpsSelector.Value = maxFPSKey.GetDefault().AsDouble();			
		};
	}

	void InitReferences() {
		resolutionSelector = GetNode<OptionButton>("%ResolutionSelector");
		fullscreenSelector = GetNode<OptionButton>("%FullscreenSelector");
		vsyncSelector = GetNode<OptionButton>("%VSyncSelector");
		fpsSelector = GetNode<SpinBox>("%FPSSelector");
		resetButton = GetNode<Button>("%ResetToDefault");
	}

	void InitOptionsButton(OptionButton btn, string[] values, int indexFocus) {
		btn.Clear();
		for (int i = 0; i < values.Length; i++) {
			btn.AddItem(values[i]);
		}
		btn.Select(indexFocus); // selects project default
	}
	
}
