using UnityEngine;
using System.Collections.Generic;

// 所持しているアイテムの情報を管理するクラス
[System.Serializable]
public class OwnedItem
{
    public int ItemID;      // マスターデータのIDを参照してアイテムを識別
    public int 所持数;       // プレイヤーが所持しているアイテムの数
}

// プレイヤーのインベントリ（所持アイテム）を管理するクラス
public class PlayerInventory : MonoBehaviour
{
    public List<OwnedItem> 所持アイテムリスト; // プレイヤーが所持しているアイテムのリスト
    public ItemDatabase itemDatabase;          // アイテムデータベース（全アイテムの情報が含まれる）

    // --- アイテムを追加するメソッド ---
    // 引数: itemID = 追加するアイテムのID, quantity = 追加する数
    public void AddItem(int itemID, int quantity)
    {
        // 所持アイテムリストの中から、指定されたitemIDのアイテムを探す
        var item = 所持アイテムリスト.Find(x => x.ItemID == itemID);
        
        if (item != null) // すでにアイテムを持っている場合
        {
            item.所持数 += quantity; // 所持数を加算する
        }
        else // 持っていないアイテムの場合
        {
            // 新しいアイテムをリストに追加する
            所持アイテムリスト.Add(new OwnedItem { ItemID = itemID, 所持数 = quantity });
        }
    }

    // --- マスターデータからアイテムの詳細情報を取得するメソッド ---
    // 引数: itemID = 取得したいアイテムのID
    public Item GetItemInfo(int itemID)
    {
        // アイテムデータベースの中からIDが一致するアイテムを探して返す
        return itemDatabase.通常アイテムリスト.Find(x => x.ID == itemID);
    }
}
