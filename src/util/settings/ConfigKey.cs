using Godot;

public class ConfigKey { 
	private string sectionKey;
	private string key;
	private Variant defaultValue;

	public ConfigKey(string sectionKey, string key, Variant defaultValue) {
		this.sectionKey = sectionKey;
		this.key = key;
		this.defaultValue = defaultValue;
	}

	public string GetSectionKey() => sectionKey;
	public string GetKey() => key;
	public Variant GetDefault() => defaultValue;
}
