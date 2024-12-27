using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public InventoryData inventoryData; // 所持品、倉庫、陳列棚を管理するデータ
    private SaveManager saveManager;   // セーブやロードを管理するクラス
    public ItemDatabase itemDatabase; // アイテムデータベース

    void Start()
    {
        saveManager = FindFirstObjectByType<SaveManager>();

        // 保存されているインベントリデータをロードして復元
        inventoryData = saveManager.LoadInventory();
    }

    // 所持品にアイテムを追加する
    public void AddItemToInventory(int itemID, int quantity)
    {
        AddItemToList(inventoryData.所持品, itemID, quantity);
    }

    // 倉庫にアイテムを追加する
    public void AddItemToStorage(int itemID, int quantity)
    {
        AddItemToList(inventoryData.倉庫, itemID, quantity);
    }

    // 陳列棚にアイテムを追加する
    public void AddItemToShelf(int itemID, int quantity)
    {
        AddItemToList(inventoryData.陳列棚, itemID, quantity);
    }

    // リスト間でアイテムを移動する
    public void MoveItem(List<NormalItem> fromList, List<NormalItem> toList, int itemID, int quantity)
    {
        var item = fromList.Find(x => x.ID == itemID); // ItemIDからIDに変更
        if (item != null && item.所持数 >= quantity)
        {
            item.所持数 -= quantity;
            AddItemToList(toList, itemID, quantity);

            if (item.所持数 == 0)
                fromList.Remove(item);

            Debug.Log($"アイテムを移動しました: ID = {itemID}, 数量 = {quantity}");
            saveManager.SaveInventory(inventoryData);
        }
        else
        {
            Debug.LogWarning("移動に失敗: アイテムが不足しています。");
        }
    }

    // 共通のアイテム追加処理
    // 共通のアイテム追加処理
    public void AddItemToList(List<NormalItem> list, int itemID, int quantity)
    {
        var item = list.Find(x => x.ID == itemID); // アイテムをIDで検索
        if (item != null)
        {
            item.所持数 += quantity; // 所持数を追加
        }
        else
        {
            // リストごとのアイテム取得処理を変更
            BaseItem newItem = null;

            // 適切なリストからアイテムを取得
            if (list == inventoryData.所持品)
            {
                newItem = itemDatabase.所持品アイテムリスト.Find(x => x.ID == itemID);
            }
            else if (list == inventoryData.倉庫)
            {
                newItem = itemDatabase.倉庫アイテムリスト.Find(x => x.ID == itemID);
            }
            else if (list == inventoryData.陳列棚)
            {
                newItem = itemDatabase.陳列棚アイテムリスト.Find(x => x.ID == itemID);
            }

            // アイテムが見つかった場合、新しいアイテムをリストに追加
            if (newItem != null)
            {
                NormalItem normalItem = new NormalItem { ID = newItem.ID, 所持数 = quantity }; // 新しいアイテムを所持数付きで追加
                list.Add(normalItem);
            }

            // UI更新処理
            UpdateUI(list);
            Debug.Log($"アイテムを追加しました: ID = {itemID}, 数量 = {quantity}");
            saveManager.SaveInventory(inventoryData); // セーブ処理
        }
    }



    // UIの更新メソッド（仮）
    private void UpdateUI(List<NormalItem> list)
    {
        // UI更新処理をここに追加
    }
}

// 所持品、倉庫、陳列棚を管理するデータクラス
[System.Serializable]
public class InventoryData
{
    public List<NormalItem> 所持品 = new List<NormalItem>();
    public List<NormalItem> 倉庫 = new List<NormalItem>();
    public List<NormalItem> 陳列棚 = new List<NormalItem>();
}
