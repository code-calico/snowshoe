using Godot;
using System;
using System.Collections.Generic;

public partial class VideoSettings : Panel
{
	OptionButton resolutionOptions;
	OptionButton fullscreenOptions;
	OptionButton vsyncOptions;
	SpinBox fpsOption;

	List<Vector2I> resolutions = new List<Vector2I>();
	List<DisplayServer.WindowMode> fullscreenModes = new List<DisplayServer.WindowMode>();
	List<DisplayServer.VSyncMode> vsyncModes = new List<DisplayServer.VSyncMode>();

	public override void _Ready() {
		InitReferences();

		InitResolutionOptions();
		InitFullscreenOptions();
		InitFPSOption();
		InitVSyncOptions();

		InitSubscriptions();
	}

	void InitResolutionOptions() {
		resolutionOptions.Clear();

		resolutions.Add(new Vector2I(3840,2160));
		resolutions.Add(new Vector2I(3440,1440));
		resolutions.Add(new Vector2I(2560,1600));
		resolutions.Add(new Vector2I(2560,1440));
		resolutions.Add(new Vector2I(2560,1080));
		resolutions.Add(new Vector2I(2048,1536));
		resolutions.Add(new Vector2I(2048,1152));
		resolutions.Add(new Vector2I(1920,1200));
		resolutions.Add(new Vector2I(1920,1080));
		resolutions.Add(new Vector2I(1680,1050));
		resolutions.Add(new Vector2I(1600,1200));
		resolutions.Add(new Vector2I(1600,900));
		resolutions.Add(new Vector2I(1536,864));
		resolutions.Add(new Vector2I(1440,900));
		resolutions.Add(new Vector2I(1366,768));
		resolutions.Add(new Vector2I(1360,768));
		resolutions.Add(new Vector2I(1280,1024));
		resolutions.Add(new Vector2I(1280,800));
		resolutions.Add(new Vector2I(1280,720));
		resolutions.Add(new Vector2I(1024,768));
		resolutions.Add(new Vector2I(960,540));

		for (int i = 0; i < resolutions.Count; i++) {
			string resolutionString = resolutions[i].X.ToString() + "x" + resolutions[i].Y.ToString();
			resolutionOptions.AddItem(resolutionString);
		}

		// selects project default
		resolutionOptions.Select(20);
	}

	void InitFullscreenOptions() {
		fullscreenOptions.Clear();

		fullscreenModes.Add(DisplayServer.WindowMode.Windowed);
		fullscreenModes.Add(DisplayServer.WindowMode.Fullscreen);
		fullscreenModes.Add(DisplayServer.WindowMode.ExclusiveFullscreen);

		for (int i = 0; i < fullscreenModes.Count; i++) {
			fullscreenOptions.AddItem(fullscreenModes[i].ToString());
		}

		// selects project default
		fullscreenOptions.Select(0);
	}

	void InitFPSOption() {
		// selects project default
		fpsOption.Value = 60;
	}

	void InitVSyncOptions() {
		vsyncOptions.Clear();

		vsyncModes.Add(DisplayServer.VSyncMode.Enabled);
		vsyncModes.Add(DisplayServer.VSyncMode.Disabled);
		vsyncModes.Add(DisplayServer.VSyncMode.Adaptive);
		vsyncModes.Add(DisplayServer.VSyncMode.Mailbox);

		for (int i = 0; i < vsyncModes.Count; i++) {
			vsyncOptions.AddItem(vsyncModes[i].ToString());
		}

		// selects project default
		vsyncOptions.Select(0);
	}

	void InitReferences() {
		resolutionOptions = GetNode<OptionButton>("%ResolutionOptions");
		fullscreenOptions = GetNode<OptionButton>("%FullscreenOptions");
		vsyncOptions = GetNode<OptionButton>("%VSyncOptions");
		fpsOption = GetNode<SpinBox>("%FPSOption");
	}

	void InitSubscriptions() {
		resolutionOptions.ItemSelected += (long idx) => {
			DisplayServer.WindowSetSize(resolutions[(int)idx]);
		};

		fullscreenOptions.ItemSelected += (long idx) => {
			DisplayServer.WindowSetMode(fullscreenModes[(int)idx]);
		};

		fpsOption.ValueChanged += (double val) => {
			Engine.MaxFps = (int)val;
		};

		vsyncOptions.ItemSelected += (long idx) => {
			DisplayServer.WindowSetVsyncMode(vsyncModes[(int)idx]);
		};

	}

}
