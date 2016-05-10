using UnityEditor;

public static class CreateAssetBundles
{
	[MenuItem("Assets/Build AssetBundles")]
	public static void BuildAllAssetBundles()
	{
		BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.Android);
	}
}
