# QA

## Build DLL
```sh
git checkout -- Assets/Scripts/
git checkout -- Assets/Scripts.meta
/Applications/Unity/Unity.app/Contents/Frameworks/Mono/bin/gmcs -target:library -out:Assets/Resources/Assembly-CSharp.bytes -debug- -recurse:*.cs -reference:/Applications/Unity/Unity.app/Contents/Frameworks/Managed/UnityEngine.dll,/Applications/Unity/Unity.app/Contents/UnityExtensions/Unity/GUISystem/UnityEngine.UI.dll,/Applications/Unity/Unity.app/Contents/Frameworks/Managed/UnityEditor.dll
```

## Build apk
```sh
rm -rf Assets/Scripts
rm -rf Assets/Scripts.meta
rm -rf Build
mkdir Build
Unity -logFile "$WORKSPACE/unity3d_editor.log"  -executeMethod AutoBuilder.PerformAndroidBuild -quit -batchmode 
```

## HotFix
1. 重打资源
2. 使用publisher工具上传差异到center服务器
3. 在OSS上新建版本目录，上传新资源
4. 在gm管理工具上更改资源版本号
