using UnityEngine;
using System.Collections.Generic;
using soubiSystem;

[CreateAssetMenu(fileName = "NCData", menuName = "Scriptable Objects/NCData")]  //NC　＝ NPC Characterの略
public class NCData : UnitData
{
    [Header("キャラクター基本情報")]
    public string NCName; // キャラクター名　

    //基底クラスの情報管理
    private void OnValidate()
    {
        SetUnitNCData();
    }

    public void SetUnitNCData()
    {
        Unitname = NCName; // 基底クラスのUnitnameにキャラクターの名前を設定
        HP = currentHP;           //基底クラスのHPにキャラクターの現在の体力を設定
        Unitagility = agility;    //基底クラスのアジリティに、キャラクターデータのアジリティを設定
    }

    [Header("ステータス")]
    [Tooltip("キャラクターの最大HP")]
    public int maxHP;            // 最大HP
    [Tooltip("初期HP")]
    public int currentHP;        //初期HP

    [HideInInspector]
    public int maxMP; //マジックポイント 後々使うかも

    [Tooltip("キャラクターの攻撃力")]
    public int attackPower;      // 攻撃力
    [Tooltip("キャラクターの防御力")]
    public int defensePower;     // 防御力
    [Tooltip("キャラクターの俊敏性")]
    public int agility;      // 俊敏性

    [Header("その他の設定")]
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

    [Header("装備品一覧")]
    public EquipItem 武器;
    public EquipItem 頭装備;
    public EquipItem 胴体装備;
    public EquipItem 脚装備;
    public EquipItem 手装備;
    public EquipItem 装飾品1;
    public EquipItem 装飾品2;

    [HideInInspector] public int totalAttack;
    [HideInInspector] public int totalDefense;
    [HideInInspector] public int totalHP;
    [HideInInspector] public int totalMP;

    // カテゴリごとの装備品を取得
    public EquipItem GetEquippedItemByCategory(EquipItem.EquipCategory category)
    {
        switch (category)
        {
            case EquipItem.EquipCategory.武器:
                return 武器;
            case EquipItem.EquipCategory.頭装備:
                return 頭装備;
            case EquipItem.EquipCategory.胴体装備:
                return 胴体装備;
            case EquipItem.EquipCategory.脚装備:
                return 脚装備;
            case EquipItem.EquipCategory.手装備:
                return 手装備;
            case EquipItem.EquipCategory.装飾品1:
                return 装飾品1;
            case EquipItem.EquipCategory.装飾品2:
                return 装飾品2;
            default:
                return null; // カテゴリが一致しない場合はnull
        }
    }

    // 装備を変更するメソッド
    public void EquipNewItem(EquipItem newItem)
    {
        switch (newItem.category)
        {
            case EquipItem.EquipCategory.武器:
                武器 = newItem; // 武器を装備
                break;
            case EquipItem.EquipCategory.頭装備:
                頭装備 = newItem; // 頭装備を装備
                break;
            case EquipItem.EquipCategory.胴体装備:
                胴体装備 = newItem; // 胴体装備を装備
                break;
            case EquipItem.EquipCategory.脚装備:
                脚装備 = newItem; // 脚装備を装備
                break;
            case EquipItem.EquipCategory.手装備:
                手装備 = newItem; // 手装備を装備
                break;
            case EquipItem.EquipCategory.装飾品1:
                装飾品1 = newItem; // 装飾品1を装備
                break;
            case EquipItem.EquipCategory.装飾品2:
                装飾品2 = newItem; // 装飾品2を装備
                break;
        }
    }

    // 装備品の影響をステータスに反映させる
    public void UpdateStatsFromEquipments()
    {
        totalAttack = attackPower;
        totalDefense = defensePower;
        totalHP = maxHP;
        totalMP = maxMP;

        // 各装備品からステータスの影響を加算
        if (武器 != null)
        {
            totalAttack += 武器.攻撃力;
            totalDefense += 武器.防御力;
            totalHP += 武器.体力ボーナス;
            totalMP += 武器.魔法力;
        }

        if (頭装備 != null)
        {
            totalAttack += 頭装備.攻撃力;
            totalDefense += 頭装備.防御力;
            totalHP += 頭装備.体力ボーナス;
            totalMP += 頭装備.魔法力;
        }

        if (胴体装備 != null)
        {
            totalAttack += 胴体装備.攻撃力;
            totalDefense += 胴体装備.防御力;
            totalHP += 胴体装備.体力ボーナス;
            totalMP += 胴体装備.魔法力;
        }

        if (脚装備 != null)
        {
            totalAttack += 脚装備.攻撃力;
            totalDefense += 脚装備.防御力;
            totalHP += 脚装備.体力ボーナス;
            totalMP += 脚装備.魔法力;
        }

        if (手装備 != null)
        {
            totalAttack += 手装備.攻撃力;
            totalDefense += 手装備.防御力;
            totalHP += 手装備.体力ボーナス;
            totalMP += 手装備.魔法力;
        }

        if (装飾品1 != null)
        {
            totalAttack += 装飾品1.攻撃力;
            totalDefense += 装飾品1.防御力;
            totalHP += 装飾品1.体力ボーナス;
            totalMP += 装飾品1.魔法力;
        }

        if (装飾品2 != null)
        {
            totalAttack += 装飾品2.攻撃力;
            totalDefense += 装飾品2.防御力;
            totalHP += 装飾品2.体力ボーナス;
            totalMP += 装飾品2.魔法力;
        }
    }

    // 次のレベルに必要な経験値を計算するメソッド
    public int GetXPToNextLevel()
    {
        return Mathf.FloorToInt(baseXP * Mathf.Pow(growthRate, level - 1));
    }

    public void AddExperience(int xpAmount)
    {
        currentXP += xpAmount;
        while (currentXP >= GetXPToNextLevel()) // レベルアップ条件
        {
            currentXP -= GetXPToNextLevel();
            level++;
            // レベルアップ時にステータスを上げるなどの処理を追加できます
            Debug.Log($"{NCName} leveled up! Now level {level}");
        }
    }

    public int MaintenanceCost
    {
        get
        {
            // レベル、攻撃力、防御力、俊敏性を維持費とする
            return (level * 50) + (attackPower * 5) + (defensePower * 4) + (agility * 3);
        }
    }

}
