using UnityEngine;

public static class ShaderGlobalsSetter
{
    public static void SetWorldColors(Color color1, Color color2, Color backgroundColor)
    {
        Shader.SetGlobalColor("_WorldColor1", color1);
        Shader.SetGlobalColor("_WorldColor2", color2);
        Camera.main.backgroundColor = backgroundColor;
    }

    public static void SetWorldColors(LevelColors colors)
    {
        Shader.SetGlobalColor("_WorldColor1", colors.worldColor1);
        Shader.SetGlobalColor("_WorldColor2", colors.worldColor2);
        Camera.main.backgroundColor = colors.backgroundColor;
    }
}