using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public GameObject アイテム出し入れポップアップ;          // ポップアップの親オブジェクト
    public Image アイテム_アイコン;  // アイテム画像表示
    public TextMeshProUGUI アイテム名_表示;  // アイテム名表示
    public TextMeshProUGUI 所持数_在庫数_表示; // 所持数/在庫数表示
    public TMP_InputField 移動量_入力欄; // 移動する量
    public Button 増加ボタン;    // 数量を増加させるボタン
    public Button 減少ボタン;    // 数量を減少させるボタン
    public Button 倉庫へ移動ボタン; // 倉庫に移動するボタン
    public Button 陳列棚へ移動ボタン;   // 陳列棚に移動するボタン
    public TMP_InputField 陳列棚_価格設定_入力欄; // 陳列棚の価格設定

    private BaseItem アイテム;     // 現在選択されているアイテム
    private int 移動量 = 1; // 現在の移動数量
    public Button バツボタン;


    private void Start()
    {
        // ボタンのクリックイベントを紐付け
        増加ボタン.onClick.AddListener(IncrementAmount);
        減少ボタン.onClick.AddListener(DecrementAmount);
        倉庫へ移動ボタン.onClick.AddListener(MoveToStorage);
        陳列棚へ移動ボタン.onClick.AddListener(MoveToShelf);
        バツボタン.onClick.AddListener(ClosePopup);
        
    }

    // ポップアップを初期化して表示
    public void ShowPopup(BaseItem item)
    {
        アイテム = item;
        アイテム出し入れポップアップ.SetActive(true);

        // アイコンを設定
        if (アイテム_アイコン != null)
        {
            アイテム_アイコン.sprite = item.商品画像;
        }

        アイテム名_表示.text = item.商品名;
        所持数_在庫数_表示.text = $"所持数: {item.所持数} / 在庫数: {item.在庫}";
        移動量 = 1;
        移動量_入力欄.text = 移動量.ToString();
        陳列棚_価格設定_入力欄.text = ""; // 値段設定を初期化
    }

    // 数量増加
    public void IncrementAmount()
    {
        移動量++;
        移動量_入力欄.text = 移動量.ToString();
    }

    // 数量減少
    public void DecrementAmount()
    {
        if (移動量 > 1)
        {
            移動量--;
            移動量_入力欄.text = 移動量.ToString();
        }
    }

    // 倉庫に移動
    public void MoveToStorage()
    {
        if (アイテム.所持数 >= 移動量)
        {
            アイテム.所持数 -= 移動量;
            アイテム.在庫 += 移動量;
            Debug.Log($"アイテム {アイテム.商品名} を倉庫に {移動量} 移動しました。");
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("移動数量が不足しています。");
        }
    }

    // 陳列棚に移動
    public void MoveToShelf()
    {
        if (int.TryParse(陳列棚_価格設定_入力欄.text, out int price) && アイテム.所持数 >= 移動量)
        {
            アイテム.所持数 -= 移動量;
            アイテム.在庫 += 移動量; // 必要に応じて在庫を操作
            アイテム.相場価格 = price;
            Debug.Log($"アイテム {アイテム.商品名} を陳列棚に {移動量} 移動しました。価格: {price}");
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
        アイテム出し入れポップアップ.SetActive(false);
    }

    private void UpdateUI()
    {
        所持数_在庫数_表示.text = $"所持数: {アイテム.所持数} / 在庫数: {アイテム.在庫}";
    }
}
