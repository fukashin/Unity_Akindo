using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public InventoryData inventoryData; // プレイヤーが所持しているアイテムリスト
    private SaveManager saveManager;   // セーブやロードを管理するクラス


    // Unityがゲーム開始時に自動で呼び出すメソッド
    void Start()
    {
        // SaveManagerスクリプトをシーン内から探して取得
        saveManager = FindFirstObjectByType<SaveManager>();


        // 保存されているインベントリデータをロードして復元
        inventoryData = saveManager.LoadInventory();

    }


    // アイテムを追加するメソッド
    // 引数: itemID（アイテムのID）, quantity（追加する数量）
    public void AddItem(int itemID, int quantity)
    {
        // 所持アイテムリストから指定したIDのアイテムを検索
        var item = inventoryData.所持アイテムリスト.Find(x => x.ItemID == itemID);

        if (item != null) // すでにアイテムを所持している場合
        {
            item.所持数 += quantity; // 所持数を増加
        }
        else // 所持していない新しいアイテムの場合
        {
            // 新しいアイテムをリストに追加
            inventoryData.所持アイテムリスト.Add(new OwnedItem { ItemID = itemID, 所持数 = quantity });
        }

        

        // デバッグログで現在のインベントリの状態を表示
        Debug.Log($"アイテムを追加しました: ID = {itemID}, 数量 = {quantity}");
        saveManager.SaveInventory(inventoryData);
    }
}
