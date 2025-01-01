using UnityEngine;
using System.Collections.Generic;
using System.Linq;


[CreateAssetMenu(fileName = "NewParty", menuName = "戦闘/PartyData")]
public class PartyData : ScriptableObject
{
    public List<CharacterData> 主人公 = new List<CharacterData>();  // CharacterData型のリスト（主人公専用）
    public List<NCData> 仲間 = new List<NCData>();  // NCData型のリスト（仲間専用）
    public List<Position> 主人公の位置 = new List<Position>();  // 主人公の位置リスト
    public List<Position> 仲間の位置 = new List<Position>();  // 仲間の位置リスト

    // パーティのメンバーを返すメソッド（主人公と仲間を統合）
    public List<UnitData> GetAllPartyMembers()
    {
        List<UnitData> allPartyMembers = new List<UnitData>();

        // 主人公と仲間を統合してリストに追加
        allPartyMembers.AddRange(主人公.Cast<UnitData>());
        allPartyMembers.AddRange(仲間.Cast<UnitData>());

        return allPartyMembers;
    }

    // 位置を設定する共通メソッド
    public void SetPosition(UnitData unit, Position position)
    {
        if (unit is CharacterData)  // 主人公の場合
        {
            int index = 主人公.IndexOf(unit as CharacterData);
            if (index >= 0)
            {
                if (index < 主人公の位置.Count)
                {
                    主人公の位置[index] = position;
                }
                else
                {
                    主人公の位置.Add(position);
                }
            }
        }
        else if (unit is NCData)  // 仲間の場合
        {
            int index = 仲間.IndexOf(unit as NCData);
            if (index >= 0)
            {
                if (index < 仲間の位置.Count)
                {
                    仲間の位置[index] = position;
                }
                else
                {
                    仲間の位置.Add(position);
                }
            }
        }
    }

    // 位置を取得する共通メソッド
    public Position GetPosition(UnitData unit)
    {
        if (unit is CharacterData)  // 主人公の場合
        {
            int index = 主人公.IndexOf(unit as CharacterData);
            if (index >= 0 && index < 主人公の位置.Count)
            {
                return 主人公の位置[index];
            }
        }
        else if (unit is NCData)  // 仲間の場合
        {
            int index = 仲間.IndexOf(unit as NCData);
            if (index >= 0 && index < 仲間の位置.Count)
            {
                return 仲間の位置[index];
            }
        }
        return new Position(-1, -1); // 無効な位置
    }
}
