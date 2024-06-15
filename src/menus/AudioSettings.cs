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
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InitReferences();
		InitSubscriptions();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	void InitSubscriptions()
	{
		MasterVolumeSlider.ValueChanged += (double val) =>
		{
			float CurrentMasterVolume = (float)MasterVolumeSlider.Value;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), CurrentMasterVolume);
			GameSettings.ConfigWrite(MasterVolume, CurrentMasterVolume);
		};
		MusicVolumeSlider.ValueChanged += (double val) =>
		{
			float CurrentMusicVolume = (float)MusicVolumeSlider.Value;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("sfx"), CurrentMusicVolume);
			GameSettings.ConfigWrite(MusicVolume, CurrentMusicVolume);
		};
		SFXVolumeSlider.ValueChanged += (double val) =>
		{
			float CurrentSFXVolume = (float)SFXVolumeSlider.Value;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("music"), CurrentSFXVolume);
			GameSettings.ConfigWrite(SFXVolume, CurrentSFXVolume);
		};
	}
	void InitReferences()
	{
		MasterVolumeSlider = GetNode<HSlider>("%MasterVolumeSlider");
		SFXVolumeSlider = GetNode<HSlider>("%SFXVolumeSlider");
		MusicVolumeSlider = GetNode<HSlider>("%MusicVolumeSlider");
	}
}
