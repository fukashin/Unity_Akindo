using UnityEngine;
using UnityEngine.UI;

public class AddTestItem : MonoBehaviour
{
    public PlayerInventory playerInventory; // PlayerInventory の参照（Inspectorで設定）
    public Button addItemButton;            // アイテム追加ボタン（Inspectorで設定）
    public SaveManager SaveManager;
    public InventoryData inventoryData; // プレイヤーが所持しているアイテムリスト

    void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogError("playerInventory が設定されていません！Inspector で設定してください。");
            return; // 処理を中断
        }

        if (addItemButton != null)
        {
            // ボタンにクリックイベントを登録
            addItemButton.onClick.AddListener(AddTestItemmmm);
        }
        else
        {
            Debug.LogError("addItemButton が設定されていません！Inspector で設定してください。");
        }
    }

    public void AddTestItemmmm()
    {
        if (playerInventory == null)
        {
            Debug.LogError("playerInventory が設定されていません！処理を中断します。");
            return;
        }

        int testItemID = 10;    // 固定のアイテムID
        int testQuantity = 1;  // 固定の数量

        Debug.Log($"AddTestItemmmm メソッドが呼び出されました: ID = {testItemID}, 数量 = {testQuantity}");

        // PlayerInventory にアイテム追加を依頼
        playerInventory.AddItem(testItemID, testQuantity);
        

        Debug.Log($"AddTestItem: ID = {testItemID}, 数量 = {testQuantity} を追加しました。");
    }
}
