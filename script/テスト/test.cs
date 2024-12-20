// using UnityEngine;
// using UnityEngine.UI;

// public class PlayerInventory : MonoBehaviour
// {
//     public InventoryData inventoryData; // 所持アイテムリスト
//     private SaveManager saveManager;

//     public Button addItemButton; // アイテム追加ボタン（Inspectorで設定）

//     void Start()
//     {
//         saveManager = FindObjectOfType<SaveManager>();

//         // ボタンにクリックイベントを登録
//         if (addItemButton != null)
//         {
//             addItemButton.onClick.AddListener(() => AddTestItem());
//         }

//         // インベントリをロード
//         inventoryData = saveManager.LoadInventory();
//     }

//     // テスト用のアイテム追加関数
//     public void AddTestItem()
//     {
//         int testItemID = 1;    // テスト用のアイテムID
//         int testQuantity = 1; // テスト用の数量

//         AddItem(testItemID, testQuantity); // アイテムを追加
//         saveManager.SaveInventory(inventoryData); // データを保存

//         Debug.Log($"テストアイテムを追加しました: ID = {testItemID}, 数量 = {testQuantity}");
//     }

//     // アイテムを追加する関数
//     public void AddItem(int itemID, int quantity)
//     {
//         var item = inventoryData.所持アイテムリスト.Find(x => x.ItemID == itemID);

//         if (item != null)
//         {
//             item.所持数 += quantity;
//         }
//         else
//         {
//             inventoryData.所持アイテムリスト.Add(new OwnedItem { ItemID = itemID, 所持数 = quantity });
//         }
//     }
// }
