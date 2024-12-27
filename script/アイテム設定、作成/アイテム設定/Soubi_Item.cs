using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewEquipItem", menuName = "Inventory/装備アイテム")]
public class EquipItem : BaseItem
{
    public int 攻撃力;              // 装備の攻撃力
    public int 防御力;              // 装備の防御力
    public int 耐久力;              // 装備の耐久力

    //ItemType を通常に設定する
    private void OnEnable()
    {
        アイテムタイプ = ItemType.装備;  // アイテムタイプを「装備」に設定
    }

}
