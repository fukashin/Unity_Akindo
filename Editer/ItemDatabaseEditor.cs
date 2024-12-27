using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // デフォルトのインスペクターを表示
        DrawDefaultInspector();

        // 保存ボタンを追加
        if (GUILayout.Button("アイテムデータベース セーブ"))
        {
            // 保存処理を実行
            SaveItemDatabase();
        }
    }

    private void SaveItemDatabase()
    {
        // アイテムデータを保存する処理
        ItemDatabase itemDatabase = (ItemDatabase)target;
        EditorUtility.SetDirty(itemDatabase);  // アイテムデータを「Dirty」状態にする（変更があったことをUnityに通知）
        AssetDatabase.SaveAssets();  // アセットを保存
        Debug.Log("セーブ完了！");
    }
}
