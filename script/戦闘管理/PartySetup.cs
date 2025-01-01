using UnityEngine;
using System.Linq;

public class PartySetup : MonoBehaviour
{
    public PartyData playerParty;

    void Start()
    {
        // サンプルユニット
        UnitData hero = playerParty.主人公.FirstOrDefault();  // 最初の主人公（CharacterData）

        // 最大3人の仲間
        var allies = playerParty.仲間.Take(3).ToList();  // 仲間リストから最大3人を取得

        if (hero != null)
        {
            // 主人公の位置を設定
            playerParty.SetPosition(hero, new Position(0, 0));

            // 仲間の位置を設定
            for (int i = 0; i < allies.Count; i++)
            {
                playerParty.SetPosition(allies[i], new Position(i + 1, 0));  // 仲間を横に並べて配置
            }

            // 位置の取得
            Position heroPosition = playerParty.GetPosition(hero);
            Debug.Log($"Hero Position: {heroPosition.x}, {heroPosition.y}");

            foreach (var ally in allies)
            {
                Position allyPosition = playerParty.GetPosition(ally);
                Debug.Log($"Ally Position: {allyPosition.x}, {allyPosition.y}");
            }
        }
        else
        {
            Debug.LogError("主人公が見つかりませんでした");
        }
    }
}
