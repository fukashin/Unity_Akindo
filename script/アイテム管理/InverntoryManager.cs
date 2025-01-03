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
    public IDManager idManager; // IDManagerへの参照
    public Sprite defaultSprite;       // デフォルトのアイコン画像
    public PopupManager popupManager; // PopupManager の参照

    public List<BaseItem> 倉庫アイテムリスト = new List<BaseItem>();
    public List<BaseItem> 所持品アイテムリスト = new List<BaseItem>();
    public List<BaseItem> 陳列棚アイテムリスト = new List<BaseItem>();

    private int currentListType; // 現在のリストタイプを記録

    // void Awake()
    // {
    //     // Resources フォルダ内から ItemDatabase をロード
    //     itemDatabase = Resources.Load<ItemDatabase>("ItemDatabase"); // "ItemDatabase" はアセットの名前
    //     if (itemDatabase == null)
    //     {
    //         Debug.LogError("ItemDatabaseが見つかりません。Resourcesフォルダ内に正しい名前のファイルがあるか確認してください。");
    //     }
    //     else
    //     {
    //         Debug.LogError("ItemDatabaseは読み込まれました。");

    //         DebugList("読み取り倉庫アイテムリスト", itemDatabase.倉庫アイテムリスト);
    //         DebugList("読み取り所持品アイテムリスト", itemDatabase.所持品アイテムリスト);
    //         DebugList("読み取り陳列棚アイテムリスト", itemDatabase.陳列棚アイテムリスト);
    //     }
    // }

    // リストの内容をデバッグ表示するメソッド
    void DebugList(string listName, List<BaseItem> itemList)
    {
        if (itemList != null && itemList.Count > 0)
        {
            Debug.Log($"{listName}のアイテム数: {itemList.Count}");
            foreach (var item in itemList)
            {
                Debug.Log($"{listName} - アイテム名: {item.商品名}");
            }
        }
        else
        {
            Debug.LogWarning($"{listName}は空です。");
        }
    }

    private void Start()
    {
        itemDatabase = Resources.Load<ItemDatabase>("ItemDatabase"); // "ItemDatabase" はアセットの名前
        // ポップアップマネーシャーを読み込み
        // アイテムリストが正しく取得できているかを確認
        InitializeItemLists();  // ここで所持品リストを初期化

        // アイテムリストが正しく取得できているかを確認
        Debug.Log("所持品アイテムリストのアイテム数: " + 所持品アイテムリスト.Count);
        Debug.Log("倉庫アイテムリストのアイテム数: " + 倉庫アイテムリスト.Count);
        Debug.Log("陳列棚アイテムリストのアイテム数: " + 陳列棚アイテムリスト.Count);

        // ボタンにクリックイベントを登録
        所持品Button.onClick.AddListener(() => UpdateUI(所持品アイテムリスト));
        倉庫Button.onClick.AddListener(() => UpdateUI(倉庫アイテムリスト));
        陳列棚Button.onClick.AddListener(() => UpdateUI(陳列棚アイテムリスト));

        // アイテム追加ボタンのクリックイベントを設定
        AddToInventoryButton.onClick.AddListener(() => AddItemToList(所持品アイテムリスト, 1, 1));
        AddToStorageButton.onClick.AddListener(() => AddItemToList(所持品アイテムリスト, 2, 1));
        AddToShelfButton.onClick.AddListener(() => AddItemToList(所持品アイテムリスト, 3, 1));

        // PopupManager のイベントを購読
        popupManager.OnPopupClosed += HandlePopupClosed;
    }

    // ポップアップが閉じられた際に実行される処理
    private void HandlePopupClosed()
    {
        // 現在のリストを基に UI を更新
        if (currentListType == 1)
        {
            UpdateUI(所持品アイテムリスト);
        }
        else if (currentListType == 2)
        {
            UpdateUI(倉庫アイテムリスト);
        }
        else if (currentListType == 3)
        {
            UpdateUI(陳列棚アイテムリスト);
        }
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

    //  アイテムの取得処理
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
            int newQuantity = item.所持数 + quantity;
            Debug.Log($"アイテムを追加しました: ID = {itemID}, 数量 = {quantity}");

            if (newQuantity > item.所持最大数)
            {
                Debug.LogWarning($"アイテム {item.商品名} の所持数が最大値を超えるため、一部のみ追加されます。");
                item.所持数 = item.所持最大数; // 所持数を最大値に設定
            }
            else
            {
                item.所持数 = newQuantity;
            }
        }
        else
        {
            // アイテムがリストに存在しない場合
            if (idManager != null)
            {
                // アイテムがリストに存在しない場合、アイテムデータベースから取得
                BaseItem newItem = idManager.GetItemByID(itemID); // GetItemByIDはアイテムIDでアイテムを取得するメソッド
                if (newItem != null)
                {
                    if (quantity > newItem.所持最大数)
                    {
                        Debug.LogWarning($"アイテム {newItem.商品名} の初期追加量が最大所持数を超えるため、最大値に調整されます。");
                        newItem.所持数 = newItem.所持最大数; // 所持数を最大値に設定
                    }
                    else
                    {
                        newItem.所持数 = quantity;
                    }

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

    private void OnItemClick(BaseItem item,int listType)
    {
        Debug.Log($"Clicked on item: {item.商品名}");
        currentListType = listType; // リストタイプを記録

        // 所持数・在庫数を表示
        Debug.Log($"所持数: {item.所持数}, 在庫数: {item.在庫}");

        // カスタムUI（ダイアログなど）を表示
        popupManager.ShowPopup(item,listType);
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

        // 削除対象アイテムのリストを準備
        List<BaseItem> itemsToRemove = new List<BaseItem>();

        foreach (var item in items)
        {
            // 所持数または在庫が0の場合は削除対象に追加
            if (items == 所持品アイテムリスト && item.所持数 == 0)
            {
                itemsToRemove.Add(item);
                Debug.Log($"Removing item from 所持品リスト: {item.商品名}");
                continue;
            }

            if (items == 倉庫アイテムリスト && item.在庫 == 0)
            {
                itemsToRemove.Add(item);
                Debug.Log($"Removing item from 倉庫リスト: {item.商品名}");
                continue;
            }

            // UIの更新
            var row = Instantiate(itemPrefab, content);
            row.gameObject.SetActive(true);

            // アイテムのボタンを設定
            var button = row.GetComponent<Button>();
            if (button != null)
            {
                if (items == 所持品アイテムリスト)
                {
                    button.onClick.AddListener(() => OnItemClick(item, 1));
                }
                else if (items == 倉庫アイテムリスト)
                {
                    button.onClick.AddListener(() => OnItemClick(item, 2));
                }
                else if (items == 陳列棚アイテムリスト)
                {
                    button.onClick.AddListener(() => OnItemClick(item, 3));
                }
            }

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

            // 商品名の設定
            var itemNameText = row.transform.Find("商品名").GetComponent<TextMeshProUGUI>();
            if (itemNameText != null)
            {
                itemNameText.text = item.商品名 ?? "Unknown Item";
            }

            // 在庫や所持数の設定
            var stockText = row.transform.Find("在庫").GetComponent<TextMeshProUGUI>();
            if (stockText != null)
            {
                if (items == 所持品アイテムリスト)
                {
                    stockText.text = item.所持数 != 0 ? $" {item.所持数}" : "所持数なし";
                }
                else if(items == 倉庫アイテムリスト)
                {
                    stockText.text = item.在庫 != 0 ? $" {item.在庫}" : "在庫なし";
                }
                else if(items == 陳列棚アイテムリスト)
                {
                    stockText.text = item.陳列数 != 0 ? $" {item.陳列数}" : "陳列数なし";
                }
            }

            // 相場価格の設定
            var priceText = row.transform.Find("相場価格").GetComponent<TextMeshProUGUI>();
            if (priceText != null)
            {
                if (items == 陳列棚アイテムリスト)
                {
                    priceText.text = item.陳列価格 != 0 ? $" {item.陳列価格}" : "価格設定なし";
                }
                else
                {
                    priceText.text = item.相場価格 != 0 ? item.相場価格.ToString() : "相場価格未設定";
                }
            }

            // 需要の設定
            var demandText = row.transform.Find("需要").GetComponent<TextMeshProUGUI>();
            if (demandText != null)
            {
                demandText.text = item.需要 != 0 ? item.需要.ToString() + "%" : "需要未設定";
            }
        }

        // リストから削除対象を一括削除
        foreach (var itemToRemove in itemsToRemove)
        {
            items.Remove(itemToRemove);
        }
    }

}
