using Godot;
using System;
using System.Collections.Generic;

public partial class OptionsMenu : CanvasLayer
{
	[Export] Button back;	
	[Export] TabBar bar;
	[Export] Control panelHolder;
	
	Panel[] panels;
	
	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ui_cancel")) { Back(); }
	}

	public override void _Ready() {
		int panelCount = panelHolder.GetChildCount();
		panels = new Panel[panelCount];

		for (int i = 0; i < panels.Length; i++) {
			panels[i] = panelHolder.GetChild<Panel>(i);
		}

		FocusPanel(0);

		//listen for back  button and changing tab events
		back.ButtonUp += Back;
		bar.TabChanged += TabSelected;
	}

	void TabSelected(long tab) { FocusPanel(tab); }

	// make selected panel visible
	void FocusPanel(long tab) {
		for (int i = 0; i < panels.Length; i++) {
			panels[i].Visible = false;
		}
		panels[tab].Visible = true;	
	}

	void Back() => QueueFree();
}


