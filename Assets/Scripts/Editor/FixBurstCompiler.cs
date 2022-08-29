using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace MizuKiri.Editor {
    public class FixBurstCompiler : IPreprocessBuildWithReport {
        const string ANDROID_NDK_ROOT = "ANDROID_NDK_ROOT";

        public int callbackOrder { get { return 0; } }

        string ndkRoot => Path.Combine(BuildPipeline.GetPlaybackEngineDirectory(BuildTarget.Android, BuildOptions.None), "NDK");

        public void OnPreprocessBuild(BuildReport report) {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(ANDROID_NDK_ROOT))) {
                Environment.SetEnvironmentVariable(ANDROID_NDK_ROOT, ndkRoot);
            }
        }
    }
}