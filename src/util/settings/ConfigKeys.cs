using Godot;

public static class ConfigKeys {
    public enum Gameplay {

    }
    
    public class Video {
        public const string SectionKey = "video";
        public static ConfigKey Resolution = new ConfigKey(SectionKey, "resolution", 20);
        public static ConfigKey FullscreenMode = new ConfigKey(SectionKey, "fullscreenMode", 0);
        public static ConfigKey VSyncMode = new ConfigKey(SectionKey, "vsyncMode", 0);
        public static ConfigKey MaxFPS = new ConfigKey(SectionKey, "maxFPS", 60);
    }

    public enum Audio {

    }
    public enum Controls {

    }
}