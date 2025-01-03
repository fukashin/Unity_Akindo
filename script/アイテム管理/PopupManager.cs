using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


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
    private int 初期入力相場価格 =1; //初期入力で入る相場価格
    public Button バツボタン;
    //リストをInverntoryManagerに依存する
    public InventoryManager inventoryManager;
    private List<BaseItem> 倉庫アイテムリスト => inventoryManager.倉庫アイテムリスト;
    private List<BaseItem> 所持品アイテムリスト => inventoryManager.所持品アイテムリスト;
    private List<BaseItem> 陳列棚アイテムリスト => inventoryManager.陳列棚アイテムリスト;
    // ポップアップが閉じられたときのイベント
    public event System.Action OnPopupClosed;

    private void Awake()
    {
        if (inventoryManager == null)
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager == null)
            {
                Debug.LogError("InventoryManager がシーン内に存在しません！");
            }
        }
    }



    private void Start()
    {
        // ボタンのクリックイベントを紐付け
        増加ボタン.onClick.AddListener(IncrementAmount);
        減少ボタン.onClick.AddListener(DecrementAmount);
        バツボタン.onClick.AddListener(ClosePopup);
        
    }

    // ポップアップを初期化して表示
    public void ShowPopup(BaseItem item, int listType)
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
        初期入力相場価格 = item.相場価格;
        陳列棚_価格設定_入力欄.text = 初期入力相場価格.ToString(); // 値段設定を初期化

         // 全てのボタンの既存リスナーを削除
        倉庫へ移動ボタン.onClick.RemoveAllListeners();
        陳列棚へ移動ボタン.onClick.RemoveAllListeners();

        // ボタン表示を切り替え
        switch (listType)
        {
            case 1: // 所持品アイテムリスト
            倉庫へ移動ボタン.GetComponentInChildren<TextMeshProUGUI>().text = "倉庫へ移動";
                倉庫へ移動ボタン.gameObject.SetActive(true);
                陳列棚へ移動ボタン.gameObject.SetActive(true);
                陳列棚へ移動ボタン.GetComponentInChildren<TextMeshProUGUI>().text = "陳列棚へ移動";
                倉庫へ移動ボタン.onClick.AddListener(() => MoveToStorage());
                陳列棚へ移動ボタン.onClick.AddListener(() => MoveToShelf());
                Debug.Log("所持アイテムリスト");
                break;

            case 2: // 倉庫アイテムリスト
                倉庫へ移動ボタン.GetComponentInChildren<TextMeshProUGUI>().text = "所持品へ移動";
                倉庫へ移動ボタン.gameObject.SetActive(true);
                陳列棚へ移動ボタン.GetComponentInChildren<TextMeshProUGUI>().text = "陳列棚へ移動";
                陳列棚へ移動ボタン.gameObject.SetActive(true);

                倉庫へ移動ボタン.onClick.RemoveAllListeners();
                倉庫へ移動ボタン.onClick.AddListener(() => MoveToInventory());
                陳列棚へ移動ボタン.onClick.AddListener(() => MoveToShelf());
                Debug.Log("倉庫リスト");
                break;

            case 3: // 陳列棚アイテムリスト
                陳列棚へ移動ボタン.GetComponentInChildren<TextMeshProUGUI>().text = "陳列をやめる";
                倉庫へ移動ボタン.gameObject.SetActive(false);
                陳列棚へ移動ボタン.gameObject.SetActive(true);

                陳列棚へ移動ボタン.onClick.RemoveAllListeners();
                陳列棚へ移動ボタン.onClick.AddListener(() => RemoveFromShelf());
                Debug.Log("陳列リスト");
                break;

            default:
                Debug.LogError("無効なリストタイプが渡されました。");
                break;
        }
    }

    // 数量増加
    public void IncrementAmount()
    {
        if (移動量 < アイテム.所持数) // 所持数を超えないようにチェック
        {
            移動量++;
            移動量_入力欄.text = 移動量.ToString();
        }
        else
        {
            Debug.LogWarning($"移動量が所持数 ({アイテム.所持数}) を超えることはできません。");
        }
    }

    // 数量減少
    public void DecrementAmount()
    {
        if (移動量 > 1) // 移動量が1未満にならないように制限
        {
            移動量--;
            移動量_入力欄.text = 移動量.ToString();
        }
        else
        {
            Debug.LogWarning("移動量は1未満にできません。");
        }
    }


    // 倉庫に移動
