using Godot;
using System;

public partial class AudioSettings : Panel
{
	HSlider MasterVolumeSlider;
	HSlider SFXVolumeSlider;
	HSlider MusicVolumeSlider;

	ConfigKey MasterVolume = ConfigKeys.Audio.MasterVolume;
	ConfigKey SFXVolume = ConfigKeys.Audio.SFXVolume;
	ConfigKey MusicVolume = ConfigKeys.Audio.MusicVolume;
	
	public override void _Ready() {
		InitReferences();
		InitSubscriptions();
	}

	public override void _Process(double delta) {
		MasterVolumeSlider.Value = GameSettings.ConfigRead(MasterVolume).AsDouble();
		MusicVolumeSlider.Value = GameSettings.ConfigRead(MusicVolume).AsDouble();
		SFXVolumeSlider.Value = GameSettings.ConfigRead(SFXVolume).AsDouble();
	}

	void InitSubscriptions() {
		MasterVolumeSlider.ValueChanged += (double val) => {
			float CurrentMasterVolume = (float)MasterVolumeSlider.Value;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), CurrentMasterVolume);
			GameSettings.ConfigWrite(MasterVolume, CurrentMasterVolume);
		};

		MusicVolumeSlider.ValueChanged += (double val) => {
			float CurrentMusicVolume = (float)MusicVolumeSlider.Value;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("music"), CurrentMusicVolume);
			GameSettings.ConfigWrite(MusicVolume, CurrentMusicVolume);
		};

		SFXVolumeSlider.ValueChanged += (double val) => {
			float CurrentSFXVolume = (float)SFXVolumeSlider.Value;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("sfx"), CurrentSFXVolume);
			GameSettings.ConfigWrite(SFXVolume, CurrentSFXVolume);
		};
	}

	void InitReferences() {
		MasterVolumeSlider = GetNode<HSlider>("%MasterVolumeSlider");
		SFXVolumeSlider = GetNode<HSlider>("%SFXVolumeSlider");
		MusicVolumeSlider = GetNode<HSlider>("%MusicVolumeSlider");
	}
}
