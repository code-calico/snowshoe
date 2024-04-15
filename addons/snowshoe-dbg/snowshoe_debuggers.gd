@tool
extends EditorPlugin

func _enter_tree():
	add_custom_type( 
		"CharacterDebug",  
		"Node2D",
		preload("CharacterDebugger.cs"), 
		preload("CharacterDebugger.svg")
	)

func _exit_tree(): 
	remove_custom_type("CharacterDebug")
