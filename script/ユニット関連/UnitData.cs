using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    public string Unitname;    // 名前（共通のフィールド）
    public int Unitagility;    // アジリティ（共通のフィールド）

    public int HP;            // ヒットポイント（例: HPが0になると死亡）
    public bool IsDead => HP <= 0;  // HPが0以下の場合、死亡と判定

    // 各キャラクターごとの行動メソッド（仮想メソッド）
    public virtual void TakeTurn()
    {
        // デフォルトのターン処理
        if (IsDead) return;  // 死んでいる場合はターンを取らない
    }
}
