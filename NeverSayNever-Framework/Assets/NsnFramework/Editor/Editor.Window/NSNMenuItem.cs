using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NeverSayNever.EditorUtilitiy
{
    public class NSNMenuItem
    {
        //[MenuItem("Assets/TextEncode to UTF-8")]
        public static void ChangeTextEncode2_UTF8()
        {
            UnityEngine.Object[] texts = Selection.GetFiltered(typeof(UnityEngine.TextAsset), SelectionMode.DeepAssets);
            foreach (var item in texts)
            {
                Debug.Log(item.name);
                //todo
                // set all text files encode to utf-8
            }
        }

        [MenuItem("Assets/Texture Format/Change Texture Format To ASTC_8x8)")]
        public static void ChangeTexturesToASTC()
        {
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请在编辑状态修改资源！", "确定");
                return;
            }

            var textures = Selection.GetFiltered<Texture>(SelectionMode.DeepAssets);
            if (textures == null)
                return;

            var total = textures.Length;
            if (!EditorUtility.DisplayDialog("格式转换", $"确定转换当前选中目录下的的图片(Num:{total})格式 为 ASTC_8x8 ？ ", "搞快", "算了")
            ) return;

            var index = 0;

            foreach (var tex in textures)
            {
                var texPath = AssetDatabase.GetAssetPath(tex);
                TextureCheckerEditor.FormatTexture(texPath,TextureImporterFormat.ASTC_8x8);
                index += 1;

                EditorUtility.DisplayProgressBar(tex.name, $"正在转换 {tex.name}, 当前进度 {index}/{total}",index / total);
            }
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("Complete", "修改已完成", "确定");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }
    }
}