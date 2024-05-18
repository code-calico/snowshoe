using Godot;
using System;

public partial class TitleMenu : Control
{
	Button play; 
	Button options;
	Button quit;
	
	CanvasLayer optionsMenu;
	
	[Export] string playScenePath = "res://scenes/levels/dev/lvl-dev-test.tscn";
	[Export] string optionsScenePath = "res://scenes/menus/options.tscn";

	public override void _Ready() {
		// getting references to nodes using scene unique names, this is pretty common in gdscript but it feels kinda weird in c# 
		play = GetNode<Button>("%Play");
		options = GetNode<Button>("%Options");
		quit = GetNode<Button>("%Quit");
		optionsMenu = GetNode<CanvasLayer>("%OptionsMenu");
	
		// subscribes using an anonymous function, just to supply it with the scene path 
		play.ButtonUp += () => SceneManager.Load(playScenePath);
		// makes the options menu visible and process input events using the show() function on the canvas layer node
		options.ButtonUp += optionsMenu.Show; 
		quit.ButtonUp += SceneManager.QuitToDesktop;  
	}

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ui_cancel") && !optionsMenu.Visible) {
			SceneManager.QuitToDesktop();
		}
	}
	
}
