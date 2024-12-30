using UnityEngine;
using System.Collections.Generic;

namespace soubiSystem  // 装備関連の名前空間に変更
{
    [CreateAssetMenu(fileName = "NewEquipItem", menuName = "Inventory/装備アイテム")]
    public class EquipItem : BaseItem
    {
        public EquipCategory category;
        public int 攻撃力;              // 装備の攻撃力
        public int 防御力;              // 装備の防御力
        public int 耐久力;              // 装備の耐久力
        public int 魔法力;              // 装備の魔法力
        public int 体力ボーナス;        // 装備で体力増やすときの値

        //ItemType を通常に設定する
        private void OnEnable()
        {
            アイテムタイプ = ItemType.装備;  // アイテムタイプを「装備」に設定
        }

        public enum EquipCategory
        {
            武器,
            頭装備,
            胴体装備,
            脚装備,
            手装備,
            装飾品1,
            装飾品2
        }
    }
}
