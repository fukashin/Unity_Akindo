using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IDManager))]
public class IDManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        IDManager idManager = Resources.Load<IDManager>("IDManager");

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
            Debug.Log("IDリセットボタンがクリックされました。");
            idManager.ResetID();  // IDをリセット
            Debug.Log("アイテムのIDがリセットされました。");
            EditorUtility.SetDirty(idManager);  // IDManagerオブジェクトをエディタに更新
            Debug.Log("エディタに更新しました。");
            AssetDatabase.SaveAssets();  // アセットを保存して変更を反映
            Debug.Log("アセットを保存して、変更を反映しました。");
        }

        // デフォルトのインスペクタを表示
        base.OnInspectorGUI();
    }
}
