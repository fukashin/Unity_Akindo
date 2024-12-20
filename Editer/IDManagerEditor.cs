using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IDManager))]
public class IDManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        IDManager idManager = (IDManager)target;

        // アイテムリストの表示
        GUILayout.Label("アイテム一覧");

        foreach (var item in idManager.アイテムリスト)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"ID: {item.ID}, 商品名: {item.商品名}");
            GUILayout.EndHorizontal();
        }

        // リセットボタンの作成
        if (GUILayout.Button("IDをリセット"))
        {
            idManager.ResetID();  // IDをリセット
            EditorUtility.SetDirty(idManager);  // IDManagerオブジェクトをエディタに更新
            AssetDatabase.SaveAssets();  // アセットを保存して変更を反映
        }

        // デフォルトのインスペクタを表示
        base.OnInspectorGUI();
    }
}
