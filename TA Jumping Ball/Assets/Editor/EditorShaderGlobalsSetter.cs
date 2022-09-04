using UnityEngine;
using UnityEditor;

public class EditorShaderGlobalsSetter : EditorWindow
{
    Color worldColor1;
    Color worldColor2;
    Color backgroundColor;

    [MenuItem("Special/Shader Globals Setter")]
    private static void Init()
    {
        EditorShaderGlobalsSetter window = (EditorShaderGlobalsSetter)GetWindow(typeof(EditorShaderGlobalsSetter));
        window.Show();
    }

    private void OnGUI()
    {
        worldColor1 = EditorGUILayout.ColorField("World Color 1", worldColor1);
        worldColor2 = EditorGUILayout.ColorField("World Color 2", worldColor2);
        backgroundColor = EditorGUILayout.ColorField("Background Color", backgroundColor);

        if (GUILayout.Button("Set World Colors"))
        {
            ShaderGlobalsSetter.SetWorldColors(worldColor1, worldColor2, backgroundColor);
        }
    }
}