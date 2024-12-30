using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "戦闘/敵モンスター情報")]
public class EnemyData : ScriptableObject
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

    [Header("戦利品リスト")]
    public List<LootItem> lootItems;       // 戦利品のリスト
}

[System.Serializable]
public class LootItem
{
    [Tooltip("ドロップするアイテム")]
    public BaseItem item;               // ドロップするアイテム（BaseItemのスクリプタブルオブジェクト）
    public int quantity;                // 戦利品の個数
    [Range(0, 100)] public float dropRate;  // 戦利品のドロップ率（％）
}

