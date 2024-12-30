using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using soubiSystem;  // EquipCategoryを参照するためにインポート

public class EquipmentUI : MonoBehaviour
{
    [Header("キャラクターのデータ")]
    public CharacterData characterData; // CharacterDataへの参照 テスト

    [Header("装備アイコン用のパネル")]
    public RectTransform equipmentPanel; // RectTransform型に変更

    private Dictionary<string, Image> equipmentIcons = new Dictionary<string, Image>();

    void Start()
    {
        InitializeEquipmentIcons();
        UpdateEquipmentIcons();
    }

    // 装備品パネル内のアイコンを自動検出
    void InitializeEquipmentIcons()
    {
        equipmentIcons.Clear();

        // 装備品アイコンの命名規則に基づいて自動登録
        foreach (Transform child in equipmentPanel)
        {
            if (child.TryGetComponent<Image>(out Image icon))
            {
                equipmentIcons[child.name.ToLower()] = icon;
            }
        }
    }

    // 装備アイコンを更新
    void UpdateEquipmentIcons()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterData is not assigned in the EquipmentUI script.");
            return;
        }

        // 装備品アイコンを動的に設定
        SetIcon(EquipItem.EquipCategory.武器, characterData.GetEquippedItemByCategory(EquipItem.EquipCategory.武器));
        SetIcon(EquipItem.EquipCategory.頭装備, characterData.GetEquippedItemByCategory(EquipItem.EquipCategory.頭装備));
        SetIcon(EquipItem.EquipCategory.胴体装備, characterData.GetEquippedItemByCategory(EquipItem.EquipCategory.胴体装備));
        SetIcon(EquipItem.EquipCategory.脚装備, characterData.GetEquippedItemByCategory(EquipItem.EquipCategory.脚装備));
        SetIcon(EquipItem.EquipCategory.手装備, characterData.GetEquippedItemByCategory(EquipItem.EquipCategory.手装備));
        SetIcon(EquipItem.EquipCategory.装飾品1, characterData.GetEquippedItemByCategory(EquipItem.EquipCategory.装飾品1));
        SetIcon(EquipItem.EquipCategory.装飾品2, characterData.GetEquippedItemByCategory(EquipItem.EquipCategory.装飾品2));
    }

    void SetIcon(EquipItem.EquipCategory category, EquipItem item)
    {
        // アイテムが null でなければアイコンを設定
        if (item != null && equipmentIcons.TryGetValue(category.ToString().ToLower(), out Image icon))
        {
            if (icon != null)
            {
                icon.sprite = item.商品画像;
                icon.gameObject.SetActive(true);
                Debug.Log($"{category} のアイコンが設定されました: {item.商品画像.name}");
            }
        }
        else
        {
            // アイテムが null の場合、アイコンを非表示にする
            if (equipmentIcons.TryGetValue(category.ToString().ToLower(), out Image iconToHide) && iconToHide != null)
            {
                iconToHide.gameObject.SetActive(false);
                Debug.LogWarning($"{category} のアイコンは表示されません");
            }
        }
    }
}