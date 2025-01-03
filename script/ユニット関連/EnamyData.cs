using UnityEngine;
using System.Collections.Generic;
using soubiSystem;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "戦闘/敵モンスター情報")]
public class EnemyData : UnitData
{
    [Header("基本情報")]
    [Tooltip("モンスター名")]
    public string enemyName;               // 敵の名前
    [Tooltip("HP")]
    public int health;                     // 敵のHP
    [Tooltip("倒したときの経験値")]
    public int experiencePoints;           // 倒したときの経験値
    [Tooltip("画像")]
    public Sprite enemyImage;              // 敵の画像（スプライト）
    [Tooltip("俊敏性")]
    public int agility;                    // 敵の俊敏性

    private void OnValidate()
    {
        SetUnitEnemyData();
    }

    public void SetUnitEnemyData()
    {
        Unitname = enemyName; // 基底クラスのUnitnameにモンスター名を設定
        HP = health;           //基底クラスのHPにモンスターの体力を設定
        Unitagility = agility;    //基底クラスのアジリティに、モンスターのアジリティを設定
        Sprite Icon = enemyImage;
}

    public AttackRange EnemyAttackRange; //攻撃範囲

    public override AttackRange GetAttackRange()
    {
        return EnemyAttackRange;
    }

    [Header("戦利品リスト")]
    public List<LootItem> lootItems;       // 戦利品のリスト

    [System.Serializable]
    public class LootItem
    {
        [Tooltip("ドロップするアイテム")]
        public BaseItem item;               // ドロップするアイテム（BaseItemのスクリプタブルオブジェクト）
        public int quantity;                // 戦利品の個数
        [Range(0, 100)] public float dropRate;  // 戦利品のドロップ率（％）
    }
}
