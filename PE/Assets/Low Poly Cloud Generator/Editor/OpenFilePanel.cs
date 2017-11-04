using UnityEditor;
using UnityEngine;

namespace LowPolyCloudGenerator
{
    public class OpenFilePanel : MonoBehaviour
    {
        /// <summary>
        /// Opens a Windows File Panel.
        /// </summary>
        /// <param name="method"></param>
        public static string Open()
        {
            string t = EditorUtility.SaveFilePanel("Export Cloud", Application.dataPath, "Procedural Cloud.obj", "Obj");

            if (!string.IsNullOrEmpty(t))
            {
                return t;
            }
            else
            {
                Debug.LogError("Export Cancelled");
                return "";
            }
        }
    }
}