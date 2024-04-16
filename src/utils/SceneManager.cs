using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class SceneManager : Node {

    public static SceneManager Instance { get; private set; }

    public override void _Ready() {
        if (Instance != null && Instance != this) {
            QueueFree();
        } else {
            Instance = this;
        }
    }

    public static void Load(string scenePath) {
        string pathToScene = ProjectSettings.GlobalizePath(scenePath);
		PackedScene sceneResource = (PackedScene)ResourceLoader.Load(pathToScene);
		Instance.GetTree().ChangeSceneToPacked(sceneResource);
    }

    public static SceneManager Get() => Instance;
    
}

