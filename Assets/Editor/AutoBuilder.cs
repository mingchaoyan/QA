using UnityEngine;
using System.Collections;
using UnityEditor;

public static class AutoBuilder {
	static BuildOptions AutoBuildOption = BuildOptions.None;

	static string GetProjectName() {
		string[] s = Application.dataPath.Split('/');
		return s[s.Length-2];
	}

	static string[] GetScenePaths() {
		var scenes = new string[EditorBuildSettings.scenes.Length];
		for(int i = 0; i < scenes.Length; ++i) {
			var scene = EditorBuildSettings.scenes[i];
			if(scene.enabled) {
				scenes[i] = scene.path;
			}
			
		}
		return scenes;
	}

	[MenuItem("File/AutoBuilder/Android")]
	static void PerformAndroidBuild() {
		Debug.Log("~/GitHub/QA/Build/" + GetProjectName() + ".apk");
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Build/" + GetProjectName() + ".apk", BuildTarget.Android, AutoBuildOption );
	}
}
