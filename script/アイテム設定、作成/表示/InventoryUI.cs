using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryUI : MonoBehaviour
{
    public ItemDatabase itemDatabase; // アイテムデータベース
    public GameObject itemRowPrefab;  // アイテム行のプレハブ
    public Transform contentParent;   // ScrollViewのContent
    public Sprite defaultSprite;  

 void Start()
    {
        DisplayItems();
    }

void DisplayItems()
{
    // Content内の子要素をクリア
    foreach (Transform child in contentParent)
    {
        Destroy(child.gameObject);
    }

    // アイテムデータを表示
    foreach (NormalItem item in itemDatabase.通常アイテムリスト)
    {
            GameObject row = Instantiate(itemRowPrefab, contentParent);

            Behaviour[] components = row.GetComponentsInChildren<Behaviour>(true);
            foreach (Behaviour component in components)
            {
                component.enabled = true;
            }

            // ログで状態を確認
            Debug.Log($"生成されたアイテム: {item.商品名}, Active状態: {row.activeSelf}, 親: {contentParent.name}");

            // UI要素にデータを反映
            TMP_Text itemNameText = row.transform.Find("ItemName").GetComponent<TMP_Text>();
            itemNameText.enabled = true; // コンポーネントを有効化
            itemNameText.text = item.商品名;

            TMP_Text priceText = row.transform.Find("Price").GetComponent<TMP_Text>();
            priceText.enabled = true;
            priceText.text = item.相場.ToString() + " G";

            TMP_Text itemTypeText = row.transform.Find("ItemType").GetComponent<TMP_Text>();
            itemTypeText.enabled = true;
            itemTypeText.text = item.大分類.ToString();

            // 画像コンポーネントを有効化し、デフォルト画像を設定
            Image itemImage = row.transform.Find("ItemImage").GetComponent<Image>();
            itemImage.enabled = true; // コンポーネントを有効化
            if (item.商品画像 != null)
            {
                itemImage.sprite = item.商品画像;
            }
            else
            {
                Debug.LogWarning($"Item '{item.商品名}' の商品画像が設定されていません！デフォルト画像を表示します。");
                itemImage.sprite = defaultSprite;
            }
        }
    }
}