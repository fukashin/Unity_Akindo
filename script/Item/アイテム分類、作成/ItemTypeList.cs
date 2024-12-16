using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewItemTypeList", menuName = "Inventory/ItemTypeList")]
public class ItemTypeList : ScriptableObject
{
    public List<ItemType> 種別リスト; // 大分類・小分類を含むItemTypeのリスト
}
