using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewNormalItem", menuName = "Inventory/通常アイテム")]
public class NormalItem : BaseItem
{
    [System.Serializable]
    public class MaterialRequirement
    {
        public BaseItem 素材;      // 素材アイテム
        public int 必要数;         // 素材必要数
    }

    public List<MaterialRequirement> 必要素材; // 必要な素材リスト
}
