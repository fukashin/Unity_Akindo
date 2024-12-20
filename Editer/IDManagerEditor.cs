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

        // デフォルトのインスペクタを表示
        base.OnInspectorGUI();
    }
}
