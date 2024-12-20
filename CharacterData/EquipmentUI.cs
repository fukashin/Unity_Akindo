using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class EquipmentUI : MonoBehaviour
{
    [Header("�L�����N�^�[�̃f�[�^")]
    public CharacterData characterData; // CharacterData�ւ̎Q��

    [Header("�����A�C�R���p�̃p�l��")]
    public RectTransform equipmentPanel; // RectTransform�^�ɕύX


    private Dictionary<string, Image> equipmentIcons = new Dictionary<string, Image>();

    void Start()
    {
        InitializeEquipmentIcons();
        UpdateEquipmentIcons();
    }

    // �����i�p�l�����̃A�C�R�����������o
    void InitializeEquipmentIcons()
    {
        equipmentIcons.Clear();

        // �����i�A�C�R���̖����K���Ɋ�Â��Ď����o�^
        foreach (Transform child in equipmentPanel)
        {
            if (child.TryGetComponent<Image>(out Image icon))
            {
                equipmentIcons[child.name.ToLower()] = icon;
            }
        }
    }

    // �����A�C�R�����X�V
    void UpdateEquipmentIcons()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterData is not assigned in the EquipmentUI script.");
            return;
        }

        // �����i�A�C�R���𓮓I�ɐݒ�
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
            if (item != null && item.���i�摜 != null)
            {
                icon.sprite = item.���i�摜;
                icon.gameObject.SetActive(true);
            }
            else
            {
                icon.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning($"No icon found for slot: {slotName}");
        }
    }
}
