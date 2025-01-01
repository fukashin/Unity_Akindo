using UnityEngine;
using System.Collections.Generic;  // List<T> を使用するために必要な名前空間

[CreateAssetMenu(fileName = "NewEnemyParty", menuName = "戦闘/EnemyPartyData")]
public class EnemyPartyData : ScriptableObject
{
    public string partyName;        // パーティ名（任意）
    public EnemyData[] enemies;     // パーティに所属する敵
    public List<Position> enemyPositions = new List<Position>();  // 敵の位置リスト

    // GetAllPartyMembers メソッドを追加
    public List<UnitData> GetAllPartyMembers()
    {
        List<UnitData> allEnemies = new List<UnitData>();
        foreach (var enemy in enemies)
        {
            allEnemies.Add(enemy as UnitData);  // EnemyData を UnitData として追加
        }
        return allEnemies;
    }

    // 敵の位置を設定するメソッド
    public void SetEnemyPosition(UnitData unit, Position position)
    {
        if (unit is EnemyData enemy)  // (1) unit が EnemyData 型の場合
        {
            int index = System.Array.IndexOf(enemies, enemy);  // enemy が enemies 配列のどこにあるか調べる
            if (index >= 0)
            {
                if (index < enemyPositions.Count)
                {
                    enemyPositions[index] = position;  // 既存の位置を更新
                }
                else
                {
                    enemyPositions.Add(position);  // 新しい位置を追加
                }
            }
        }
    }

    // 敵の位置を取得するメソッド
    public Position GetEnemyPosition(UnitData unit)
    {
        if (unit is EnemyData enemy)  // (3) unit が EnemyData 型の場合
        {
            int index = System.Array.IndexOf(enemies, enemy);  // enemy が enemies 配列のどこにあるか調べる
            if (index >= 0 && index < enemyPositions.Count)
            {
                return enemyPositions[index];  // 敵の位置を返す
            }
        }
        return new Position(-1, -1);  // 無効な位置（例: -1, -1）
    }

}
