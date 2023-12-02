using UnityEditor;
using UnityEngine;

namespace Editor
{
    public abstract class Tools
    {
        [MenuItem("Tools/Screenshot")]
        public static void MakeScreenshot()
        {
            ScreenCapture.CaptureScreenshot("fast_tap_.png");
        }
    }
}