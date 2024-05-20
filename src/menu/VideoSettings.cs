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
		resolutionSelector.ItemSelected += (long idx) => { 
			Vector2I resolution = ResolutionOptions.GetArray()[(int)idx];
			DisplayServer.WindowSetSize(resolution); 
		};

		fullscreenSelector.ItemSelected += (long idx) => { 
			DisplayServer.WindowMode fullscreenMode = FullscreenOptions.GetArray()[(int)idx]; 
			DisplayServer.WindowSetMode(fullscreenMode); 
		};

		vsyncSelector.ItemSelected += (long idx) => { 
			DisplayServer.VSyncMode vsyncMode = VSyncOptions.GetArray()[(int)idx];
			DisplayServer.WindowSetVsyncMode(vsyncMode); 
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
}
