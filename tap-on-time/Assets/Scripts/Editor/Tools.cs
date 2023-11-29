using UnityEditor;
using UnityEngine;

namespace Editor
{
    public abstract class Tools
    {
        [MenuItem("Tools/Game state/Clear")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        [MenuItem("Tools/Screenshot")]
        public static void MakeScreenshot()
        {
            ScreenCapture.CaptureScreenshot("fast_tap_.png");
        }
    }
}