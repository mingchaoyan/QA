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
