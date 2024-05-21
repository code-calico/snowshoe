using Godot;

public static class ResolutionOptions {
    
    static readonly Vector2I[] arr = {
        new Vector2I(3840,2160),
        new Vector2I(3440,1440),
        new Vector2I(2560,1600),
        new Vector2I(2560,1440),
        new Vector2I(2560,1080),
        new Vector2I(2048,1536),
        new Vector2I(2048,1152),
        new Vector2I(1920,1200),
        new Vector2I(1920,1080),
        new Vector2I(1680,1050),
        new Vector2I(1600,1200),
        new Vector2I(1600,900),
        new Vector2I(1536,864),
        new Vector2I(1440,900),
        new Vector2I(1366,768),
        new Vector2I(1360,768),
        new Vector2I(1280,1024),
        new Vector2I(1280,800),
        new Vector2I(1280,720),
        new Vector2I(1024,768),
        new Vector2I(960,540)
    };

    public static Vector2I[] GetArray() => arr;
    
    public static string[] GetStringArray() {
        string[] strArr = new string[arr.Length];
        for (int i = 0; i < arr.Length; i++) {
            strArr[i] = arr[i].X.ToString() + "x" + arr[i].Y.ToString();
        }
        return strArr;
    }
}