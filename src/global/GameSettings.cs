using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class GameSettings : Node {

	public static GameSettings Instance { get; private set; }
	
	ConfigFile settingsConfig = new ConfigFile();
	const string configPath = "user://settings.cfg";

	public override void _Ready() {
		if (Instance != null && Instance != this) {
			QueueFree();
		} else {
			Instance = this;
		}
		settingsConfig.Load(configPath);

		ApplyVideoSettings();
		ApplyAudioSettings();
	}

	public static void ConfigWrite(ConfigKey key, Variant value) {		
		Instance.settingsConfig.SetValue(key.GetSectionKey(), key.GetKey(), value);
		Instance.settingsConfig.Save(configPath);
	}

	public static Variant ConfigRead(ConfigKey key) {
		return Instance.settingsConfig.GetValue(key.GetSectionKey(), key.GetKey(), key.GetDefault());
	}

	public static void ClearSection(string sectionKey) {
		if (Instance.settingsConfig.HasSection(sectionKey)) {
			Instance.settingsConfig.EraseSection(sectionKey);
			Instance.settingsConfig.Save(configPath);
		}
	}

	public static void ClearConfig() {
		Instance.settingsConfig.Clear();
		Instance.settingsConfig.Save(configPath);
	}

	public static GameSettings Get() => Instance;

	public static void ApplyAudioSettings() {
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), (float)ConfigRead(ConfigKeys.Audio.MasterVolume).AsDouble());
		GD.Print("Master Bus DB: " + AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master")));
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("music"), (float)ConfigRead(ConfigKeys.Audio.MusicVolume).AsDouble());
		GD.Print("Music Bus DB: " + AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("music")));
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("sfx"), (float)ConfigRead(ConfigKeys.Audio.SFXVolume).AsDouble());
		GD.Print("SFX Bus DB: " + AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("sfx")));

	}

	public static void ApplyVideoSettings() {
		Vector2I[] resolutions = ResolutionOptions.GetArray();
		DisplayServer.WindowSetSize(resolutions[ConfigRead(ConfigKeys.Video.Resolution).AsInt32()]);

		DisplayServer.WindowMode[] windowModes = FullscreenOptions.GetArray();
		DisplayServer.WindowSetMode(windowModes[ConfigRead(ConfigKeys.Video.FullscreenMode).AsInt32()]);

		DisplayServer.VSyncMode[] vsyncModes = VSyncOptions.GetArray();
		DisplayServer.WindowSetVsyncMode(vsyncModes[ConfigRead(ConfigKeys.Video.VSyncMode).AsInt32()]);

		Engine.MaxFps = ConfigRead(ConfigKeys.Video.Resolution).AsInt32();	
	}
}
