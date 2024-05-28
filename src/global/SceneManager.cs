using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class SceneManager : Node {

	public static SceneManager Instance { get; private set; }

	[Signal]
	public delegate void SceneChangedEventHandler(string scenePath);

	public override void _Ready() {
		if (Instance != null && Instance != this) {
			QueueFree();
		} else {
			Instance = this;
		}
	}

	public static async void Load(string scenePath) {
		string pathToScene = ProjectSettings.GlobalizePath(scenePath);
		PackedScene sceneResource = (PackedScene)ResourceLoader.Load(pathToScene);
		
		// instantiating a scene transition instance
		PackedScene transition = GD.Load<PackedScene>(SceneFiles.Transitions.FADE);
		Node transitionInstance = transition.Instantiate();
		Instance.AddChild(transitionInstance);
		AnimationPlayer animPlayer = transitionInstance.GetNode<AnimationPlayer>("%AnimationPlayer");
		
		// fade in 
		animPlayer.Play("fade");
		await animPlayer.ToSignal(animPlayer, "animation_finished");
		
		// switch scene
		Instance.GetTree().ChangeSceneToPacked(sceneResource);
		animPlayer.PlayBackwards("fade");
		
		// fade out
		await animPlayer.ToSignal(animPlayer, "animation_finished");
		transitionInstance.QueueFree();
		
		Instance.EmitSignal(SignalName.SceneChanged, scenePath);
	}

	public static void QuitToDesktop() => Instance.GetTree().Quit();

	public static SceneManager Get() => Instance;
	
}

