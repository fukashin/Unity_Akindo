using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NCDataGenerator : EditorWindow
{
    [MenuItem("Tools/Generate Random NCData")]
    public static void ShowWindow()
    {
        GetWindow<NCDataGenerator>("NCData Generator");
    }

    private static List<string> nameList = new List<string>
    {
        "アリス", "ボブ", "チャーリー", "デイヴィッド", "エマ", "フランク", "グレース", "ハンナ", "イヴ", "ジャック"
    };

    private void OnGUI()
    {
        GUILayout.Label("ランダムNCData生成", EditorStyles.boldLabel);

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
        int randomIndex = Random.Range(0, nameList.Count);
        newNCData.NCName = nameList[randomIndex];  // ランダムなキャラ名を設定
        newNCData.maxHP = Random.Range(50, 200);
        newNCData.currentHP = Random.Range(10, newNCData.maxHP);
        newNCData.attackPower = Random.Range(5, 20);
        newNCData.defensePower = Random.Range(5, 20);
        newNCData.agility = Random.Range(1, 10);
        newNCData.level = 1;
        newNCData.baseXP = 100;
        newNCData.growthRate = Random.Range(1.1f, 1.5f);
        newNCData.skills = new List<string>(); // 空のリスト
        newNCData.inventory = new List<BaseItem>(); // 空のリスト

        // ファイル名にキャラ名を使用
        string fileName = newNCData.NCName.Replace(" ", "_"); // 空白があればアンダースコアに置き換え（ファイル名として適切な形式に）
                                                              // フォルダが存在しない場合に作成
        if (!System.IO.Directory.Exists("Assets/NCData"))
        {
            System.IO.Directory.CreateDirectory("Assets/NCData");
        }

        // ランダムなキャラクター名をファイル名として設定
        string path = $"Assets/NCData/{newNCData.NCName}.asset";

        // ScriptableObjectをアセットとして保存
        AssetDatabase.CreateAsset(newNCData, path);
        AssetDatabase.SaveAssets();

        // 完了メッセージ
        Debug.Log($"ランダムなNCDataを生成しました: {path}");
    }
}
