using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewItemType", menuName = "Inventory/小分類")]
public class ItemType2 : ScriptableObject
{
    public ItemType 大分類;              // 大分類（例: 武器、消耗品、素材）
    public string 小分類;              // 小分類（例: 武器、消耗品、素材）
}