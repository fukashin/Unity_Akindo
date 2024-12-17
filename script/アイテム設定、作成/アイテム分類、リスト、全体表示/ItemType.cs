using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewItemType", menuName = "Inventory/大分類")]
public class ItemType : ScriptableObject
{
    public string 大分類;              // 大分類（例: 武器、消耗品、素材）
}