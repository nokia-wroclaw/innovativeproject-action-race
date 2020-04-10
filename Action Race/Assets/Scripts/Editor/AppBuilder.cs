using UnityEditor;

public class AppBuilder
{
    public static void Build()
    {
        string[] scenes = { "Assets/Scenes/MainMenuScene.unity", "Assets/Scenes/Small Map.unity" };
        BuildPipeline.BuildPlayer(scenes, "Build/WebGL", BuildTarget.WebGL, BuildOptions.None);
    }
}