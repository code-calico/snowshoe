using Godot;

public partial class OptionsMenu : CanvasLayer
{
	Button backButton;	
	TabBar tabBar;
	Control panelHolder;
	
	Panel[] panels;

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ui_cancel")) { 
			Hide(); 
		}
	}

	public override void _Ready() {
		InitReferences();
		InitPanels();
		InitSubscriptions();
		// compiles w/o error, but nothing seems to actually be focused. annoying
		GetNode<TabBar>("%TabBar").GrabFocus();
	}

	// hide all panels and turn the new panel visible
	void FocusPanel(long tab) {
		for (int i = 0; i < panels.Length; i++) {
			panels[i].Visible = false;
		}
		panels[tab].Visible = true;	
	}

	void InitReferences() {
		backButton = GetNode<Button>("%BackButton");
		tabBar = GetNode<TabBar>("%TabBar");
		panelHolder = GetNode<Control>("%PanelHolder");
	}

	void InitPanels() {
		int panelCount = panelHolder.GetChildCount();
		panels = new Panel[panelCount];

		for (int i = 0; i < panels.Length; i++) {
			panels[i] = panelHolder.GetChild<Panel>(i);
		}

		FocusPanel(0);
	}

	void InitSubscriptions() {
		backButton.ButtonUp += Hide;
		tabBar.TabChanged += FocusPanel;
	}


}


