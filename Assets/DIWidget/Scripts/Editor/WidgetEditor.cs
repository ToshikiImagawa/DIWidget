using System.Reflection;
using DIWidget;
using UnityEditor;
using UnityEngine;

namespace DIWidgetEditor
{
    [CustomEditor(typeof(WidgetBase), true)]
    public class WidgetEditor : Editor
    {
        private static Texture2D _texture;

        private static Texture2D Texture
        {
            get
            {
                if (_texture != null) return _texture;
                var pass = $"{DIWidgetEditorUtility.PackageRelativePath}/Editor Resources/Textures/icon_Widget.psd";
                _texture = AssetDatabase.LoadAssetAtPath(pass, typeof(Texture2D)) as Texture2D;
                return _texture;
            }
        }

        private bool InitIcon { get; set; }

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            return Texture;
        }

        public override void OnInspectorGUI()
        {
            if (!InitIcon)
            {
                var editorGuiUtilityType = typeof(EditorGUIUtility);
                const BindingFlags bindingFlags =
                    BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
                var args = new object[] {target, Texture};
                editorGuiUtilityType.InvokeMember("SetIconForObject", bindingFlags, null, null, args);
                InitIcon = true;
            }

            base.OnInspectorGUI();
        }
    }
}