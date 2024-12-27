using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(NormalItem))]
public class NormalItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        NormalItem normalItem = (NormalItem)target;

        // 初期化時にIDが0なら、IDManagerからIDを取得
        if (normalItem.ID == 0)
        {
            IDManager idManager = Resources.Load<IDManager>("IDManager");
            if (idManager != null)
            {
                normalItem.ID = idManager.GetNewID();

                idManager.AddItem(normalItem);  // アイテムをIDManagerに追加
                EditorUtility.SetDirty(normalItem);  // 変更をエディタに通知
                Debug.Log("変更がエディタに通知されました。");
            }
        }

        // IDの表示
        normalItem.ID = EditorGUILayout.IntField("iD", normalItem.ID);

        // 他のフィールドの表示
        normalItem.商品名 = EditorGUILayout.TextField("商品名", normalItem.商品名);

        // 他のフィールドの表示
        EditorGUILayout.LabelField("説明欄");
        normalItem.説明欄 = EditorGUILayout.TextArea(normalItem.説明欄, EditorStyles.textArea, GUILayout.Height(60)); 

        // アイテムタイプの選択
        normalItem.アイテムタイプ = (ItemType)EditorGUILayout.EnumPopup("アイテムタイプ", normalItem.アイテムタイプ);

        // アイテムタイプに応じた処理（例えば装備品に特化するなど）
        if (normalItem.アイテムタイプ == ItemType.装備)
        {
            // 装備品専用の設定を表示する
        }

        normalItem.相場価格 = EditorGUILayout.IntField("相場価格", normalItem.相場価格);
        normalItem.需要 = EditorGUILayout.IntField("需要", normalItem.需要);
        normalItem.商品画像 = (Sprite)EditorGUILayout.ObjectField("商品画像", normalItem.商品画像, typeof(Sprite), false);

        // 必要素材リストの表示
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

        // 素材追加ボタン
        if (GUILayout.Button("素材を追加"))
        {
            normalItem.必要素材.Add(new NormalItem.MaterialRequirement());
        }

        // 変更があった場合、インスペクターに通知
        if (GUI.changed)
        {
            EditorUtility.SetDirty(normalItem);
        }
    }
}
