using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewEquipItem", menuName = "Inventory/装備アイテム")]
public class EquipItem : NormalItem
{
    public int 攻撃力;              // 装備の攻撃力
    public int 防御力;              // 装備の防御力
    public int 耐久力;              // 装備の耐久力

}
