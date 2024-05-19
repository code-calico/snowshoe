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
		backButton = GetNode<Button>("%BackButton");
		tabBar = GetNode<TabBar>("%TabBar");
		panelHolder = GetNode<Control>("%PanelHolder");
		
		int panelCount = panelHolder.GetChildCount();
		panels = new Panel[panelCount];

		for (int i = 0; i < panels.Length; i++) {
			panels[i] = panelHolder.GetChild<Panel>(i);
		}

		FocusPanel(0);

		// event subscription
		backButton.ButtonUp += Hide;
		tabBar.TabChanged += FocusPanel;
	}


	// hide all panels and turn the new panel visible
	void FocusPanel(long tab) {
		for (int i = 0; i < panels.Length; i++) {
			panels[i].Visible = false;
		}
		panels[tab].Visible = true;	
	}
}