public void MoveToStorage()
{
    if (アイテム == null)
    {
        Debug.LogError("アイテムが設定されていません！");
        return;
    }

    if (アイテム.所持数 >= 移動量)
    {
        int newStock = アイテム.在庫 + 移動量;

        if (newStock > アイテム.在庫最大数)
        {
            // 在庫最大数を超えた場合の処理
            int actualTransfer = アイテム.在庫最大数 - アイテム.在庫; // 実際に移動可能な数量
            アイテム.所持数 -= actualTransfer;
            アイテム.在庫 = アイテム.在庫最大数;

            Debug.LogWarning($"アイテム {アイテム.商品名} の在庫数が最大値を超えるため、一部のみ移動されました。移動量: {actualTransfer}");
        }
        else
        {
            // 在庫最大数を超えない場合
            アイテム.所持数 -= 移動量;
            アイテム.在庫 += 移動量;

            Debug.Log($"アイテム {アイテム.商品名} を倉庫に {移動量} 移動しました。");
        }
        Debug.Log($"{倉庫アイテムリスト.Contains(アイテム)}aaaaaaaaaaaaaaaaaaaaaaaa");

        // 倉庫リストにアイテムが存在しない場合、登録
        if (!倉庫アイテムリスト.Contains(アイテム))
        {
            倉庫アイテムリスト.Add(アイテム);
            Debug.Log($"アイテム {アイテム.商品名} を倉庫リストに追加しました。");
        }


        UpdateUI(); // UI更新
    }
    else
    {
        Debug.LogWarning("移動数量が不足しています。");
    }
}

    // 所持品へ移動（倉庫から）
    public void MoveToInventory()
    {
        if (アイテム == null)
        {
            Debug.LogError("アイテムが設定されていません！");
            return;
        }

        if (アイテム.在庫 >= 移動量)
        {
            int newInventoryCount = アイテム.所持数 + 移動量;

            if (newInventoryCount > アイテム.所持最大数)
            {
                int actualTransfer = アイテム.所持最大数 - アイテム.所持数;
                アイテム.在庫 -= actualTransfer;
                アイテム.所持数 = アイテム.所持最大数;

                Debug.LogWarning($"アイテム {アイテム.商品名} の所持数が最大値を超えるため、一部のみ移動されました。移動量: {actualTransfer}");
            }
            else
            {
                アイテム.在庫 -= 移動量;
                アイテム.所持数 += 移動量;

                Debug.Log($"アイテム {アイテム.商品名} を所持品に {移動量} 移動しました。");
            }
            

            // 所持リストにアイテムが存在しない場合、登録
            if (!所持品アイテムリスト.Contains(アイテム))
            {
                所持品アイテムリスト.Add(アイテム);
                Debug.Log($"アイテム {アイテム.商品名} を所持品アイテムリストに追加しました。");
            }

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
        // ShopData を取得
        ShopData shopData = Resources.Load<ShopData>("ShopData");
        if (shopData == null)
        {
            Debug.LogError("ShopData が見つかりません！");
            return;
        }

        // 陳列棚にあるアイテムの種類数を取得
        int totalShelfTypes = 陳列棚アイテムリスト.Count;

        // 陳列棚に同じアイテムが存在するか確認
        var existingItem = 陳列棚アイテムリスト.Find(x => x.ID == アイテム.ID);

        // 新しい種類を追加する場合に種類数をチェック
        if (existingItem == null && totalShelfTypes >= shopData.Chinretu_Dana_Max)
        {
            Debug.LogWarning($"陳列棚の種類数が最大値 ({shopData.Chinretu_Dana_Max}) を超えるため、新しいアイテムを追加できません！");
            return;
        }

        // 移動量と価格の入力をチェック
        if (int.TryParse(陳列棚_価格設定_入力欄.text, out int price) && アイテム.所持数 >= 移動量)
        {
            if (existingItem != null)
            {
                // 既存のアイテムがある場合、陳列数を増加し価格を更新
                existingItem.陳列数 += 移動量;
                existingItem.陳列価格 = price;
                Debug.Log($"既存のアイテム {existingItem.商品名} の陳列数を {移動量} 増加しました。新価格: {price}");
            }
            else
            {
                // 新しいアイテムを陳列棚に追加
                アイテム.陳列数 = 移動量;
                アイテム.陳列価格 = price;
                陳列棚アイテムリスト.Add(アイテム);
                Debug.Log($"アイテム {アイテム.商品名} を陳列棚に新たに追加しました。数量: {移動量}, 価格: {price}");
            }

            // 所持数を減少
            アイテム.所持数 -= 移動量;
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("価格が無効、または移動数量が不足しています。");
        }
    }
    // 陳列棚から降ろす
    public void RemoveFromShelf()
    {
        if (アイテム == null)
        {
            Debug.LogError("アイテムが設定されていません！");
            return;
        }

        陳列棚アイテムリスト.Remove(アイテム);
        Debug.Log($"アイテム {アイテム.商品名} を陳列棚から降ろしました。");

        // ポップアップを閉じる
        ClosePopup();
    }

    // ポップアップを非表示にする
    public void ClosePopup()
    {
        アイテム出し入れポップアップ.SetActive(false);
        OnPopupClosed?.Invoke();
    }

    private void UpdateUI()
    {
        所持数_在庫数_表示.text = $"所持数: {アイテム.所持数} / 在庫数: {アイテム.在庫}";
    }
}
