using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


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
        SetIcon("weapon", characterData.weapon);
        SetIcon("headgear", characterData.headGear);
        SetIcon("bodyarmor", characterData.bodyArmor);
        SetIcon("leg", characterData.leg);
        SetIcon("hands", characterData.hands);
        SetIcon("accessory1", characterData.accessory1);
        SetIcon("accessory2", characterData.accessory2);
    }

    void SetIcon(string slotName, EquipItem item)
    {
        if (equipmentIcons.TryGetValue(slotName, out Image icon))
        {
            if (item != null && item.商品画像 != null)
            {
                icon.sprite = item.商品画像;
                icon.gameObject.SetActive(true);
                Debug.Log($"Icon set for {slotName}: {item.商品画像.name}");
            }
            else
            {
                icon.gameObject.SetActive(false);
                Debug.LogWarning($"No image found for {slotName}");
            }
        }
        else
        {
            Debug.LogWarning($"No icon found for slot: {slotName}");
        }
    }
}