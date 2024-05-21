using Godot;

public static class FullscreenOptions {
    
    static readonly DisplayServer.WindowMode[] arr = {
        DisplayServer.WindowMode.Windowed,
        DisplayServer.WindowMode.Fullscreen,
        DisplayServer.WindowMode.ExclusiveFullscreen
    };

    public static DisplayServer.WindowMode[] GetArray() => arr;
    
    public static string[] GetStringArray() {
        string[] strArr = new string[arr.Length];
        for (int i = 0; i < arr.Length; i++) {
            strArr[i] = arr[i].ToString();
        }
        return strArr;
    }
}