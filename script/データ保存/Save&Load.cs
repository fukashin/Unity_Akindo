using System.IO;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    private string saveFilePath; // 保存ファイルのパス

    void Awake()
    {
        // プラットフォームに依存しない保存先パス
        saveFilePath = Application.persistentDataPath + "/inventory.json";
    }

    // ########################################## データを保存 ##########################################
    public void SaveInventory(InventoryData inventory)
    {
        // JSON形式に変換
        string json = JsonUtility.ToJson(inventory, true); // 整形して保存

        // ファイルに書き込む
        File.WriteAllText(saveFilePath, json);

        Debug.Log("インベントリデータを保存しました: " + saveFilePath);
    }

    // ########################################## データを読み込む ##########################################
    public InventoryData LoadInventory()
    {
        if (File.Exists(saveFilePath))
        {
            // ファイルからJSON文字列を読み込む
            string json = File.ReadAllText(saveFilePath);

            Debug.Log("読み込まれたJSONデータ: " + json);

            // JSON文字列をインベントリデータに変換
            InventoryData inventory = JsonUtility.FromJson<InventoryData>(json);

            Debug.Log("インベントリデータを読み込みました。");
            return inventory;
        }
        else
        {
            Debug.LogWarning("セーブデータが存在しません。新しいデータを作成します。");
            return new InventoryData(); // データがない場合は空のインベントリを返す
        }
    }
}
