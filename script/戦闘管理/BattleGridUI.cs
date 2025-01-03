using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleGridUI : MonoBehaviour
{
    public static BattleGridUI Instance;

    public GameObject gridCellPrefab;  // グリッドセルのPrefab
    public Transform gridContainer;    // グリッドを配置する親オブジェクト

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateGridUI(UnitData[,] battleGrid)
    {
        // 既存のUIを削除
        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }

        // グリッドを描画
        for (int x = 0; x < battleGrid.GetLength(0); x++)
        {
            for (int y = 0; y < battleGrid.GetLength(1); y++)
            {
                UnitData character = battleGrid[x, y];
                GameObject gridCell = Instantiate(gridCellPrefab, gridContainer);
                gridCell.name = $"Cell ({x}, {y})";

                // テキストコンポーネントの取得
                TextMeshProUGUI textComponent = gridCell.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    // キャラクターがいる場合、名前と座標を表示
                    if (character != null)
                    {
                        textComponent.text = $"{character.Unitname}\n({x}, {y})";
                    }
                    // キャラクターがいない場合、座標のみ表示
                    else
                    {
                        textComponent.text = $"({x}, {y})";
                    }
                }

                // アイコンを追加する部分
                Image iconImage = gridCell.GetComponentInChildren<Image>();
                if (iconImage != null && character != null && character.Icon != null)
                {
                    // キャラクターがいる場合、アイコン画像を設定
                    iconImage.sprite = character.Icon;
                    iconImage.enabled = true;  // アイコンを表示
                }
                else if (iconImage != null)
                {
                    // アイコンがない場合は非表示
                    iconImage.enabled = false;
                }
            }
        }
    }
}
