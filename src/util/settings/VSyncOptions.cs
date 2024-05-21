using Godot;

public static class VSyncOptions {
    
    static readonly DisplayServer.VSyncMode[] arr = {
        DisplayServer.VSyncMode.Enabled,
        DisplayServer.VSyncMode.Disabled,
        DisplayServer.VSyncMode.Mailbox,
        DisplayServer.VSyncMode.Adaptive
    };

    public static DisplayServer.VSyncMode[] GetArray() => arr;

    public static string[] GetStringArray() {
        string[] strArr = new string[arr.Length];
        for (int i = 0; i < arr.Length; i++) {
            strArr[i] = arr[i].ToString();
        }
        return strArr;
    }
}