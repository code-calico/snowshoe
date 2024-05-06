using Godot;

public partial class PauseMenu : CanvasLayer
{
	[Export] Button resume;
	[Export] Button options;
	[Export] Button title;
	[Export] Button desktop;
	
	public override void _Ready() {
		resume.ButtonUp += Resume;
		options.ButtonUp += Options;
		title.ButtonUp += Title;
		desktop.ButtonDown += Desktop;
	}

	void Resume() {
		GetTree().Paused = false;
		QueueFree();
	}

	void Options() {
		PackedScene optionsScene = GD.Load<PackedScene>("res://scene/menus/options.tscn");
		AddChild(optionsScene.Instantiate());
	}

	void Desktop() => GetTree().Quit();
	void Title() {
		QueueFree();
		GetTree().Paused = false;
		SceneManager.Load("res://scene/menus/title.tscn");
	} 
}


