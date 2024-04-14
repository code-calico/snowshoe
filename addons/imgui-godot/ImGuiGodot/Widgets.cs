#if GODOT_PC
using Godot;
using ImGuiNET;
using System;
using Vector2 = System.Numerics.Vector2;
using Vector4 = System.Numerics.Vector4;

namespace ImGuiGodot;

public static class Widgets
{
    /// <summary>
    /// Display an interactable SubViewport
    /// </summary>
    /// <remarks>
    /// Be sure to change the SubViewport's <see cref="SubViewport.RenderTargetUpdateMode"/> to <see cref="SubViewport.UpdateMode.Always"/>
    /// </remarks>
    /// <returns>
    /// True if active (mouse hovered)
    /// </returns>
    public static bool SubViewport(SubViewport vp)
    {
        Vector2 vpSize = new(vp.Size.X, vp.Size.Y);
        var pos = ImGui.GetCursorScreenPos();
        var pos_max = new Vector2(pos.X + vpSize.X, pos.Y + vpSize.Y);
        ImGui.GetWindowDrawList().AddImage((IntPtr)vp.GetTexture().GetRid().Id, pos, pos_max);

        ImGui.PushID(vp.NativeInstance);
        ImGui.InvisibleButton("godot_subviewport", vpSize);
        ImGui.PopID();

        if (ImGui.IsItemHovered())
        {
            Internal.State.Instance.Input.CurrentSubViewport = vp;
            Internal.State.Instance.Input.CurrentSubViewportPos = pos;
            return true;
        }
        return false;
    }

    public static void Image(Texture2D tex, Vector2 size)
    {
        Image(tex, size, Vector2.Zero, Vector2.One, Vector4.One, Vector4.Zero);
    }

    public static void Image(Texture2D tex, Vector2 size, Vector2 uv0)
    {
        Image(tex, size, uv0, Vector2.One, Vector4.One, Vector4.Zero);
    }

    public static void Image(Texture2D tex, Vector2 size, Vector2 uv0, Vector2 uv1)
    {
        Image(tex, size, uv0, uv1, Vector4.One, Vector4.Zero);
    }

    public static void Image(Texture2D tex, Vector2 size, Vector2 uv0, Vector2 uv1, Vector4 tint_col)
    {
        Image(tex, size, uv0, uv1, tint_col, Vector4.Zero);
    }

    public static void Image(Texture2D tex, Vector2 size, Vector2 uv0, Vector2 uv1, Vector4 tint_col, Vector4 border_col)
    {
        ImGuiNative.igImage((IntPtr)tex.GetRid().Id, size, uv0, uv1, tint_col, border_col);
    }

    public static void Image(AtlasTexture tex, Vector2 size)
    {
        Image(tex, size, Vector4.One, Vector4.Zero);
    }

    public static void Image(AtlasTexture tex, Vector2 size, Vector4 tint_col)
    {
        Image(tex, size, tint_col, Vector4.Zero);
    }

    public static void Image(AtlasTexture tex, Vector2 size, Vector4 tint_col, Vector4 border_col)
    {
        (Vector2 uv0, Vector2 uv1) = GetAtlasUVs(tex);
        ImGuiNative.igImage((IntPtr)tex.GetRid().Id, size, uv0, uv1, tint_col, border_col);
    }

    public static bool ImageButton(string str_id, Texture2D tex, Vector2 size)
    {
        return ImageButton(str_id, tex, size, Vector2.Zero, Vector2.One, Vector4.Zero, Vector4.One);
    }

    public static bool ImageButton(string str_id, Texture2D tex, Vector2 size, Vector2 uv0)
    {
        return ImageButton(str_id, tex, size, uv0, Vector2.One, Vector4.Zero, Vector4.One);
    }

    public static bool ImageButton(string str_id, Texture2D tex, Vector2 size, Vector2 uv0, Vector2 uv1)
    {
        return ImageButton(str_id, tex, size, uv0, uv1, Vector4.Zero, Vector4.One);
    }

    public static bool ImageButton(string str_id, Texture2D tex, Vector2 size, Vector2 uv0, Vector2 uv1, Vector4 bg_col)
    {
        return ImageButton(str_id, tex, size, uv0, uv1, bg_col, Vector4.One);
    }

    public static bool ImageButton(string str_id, Texture2D tex, Vector2 size, Vector2 uv0, Vector2 uv1, Vector4 bg_col, Vector4 tint_col)
    {
        return ImGui.ImageButton(str_id, (IntPtr)tex.GetRid().Id, size, uv0, uv1, bg_col, tint_col);
    }

    public static bool ImageButton(string str_id, AtlasTexture tex, Vector2 size)
    {
        return ImageButton(str_id, tex, size, Vector4.Zero, Vector4.One);
    }

    public static bool ImageButton(string str_id, AtlasTexture tex, Vector2 size, Vector4 bg_col)
    {
        return ImageButton(str_id, tex, size, bg_col, Vector4.One);
    }

    public static bool ImageButton(string str_id, AtlasTexture tex, Vector2 size, Vector4 bg_col, Vector4 tint_col)
    {
        (Vector2 uv0, Vector2 uv1) = GetAtlasUVs(tex);
        return ImGui.ImageButton(str_id, (IntPtr)tex.GetRid().Id, size, uv0, uv1, bg_col, tint_col);
    }

    private static (Vector2 uv0, Vector2 uv1) GetAtlasUVs(AtlasTexture tex)
    {
        Godot.Vector2 atlasSize = tex.Atlas.GetSize();
        Godot.Vector2 guv0 = tex.Region.Position / atlasSize;
        Godot.Vector2 guv1 = tex.Region.End / atlasSize;
        return (new(guv0.X, guv0.Y), new(guv1.X, guv1.Y));
    }
}
#endif
