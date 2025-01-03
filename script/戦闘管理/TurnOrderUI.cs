using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurnOrderUI : MonoBehaviour
{
    public RectTransform uiParent;  // UIの親オブジェクト
    public GameObject unitUIPrefab; // プレハブの参照（Unityエディタで設定）

    void Start()
    {
        // VerticalLayoutGroupの設定
        var layoutGroup = uiParent.gameObject.AddComponent<VerticalLayoutGroup>();
        layoutGroup.spacing = 100f;  // 生成されるオブジェクト同士の間隔
        layoutGroup.padding = new RectOffset(0, 0, 0, 0);  // パディングなし
        layoutGroup.childForceExpandWidth = false;  // 幅の自動拡張を無効にする
        layoutGroup.childForceExpandHeight = false; // 高さの自動拡張を無効にする

        // 親オブジェクトのアンカーを設定（親のサイズに依存しないように）
        uiParent.anchorMin = new Vector2(0f, 1f);  // 左下に設定
        uiParent.anchorMax = new Vector2(0f, 1f);  // 左上に設定
        uiParent.pivot = new Vector2(0.5f, 0.5f);  // 左中央を基準に設定
    }

    public void UpdateTurnOrderUI(List<UnitData> turnOrder)
    {
        // 既存のUIをクリア
        foreach (Transform child in uiParent)
        {
            Destroy(child.gameObject);
        }

        // プレハブを使って新しいUIを生成
        foreach (var unit in turnOrder)
        {
            GameObject unitUIElement = Instantiate(unitUIPrefab, uiParent);
            unitUIElement.name = unit.Unitname;  // 名前を設定

            // アイコンとテキストを設定
            var iconImage = unitUIElement.transform.Find("Icon").GetComponent<Image>();
            var text = unitUIElement.transform.Find("UnitName").GetComponent<TextMeshProUGUI>();

            text.lineSpacing = 10f;  // 行間を広げる（数値はお好みで調整）

            // アイコンの設定
            iconImage.sprite = unit.Icon;

            // 名前の設定
            text.text = unit.Unitname;

            // プレハブのサイズ調整
            RectTransform unitRect = unitUIElement.GetComponent<RectTransform>();
            unitRect.sizeDelta = new Vector2(400, 100);  // 必要に応じてサイズを調整
        }
    }
}
