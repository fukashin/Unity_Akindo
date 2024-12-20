using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OwnedItem
{
    public int ItemID;    // アイテムの識別ID
    public int 所持数;     // 所持している数
}

// 所持アイテムリストをラップするクラス
[System.Serializable]
public class InventoryData
{
    public List<OwnedItem> 所持アイテムリスト = new List<OwnedItem>();
}
