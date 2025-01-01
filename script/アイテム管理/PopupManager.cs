using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public GameObject popup;          // ポップアップの親オブジェクト
    public TextMeshProUGUI itemName;  // アイテム名表示
    public TextMeshProUGUI stockText; // 所持数/在庫数表示
    public InputField moveAmountInput; // 移動する量
    public Button incrementButton;    // 数量を増加させるボタン
    public Button decrementButton;    // 数量を減少させるボタン
    public Button moveToStorageButton; // 倉庫に移動するボタン
    public Button moveToShelfButton;   // 陳列棚に移動するボタン
    public InputField shelfPriceInput; // 陳列棚の価格設定

    private BaseItem currentItem;     // 現在選択されているアイテム
    private int currentMoveAmount = 1; // 現在の移動数量

    // ポップアップを初期化して表示
    public void ShowPopup(BaseItem item)
    {
        currentItem = item;
        popup.SetActive(true);
        itemName.text = item.商品名;
        stockText.text = $"所持数: {item.所持数} / 在庫数: {item.在庫}";
        currentMoveAmount = 1;
        moveAmountInput.text = currentMoveAmount.ToString();
        shelfPriceInput.text = ""; // 値段設定を初期化
    }

    // 数量増加
    public void IncrementAmount()
    {
        currentMoveAmount++;
        moveAmountInput.text = currentMoveAmount.ToString();
    }

    // 数量減少
    public void DecrementAmount()
    {
        if (currentMoveAmount > 1)
        {
            currentMoveAmount--;
            moveAmountInput.text = currentMoveAmount.ToString();
        }
    }

    // 倉庫に移動
    public void MoveToStorage()
    {
        if (currentItem.所持数 >= currentMoveAmount)
        {
            currentItem.所持数 -= currentMoveAmount;
            currentItem.在庫 += currentMoveAmount;
            Debug.Log($"アイテム {currentItem.商品名} を倉庫に {currentMoveAmount} 移動しました。");
            UpdateUI();
        }
    }

    // 陳列棚に移動
    public void MoveToShelf()
    {
        if (int.TryParse(shelfPriceInput.text, out int price) && currentItem.所持数 >= currentMoveAmount)
        {
            currentItem.所持数 -= currentMoveAmount;
            currentItem.在庫 += currentMoveAmount; // 必要に応じて在庫を操作
            currentItem.相場価格 = price;
            Debug.Log($"アイテム {currentItem.商品名} を陳列棚に {currentMoveAmount} 移動しました。価格: {price}");
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("価格が無効、または移動数量が不足しています。");
        }
    }

    // ポップアップを非表示にする
    public void ClosePopup()
    {
        popup.SetActive(false);
    }

    private void UpdateUI()
    {
        stockText.text = $"所持数: {currentItem.所持数} / 在庫数: {currentItem.在庫}";
    }
}
