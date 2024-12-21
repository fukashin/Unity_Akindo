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
        normalItem.小分類 = (ItemType2)EditorGUILayout.ObjectField("小分類", normalItem.小分類, typeof(ItemType2), false);

        if (normalItem.小分類 != null)
        {
            normalItem.大分類 = normalItem.小分類.大分類;
        }

        EditorGUI.BeginDisabledGroup(true);
        normalItem.大分類 = (ItemType)EditorGUILayout.ObjectField("大分類", normalItem.大分類, typeof(ItemType), false);
        EditorGUI.EndDisabledGroup();

        normalItem.相場 = EditorGUILayout.IntField("相場", normalItem.相場);
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

                // IDManagerからIDを削除
                /*
                IDManager idManager = Object.FindFirstObjectByType<IDManager>();
                if (idManager != null)
                {
                    idManager.RemoveItemID(normalItem);
                    #if UNITY_EDITOR
                    EditorUtility.SetDirty(idManager);
                    #endif
                }
                */
            }
            EditorGUILayout.EndHorizontal();
        }

        // 素材追加ボタン
        if (GUILayout.Button("素材を追加"))
        {
            normalItem.必要素材.Add(new NormalItem.MaterialRequirement());
        }

        // IDリセットボタンを追加
        if (GUILayout.Button("IDリセット"))
        {
            Debug.Log("IDリセットボタンがクリックされました。");

            IDManager idManager = Resources.Load<IDManager>("IDManager");
            //IDManager idManager = Object.FindFirstObjectByType<IDManager>();
            if (idManager != null)
            {
                Debug.Log("IDManagerが見つかりました。");

                idManager.RemoveItemID(normalItem);  // 現在のIDをIDManagerから削除
                EditorUtility.SetDirty(normalItem);

                normalItem.ResetID();  // アイテムのIDをリセット
                Debug.Log("アイテムのIDがリセットされました。");

                normalItem.ID = idManager.GetNewID();  // 新しいIDを設定
                Debug.Log("新しいIDが設定されました: " + normalItem.ID);

                EditorUtility.SetDirty(normalItem);  // 変更をエディタに通知
                Debug.Log("変更がエディタに通知されました。");

                // IDリセット後にログ出力
                Debug.Log("個別にIDがリセットされ、再割り当てされました。");
            }
            else
            {
                Debug.LogError("IDManagerが見つかりませんでした。");
            }
        }

        // 変更があった場合、インスペクターに通知
        if (GUI.changed)
        {
            EditorUtility.SetDirty(normalItem);
        }
    }
}
