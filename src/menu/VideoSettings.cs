using Godot;
using System;
using System.Collections.Generic;

public partial class VideoSettings : Panel
{
	OptionButton resolutionSelector;
	OptionButton fullscreenSelector;
	OptionButton vsyncSelector;
	SpinBox fpsSelector;


	public override void _Ready() {
		InitReferences();

		InitOptionsButton(resolutionSelector, ResolutionOptions.GetStringArray(), 20);
		InitOptionsButton(fullscreenSelector, FullscreenOptions.GetStringArray(), 0);
		InitOptionsButton(vsyncSelector, VSyncOptions.GetStringArray(), 0);
		fpsSelector.Value = 60;
		
		InitSubscriptions();
	}
	
	void InitSubscriptions() {

	var config = new ConfigFile();
		resolutionSelector.ItemSelected += (long idx) => {
			Vector2I resolution = ResolutionOptions.GetArray()[(int)idx];
			DisplayServer.WindowSetSize(resolution);
			config.SetValue("video", "resolution", resolution);
		config.Save("user://video.cfg");
		};

		fullscreenSelector.ItemSelected += (long idx) => { 
			DisplayServer.WindowMode fullscreenMode = FullscreenOptions.GetArray()[(int)idx]; 
			DisplayServer.WindowSetMode(fullscreenMode);
			//very very hacky so sorry to whoever reads this
		string currentFullscreenMode = Convert.ToString(fullscreenMode);
			config.SetValue("video", "fullscreenMode", currentFullscreenMode);
		config.Save("user://video.cfg");
		};

		vsyncSelector.ItemSelected += (long idx) => { 
			DisplayServer.VSyncMode vsyncMode = VSyncOptions.GetArray()[(int)idx];
			DisplayServer.WindowSetVsyncMode(vsyncMode);
			string currentVsyncMode = Convert.ToString(vsyncMode);
			config.SetValue("video", "vsyncMode", currentVsyncMode);
			//may allah forgive me for the absolute dogshit way i've implemented this
		config.Save("user://video.cfg");
		};

		fpsSelector.ValueChanged += (double val) => { Engine.MaxFps = (int)val; };
	}

	void InitReferences() {
		resolutionSelector = GetNode<OptionButton>("%ResolutionSelector");
		fullscreenSelector = GetNode<OptionButton>("%FullscreenSelector");
		vsyncSelector = GetNode<OptionButton>("%VSyncSelector");
		fpsSelector = GetNode<SpinBox>("%FPSSelector");
	}

	void InitOptionsButton(OptionButton btn, string[] values, int indexFocus) {
		btn.Clear();
		for (int i = 0; i < values.Length; i++) {
			btn.AddItem(values[i]);
		}
		btn.Select(indexFocus); // selects project default
	}

	void readConfigFile()
	{
		var config = new ConfigFile();

// Load data from a file.
	Error err = config.Load("user://scores.cfg");

// If the file didn't load, ignore it.
	if (err != Error.Ok) {
    return;
	}
	foreach (string video in config.GetSections())
	{
	var test = config.GetValue(video, "resolution");
	DisplayServer.WindowSetMode(test);
	}

	}
}
