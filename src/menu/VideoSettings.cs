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

	ConfigFile videoConfig = new ConfigFile();
	
	const string configPath = "user://video.cfg";
	
	const int defaultResolution = 20;
	const int defaultFullScreen = 0;
	const int defaultVSync = 0;
	const int defaultFPS = 60;

	public override void _Ready() {
		InitReferences();

		videoConfig.Load(configPath);
			
		InitOptionsButton (
			resolutionSelector, 
			ResolutionOptions.GetStringArray(),
			videoConfig.GetValue("video", "resolution", defaultResolution).AsInt32()
		);

		InitOptionsButton (
			fullscreenSelector, 
			FullscreenOptions.GetStringArray(), 
			videoConfig.GetValue("video", "fullscreenMode", defaultFullScreen).AsInt32()
		);

		InitOptionsButton (
			vsyncSelector, 
			VSyncOptions.GetStringArray(), 
			videoConfig.GetValue("video", "vsyncMode", defaultVSync).AsInt32()
		);

		fpsSelector.Value = videoConfig.GetValue("video", "maxFPS", defaultFPS).AsInt32();
		
		InitSubscriptions();
	}
	
	void InitSubscriptions() {

		resolutionSelector.ItemSelected += (long idx) => {
			Vector2I resolution = ResolutionOptions.GetArray()[(int)idx];
			DisplayServer.WindowSetSize(resolution);
			ConfigWrite("resolution", (int)idx);
		};

		fullscreenSelector.ItemSelected += (long idx) => { 
			DisplayServer.WindowMode fullscreenMode = FullscreenOptions.GetArray()[(int)idx]; 
			DisplayServer.WindowSetMode(fullscreenMode);
			ConfigWrite("fullscreenMode", (int)idx);
		};

		vsyncSelector.ItemSelected += (long idx) => { 
			DisplayServer.VSyncMode vsyncMode = VSyncOptions.GetArray()[(int)idx];
			DisplayServer.WindowSetVsyncMode(vsyncMode);
			ConfigWrite("vsyncMode", (int)idx);
		};

		fpsSelector.ValueChanged += (double val) => { 
			Engine.MaxFps = (int)val; 
			ConfigWrite("maxFPS", (int)val);
		};

		resetButton.ButtonUp += () => {
			videoConfig.Clear();
			videoConfig.Save(configPath);

			DisplayServer.WindowSetSize(ResolutionOptions.GetArray()[defaultResolution]);
			resolutionSelector.Select(defaultResolution);
			DisplayServer.WindowSetMode(FullscreenOptions.GetArray()[defaultFullScreen]);
			fullscreenSelector.Select(defaultFullScreen);
			DisplayServer.WindowSetVsyncMode(VSyncOptions.GetArray()[defaultVSync]);
			vsyncSelector.Select(defaultVSync);
			Engine.MaxFps = defaultFPS;
			fpsSelector.Value = defaultFPS;			
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

	void ConfigWrite(string key, Variant value) {
		videoConfig.SetValue("video", key, value);
		videoConfig.Save(configPath);
	}
}
