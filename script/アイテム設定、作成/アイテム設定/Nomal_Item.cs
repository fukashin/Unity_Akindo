using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewNormalItem", menuName = "Inventory/通常アイテム")]
public class NormalItem : BaseItem
{
    // NormalItem で特に何も追加しない場合でも、ItemType を通常に設定する
    private void OnEnable()
    {
        アイテムタイプ = ItemType.通常;  // アイテムタイプを「通常」に設定
    }
}
