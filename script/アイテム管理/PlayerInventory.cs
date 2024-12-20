using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public InventoryData inventoryData; // プレイヤーが所持しているアイテムリスト
    private SaveManager saveManager;   // セーブやロードを管理するクラス

    public Button addItemButton;       // アイテム追加ボタン（Inspectorで設定）
    public InputField itemIDInput;     // アイテムID入力フィールド
    public InputField quantityInput;   // 数量入力フィールド

    // Unityがゲーム開始時に自動で呼び出すメソッド
    void Start()
    {
        // SaveManagerスクリプトをシーン内から探して取得
        saveManager = FindObjectOfType<SaveManager>();

        // アイテム追加ボタンにクリックイベントを登録
        if (addItemButton != null)
        {
            // ボタンをクリックすると AddTestItem メソッドが呼び出される
            addItemButton.onClick.AddListener(() => AddTestItem());
            Debug.Log("倉庫が開きます");
        }

        // 保存されているインベントリデータをロードして復元
        inventoryData = saveManager.LoadInventory();
    }

    // テスト用のアイテム追加関数
    public void AddTestItem()
    {
        // 入力フィールドからアイテムIDと数量を取得
        int testItemID;
        int testQuantity;

        // 入力値を整数に変換（失敗した場合はデフォルト値として1を使用）
        if (!int.TryParse(itemIDInput.text, out testItemID)) testItemID = 1;
        if (!int.TryParse(quantityInput.text, out testQuantity)) testQuantity = 1;

        // アイテムをインベントリに追加
        AddItem(testItemID, testQuantity);

        // 追加したインベントリを保存
        saveManager.SaveInventory(inventoryData);

        // デバッグログで結果を確認
        Debug.Log($"テストアイテムを追加しました: ID = {testItemID}, 数量 = {testQuantity}");
    }

    // アイテムを追加するメソッド
    // 引数: itemID（アイテムのID）, quantity（追加する数量）
    public void AddItem(int itemID, int quantity)
    {
        // 所持アイテムリストから指定したIDのアイテムを検索
        var item = inventoryData.所持アイテムリスト.Find(x => x.ItemID == itemID);

        if (item != null) // すでにアイテムを所持している場合
        {
            item.所持数 += quantity; // 所持数を増加
        }
        else // 所持していない新しいアイテムの場合
        {
            // 新しいアイテムをリストに追加
            inventoryData.所持アイテムリスト.Add(new OwnedItem { ItemID = itemID, 所持数 = quantity });
        }

        // デバッグログで現在のインベントリの状態を表示
        Debug.Log($"アイテムを追加しました: ID = {itemID}, 数量 = {quantity}");
    }
}
