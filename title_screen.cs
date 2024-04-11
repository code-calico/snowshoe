using Godot;
using System;
public partial class title_screen : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		this.ButtonUp += OnButtonUp;
	}

	private void OnButtonUp() {
		GD.Print("Please fucking work!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
