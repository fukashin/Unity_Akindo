using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(NormalItem))]
public class NormalItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 対象のNormalItemを取得
        NormalItem normalItem = (NormalItem)target;

        // ID
        normalItem.ID = EditorGUILayout.IntField("iD", normalItem.ID);

        // 商品名入力
        normalItem.商品名 = EditorGUILayout.TextField("商品名", normalItem.商品名);

        // 小分類の選択
        normalItem.小分類 = (ItemType2)EditorGUILayout.ObjectField("小分類", normalItem.小分類, typeof(ItemType2), false);

        // 小分類が選択された場合、大分類を自動で設定
        if (normalItem.小分類 != null)
        {
            normalItem.大分類 = normalItem.小分類.大分類;
        }

        // 大分類（ReadOnlyにする）
        EditorGUI.BeginDisabledGroup(true);
        normalItem.大分類 = (ItemType)EditorGUILayout.ObjectField("大分類", normalItem.大分類, typeof(ItemType), false);
        EditorGUI.EndDisabledGroup();

        // 所持数や価格など他のパラメータ入力
        normalItem.相場 = EditorGUILayout.IntField("相場", normalItem.相場);
        normalItem.需要 = EditorGUILayout.IntField("需要", normalItem.需要);
        normalItem.商品画像 = (Sprite)EditorGUILayout.ObjectField("商品画像", normalItem.商品画像, typeof(Sprite), false);

        // 必要素材リスト
        EditorGUILayout.LabelField("必要素材リスト", EditorStyles.boldLabel);
        if (normalItem.必要素材 == null)
            normalItem.必要素材 = new List<NormalItem.MaterialRequirement>();

        for (int i = 0; i < normalItem.必要素材.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            normalItem.必要素材[i].素材 = (BaseItem)EditorGUILayout.ObjectField("素材", normalItem.必要素材[i].素材, typeof(BaseItem), false);
            normalItem.必要素材[i].必要数 = EditorGUILayout.IntField("必要数", normalItem.必要素材[i].必要数);
            if (GUILayout.Button("削除", GUILayout.Width(60)))
            {
                normalItem.必要素材.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("素材を追加"))
        {
            normalItem.必要素材.Add(new NormalItem.MaterialRequirement());
        }

        // 変更があった場合、保存する
        if (GUI.changed)
        {
            EditorUtility.SetDirty(normalItem);
        }
    }
}
