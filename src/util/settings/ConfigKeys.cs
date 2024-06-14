using Godot;

public static class ConfigKeys {
    public class Gameplay {
        public const string SectionKey = "gameplay";
        public static ConfigKey SkipSplash = new ConfigKey(SectionKey, "skipSplash", false);
    }
    
    public class Video {
        public const string SectionKey = "video";
        public static ConfigKey Resolution = new ConfigKey(SectionKey, "resolution", 20);
        public static ConfigKey FullscreenMode = new ConfigKey(SectionKey, "fullscreenMode", 0);
        public static ConfigKey VSyncMode = new ConfigKey(SectionKey, "vsyncMode", 0);
        public static ConfigKey MaxFPS = new ConfigKey(SectionKey, "maxFPS", 60);
    }

    public class Audio
    {
        public const string SectionKey = "audio";
        public static ConfigKey MasterVolume = new ConfigKey(SectionKey, "masterVolume", 100);
        public static ConfigKey MusicVolume = new ConfigKey(SectionKey, "Musicvolume", 100);
        public static ConfigKey SFXVolume = new ConfigKey(SectionKey, "sfxVolume", 100);
    }

    public class Controls {

    }
}