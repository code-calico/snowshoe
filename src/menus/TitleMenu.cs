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
		InitReferences();
		InitSubscriptions(); 
	}

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ui_cancel") && !optionsMenu.Visible) {
			SceneManager.QuitToDesktop();
		}
	}

	void InitReferences() {
		play = GetNode<Button>("%Play");
		options = GetNode<Button>("%Options");
		quit = GetNode<Button>("%Quit");
		optionsMenu = GetNode<CanvasLayer>("%OptionsMenu");
	}

	void InitSubscriptions() {
		play.ButtonUp += () => SceneManager.Load(playScenePath);
		options.ButtonUp += optionsMenu.Show; 
		quit.ButtonUp += SceneManager.QuitToDesktop;  
	}
	
}
