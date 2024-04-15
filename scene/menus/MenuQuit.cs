using Godot;
using System;
using System.Diagnostics;
using System.IO;

public partial class MenuQuit : Button {
	public override void _Ready() => this.ButtonUp += OnButtonUp;

	// "button_up" signal subscription
	private void OnButtonUp() {

		GetTree().Quit();
	}
}
