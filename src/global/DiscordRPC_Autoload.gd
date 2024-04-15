extends Node

func _ready():
	DiscordRPC.app_id = 1229233679285616685
	if (DiscordRPC.get_is_discord_working()):
		print("Established Connection w/ Discord ...!")
	else:
		print("Connection w/ Discord Failed ...")
	DiscordRPC.state = "Now Playing:"
	DiscordRPC.start_timestamp = int(Time.get_unix_time_from_system())
	DiscordRPC.refresh()
