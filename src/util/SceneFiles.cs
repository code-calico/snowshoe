
using System.IO;

public static class SceneFiles {

    public static readonly string directory = "res://scenes/";

    public static class Transitions {
        public static readonly string directory = SceneFiles.directory + "transitions";

        public static readonly string FADE = BuildPath("fade_transition.tscn");

        public static string BuildPath(string path) => directory + path;
    }

    public static class Menus {
        public static readonly string directory = SceneFiles.directory + "menus";

        public static readonly string MAIN = BuildPath("title.tscn");
        public static readonly string SPLASH = BuildPath("splash.tscn"); 

        public static string BuildPath(string path) => directory + path;
    }

    public static class Levels {
        public static readonly string directory = SceneFiles.directory;

        public static readonly string DEV_TESTING = BuildPath("dev/level-dev-test.tscn");

        public static string BuildPath(string path) => directory + path;
    }

}