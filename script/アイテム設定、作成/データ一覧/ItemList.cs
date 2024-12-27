using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<BaseItem> 倉庫アイテムリスト = new List<BaseItem>();
    public List<BaseItem> 所持品アイテムリスト = new List<BaseItem>();
    public List<BaseItem> 陳列棚アイテムリスト = new List<BaseItem>();

    // アイテムIDで検索
    public BaseItem GetItemByID(int itemID)
    {
        return 所持品アイテムリスト.Find(x => x.ID == itemID) ??
               倉庫アイテムリスト.Find(x => x.ID == itemID) ??
               陳列棚アイテムリスト.Find(x => x.ID == itemID);
    }

    // カテゴリ名でアイテムリストを取得するメソッド
    public List<BaseItem> GetItemsByCategory(string category)
    {
        switch (category)
        {
            case "所持品":
                return 所持品アイテムリスト;
            case "倉庫":
                return 倉庫アイテムリスト;
            case "陳列棚":
                return 陳列棚アイテムリスト;
            default:
                Debug.LogError($"指定されたカテゴリ '{category}' は存在しません。");
                return new List<BaseItem>();
        }
    }
}
