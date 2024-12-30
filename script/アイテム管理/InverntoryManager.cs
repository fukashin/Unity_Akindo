using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; // TextMeshProを使うためにインポート

public class InventoryManager : MonoBehaviour
{
    public GameObject itemPrefab; // アイテム表示用のプレハブ
    public Transform content;    // Scroll View内のContent
    public Button 所持品Button;
    public Button 倉庫Button;
    public Button 陳列棚Button;
    public Button AddToInventoryButton; // 所持品にアイテムを追加するボタン
    public Button AddToStorageButton;   // 倉庫にアイテムを追加するボタン
    public Button AddToShelfButton;     // 陳列棚にアイテムを追加するボタン

    public ItemDatabase itemDatabase;  // ItemDatabaseの参照
    public Sprite defaultSprite;       // デフォルトのアイコン画像

    public List<BaseItem> 倉庫アイテムリスト = new List<BaseItem>();
    public List<BaseItem> 所持品アイテムリスト = new List<BaseItem>();
    public List<BaseItem> 陳列棚アイテムリスト = new List<BaseItem>();

    /*
    void Awake()
    {
        // Resources フォルダ内から ItemDatabase をロード
        itemDatabase = Resources.Load<ItemDatabase>("ItemDatabase"); // "ItemDatabase" はアセットの名前
        if (itemDatabase == null)
        {
            Debug.LogError("ItemDatabaseが見つかりません。Resourcesフォルダ内に正しい名前のファイルがあるか確認してください。");
        }
        else
        {

        }
    }
    */

    private void Start()
    {
        // アイテムリストが正しく取得できているかを確認
        InitializeItemLists();  // ここで所持品リストを初期化

        // ボタンにクリックイベントを登録
        所持品Button.onClick.AddListener(() => UpdateUI(所持品アイテムリスト));
        倉庫Button.onClick.AddListener(() => UpdateUI(倉庫アイテムリスト));
        陳列棚Button.onClick.AddListener(() => UpdateUI(陳列棚アイテムリスト));

        // アイテム追加ボタンのクリックイベントを設定
        AddToInventoryButton.onClick.AddListener(() => AddItemToList(所持品アイテムリスト, 1, 1));
        AddToStorageButton.onClick.AddListener(() => AddItemToList(倉庫アイテムリスト, 2, 1));
        AddToShelfButton.onClick.AddListener(() => AddItemToList(陳列棚アイテムリスト, 3, 1));
    }

    private void InitializeItemLists()
    {
        if (itemDatabase != null)
        {
            所持品アイテムリスト = itemDatabase.GetItemsByCategory("所持品");
            倉庫アイテムリスト = itemDatabase.GetItemsByCategory("倉庫");
            陳列棚アイテムリスト = itemDatabase.GetItemsByCategory("陳列棚");
        }
        else
        {
            Debug.LogError("itemDatabase is null!");
        }
    }

    private void AddItemToList(List<BaseItem> list, int itemID, int quantity)
    {
        if (list == null)
        {
            Debug.LogError("List is null!");
            return;
        }

        // アイテムをリスト内で探す
        var item = list.Find(x => x.ID == itemID);
        if (item != null)
        {
            // アイテムがすでに存在する場合は所持数を増加させる
            item.所持数 += quantity;
        }
        else
        {
            if (itemDatabase != null)
            {
                // アイテムがリストに存在しない場合、アイテムデータベースから取得
                BaseItem newItem = itemDatabase.GetItemByID(itemID); // GetItemByIDはアイテムIDでアイテムを取得するメソッド
                if (newItem != null)
                {
                    // 新しいアイテムをリストに追加
                    list.Add(newItem);
                }
                else
                {
                    Debug.LogError($"アイテムID {itemID} がデータベースに存在しません。");
                }
            }
            else
            {
                Debug.LogError("itemDatabase is null!");
            }
        }

        // UI更新
        UpdateUI(list);
        Debug.Log($"アイテムを追加しました: ID = {itemID}, 数量 = {quantity}");
    }


    private void UpdateUI(List<BaseItem> items)
    {
        Debug.Log("UpdateUI called");
        Debug.Log("Child count: " + content.childCount);

        // 既存のアイテムを削除（プレースホルダーは削除しない）
        foreach (Transform child in content)
        {
            if (child.name == "ItemRowPrefab")
            {
                Debug.Log("Skipping placeholder prefab: " + child.name);
                continue;
            }
            Debug.Log("Deleting: " + child.name);
            Destroy(child.gameObject);
        }

        if (items == null || items.Count == 0)
        {
            Debug.LogWarning("Item list is empty!");
            return;
        }

        foreach (var item in items)
        {
            var row = Instantiate(itemPrefab, content);
            row.gameObject.SetActive(true);
            Debug.Log("Instantiated row: " + row.name);

            // アイコンの設定
            var itemImage = row.transform.Find("アイコン");
            if (itemImage != null)
            {
                var imageComponent = itemImage.GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = item.商品画像 ?? defaultSprite;
                }
            }

            // 商品名の設定（TextMeshProUGUI）
            var itemNameText = row.transform.Find("商品名").GetComponent<TextMeshProUGUI>();
            if (itemNameText != null)
            {
                itemNameText.text = item.商品名 ?? "Unknown Item";
            }

            // 在庫の設定（TextMeshProUGUI）
            var stockText = row.transform.Find("在庫").GetComponent<TextMeshProUGUI>();
            if (stockText != null)
            {
                if(items == 所持品アイテムリスト)
                {
                    stockText.text = item.所持数 != 0 ? $"所持数: {item.所持数}" : "所持数なし";
                }
                else  // 他のリストの場合（倉庫、陳列棚など）
                {
                    stockText.text = item.在庫 != 0 ? $"在庫数: {item.在庫}" : "在庫なし";
                }
            }

            // 相場価格の設定（TextMeshProUGUI）
            var priceText = row.transform.Find("相場価格").GetComponent<TextMeshProUGUI>();
            if (priceText != null)
            {
                priceText.text = item.相場価格 != 0 ? item.相場価格.ToString() : "相場価格未設定";
            }

            // 需要の設定（TextMeshProUGUI）
            var demandText = row.transform.Find("需要").GetComponent<TextMeshProUGUI>();
            if (demandText != null)
            {
                demandText.text = item.需要 != 0 ? item.需要.ToString() +"%" : "需要未設定";
            }
        }
    }
}
