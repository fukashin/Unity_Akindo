using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("キャラクター基本情報")]
    public string characterName; // キャラクター名

    [Header("ステータス")]
    [Tooltip("キャラクターの最大HP")]
    public int maxHP;            // 最大HP
    [Tooltip("キャラクターの攻撃力")]
    public int attackPower;      // 攻撃力
    [Tooltip("キャラクターの防御力")]
    public int defensePower;     // 防御力

    [Header("その他の設定")]
    [Tooltip("キャラクターの移動速度")]
    public float moveSpeed;      // 移動速度
    [Tooltip("キャラクターのアイコン")]
    public Sprite characterIcon; // キャラクターのアイコン
    [Tooltip("キャラクターのプレハブ")]
    public GameObject prefab;    // キャラクターのプレハブ

    [Header("経験値やレベル")]
    public int level;            // キャラクターのレベル
    [Tooltip("現在の経験値")]
    public int currentXP;        // 現在の経験値
    [Tooltip("初期のレベルアップに必要な経験値")]
    public int baseXP;           // 初期のレベルアップに必要な経験値
    [Tooltip("経験値の成長率")]
    public float growthRate;     // 経験値の成長率（例えば1.2なら20%増加）

    [Header("スキル")]
    public List<string> skills;  // キャラクターが持つスキルのリスト

    [Header("所持品")]
    public List<BaseItem> inventory; // CharacterData内でBaseItemを使用
    public int maxInventorySize = 5; // 最大5種類まで


    [Header("装備品")]
    [Tooltip("武器")]
    public EquipItem weapon;      // 武器
    [Tooltip("頭装備")]
    public EquipItem headGear;    // 頭装備
    [Tooltip("胴体装備")]
    public EquipItem bodyArmor;    // 胴体装備
    [Tooltip("脚装備")]
    public EquipItem leg;     // 脚装備
    [Tooltip("手装備")]
    public EquipItem hands;    // 手装備
    [Tooltip("装飾品１")]
    public EquipItem accessory1;  // 装飾品1
    [Tooltip("装飾品２")]
    public EquipItem accessory2;  // 装飾品2

    // 次のレベルに必要な経験値を計算するメソッド
    public int GetXPToNextLevel()
    {
        return Mathf.FloorToInt(baseXP * Mathf.Pow(growthRate, level - 1));
    }
}
