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
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	void InitReferences()
	{
		MasterVolumeSlider = GetNode<HSlider>("%MasterVolumeSlider");
		MasterVolumeSlider = GetNode<HSlider>("%SFXVolumeSlider");
		MasterVolumeSlider = GetNode<HSlider>("%MusicVolumeSlider");
	}
}
