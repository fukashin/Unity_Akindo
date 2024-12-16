using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<NormalItem> 通常アイテムリスト;  // 通常アイテム
    public List<EquipItem> 装備アイテムリスト;   // 装備アイテム
}