using Godot;

public partial class PauseMenu : CanvasLayer
{
	
	[Export] bool isVisibleOnStart;

	Button resume;
	Button options;
	Button title;
	Button quit;

	CanvasLayer optionsMenu;

	bool paused = false;
	
	public override void _Ready() {
		InitReferences();
		InitSubscriptions();
		Visible = isVisibleOnStart;
	}

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ui_cancel") && !optionsMenu.Visible) {
			Visible = !Visible;
		}
	}

	void InitReferences() {
		resume = GetNode<Button>("%Resume");
		options = GetNode<Button>("%Options");
		title = GetNode<Button>("%Title");
		quit = GetNode<Button>("%Quit");
		optionsMenu = GetNode<CanvasLayer>("%OptionsMenu");
	}

	void InitSubscriptions() {
		resume.ButtonUp += () => {
			GetTree().Paused = false;
			Hide();
		};

		options.ButtonUp += optionsMenu.Show;
		
		title.ButtonUp += () => {
			GetTree().Paused = false;
			SceneManager.Load("res://scenes/menus/title.tscn");
		};
		
		quit.ButtonDown += SceneManager.QuitToDesktop;
	}
}


