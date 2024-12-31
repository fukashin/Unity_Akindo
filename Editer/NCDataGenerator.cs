using UnityEngine;
using UnityEditor;

public class NCDataGenerator : EditorWindow
{
    [MenuItem("Tools/Generate Random NCData")]
    public static void ShowWindow()
    {
        GetWindow<NCDataGenerator>("NCData Generator");
    }

    private string fileName = "NewNCData";

    private void OnGUI()
    {
        GUILayout.Label("ランダムNCData生成", EditorStyles.boldLabel);

        fileName = EditorGUILayout.TextField("ファイル名", fileName);

        if (GUILayout.Button("ランダム生成"))
        {
            GenerateRandomNCData();
        }
    }

    private void GenerateRandomNCData()
    {
        // 新しいNCDataを生成
        NCData newNCData = ScriptableObject.CreateInstance<NCData>();

        // ランダムデータの割り当て
        newNCData.NCName = "キャラ" + Random.Range(1, 100);
        newNCData.maxHP = Random.Range(50, 200);
        newNCData.currentHP = Random.Range(10, newNCData.maxHP);
        newNCData.attackPower = Random.Range(5, 20);
        newNCData.defensePower = Random.Range(5, 20);
        newNCData.agility = Random.Range(1, 10);
        newNCData.level = 1;
        newNCData.baseXP = 100;
        newNCData.growthRate = Random.Range(1.1f, 1.5f);
        newNCData.skills = new System.Collections.Generic.List<string>(); //空のリスト
        newNCData.inventory = new System.Collections.Generic.List<BaseItem>(); // 空のリスト

        // ScriptableObjectをアセットとして保存
        string path = $"Assets/NCData/{fileName}.asset";
        AssetDatabase.CreateAsset(newNCData, path);
        AssetDatabase.SaveAssets();

        // 完了メッセージ
        Debug.Log($"ランダムなNCDataを生成しました: {path}");
    }
}
