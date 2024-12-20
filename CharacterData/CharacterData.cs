using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("�L�����N�^�[��{���")]
    public string characterName; // �L�����N�^�[��

    [Header("�X�e�[�^�X")]
    [Tooltip("�L�����N�^�[�̍ő�HP")]
    public int maxHP;            // �ő�HP
    [Tooltip("�L�����N�^�[�̍U����")]
    public int attackPower;      // �U����
    [Tooltip("�L�����N�^�[�̖h���")]
    public int defensePower;     // �h���

    [Header("���̑��̐ݒ�")]
    [Tooltip("�L�����N�^�[�̈ړ����x")]
    public float moveSpeed;      // �ړ����x
    [Tooltip("�L�����N�^�[�̃A�C�R��")]
    public Sprite characterIcon; // �L�����N�^�[�̃A�C�R��
    [Tooltip("�L�����N�^�[�̃v���n�u")]
    public GameObject prefab;    // �L�����N�^�[�̃v���n�u

    [Header("�o���l�⃌�x��")]
    public int level;            // �L�����N�^�[�̃��x��
    [Tooltip("���݂̌o���l")]
    public int currentXP;        // ���݂̌o���l
    [Tooltip("�����̃��x���A�b�v�ɕK�v�Ȍo���l")]
    public int baseXP;           // �����̃��x���A�b�v�ɕK�v�Ȍo���l
    [Tooltip("�o���l�̐�����")]
    public float growthRate;     // �o���l�̐������i�Ⴆ��1.2�Ȃ�20%�����j

    [Header("�X�L��")]
    public List<string> skills;  // �L�����N�^�[�����X�L���̃��X�g

    [Header("�����i")]
    public List<BaseItem> inventory; // CharacterData����BaseItem���g�p
    public int maxInventorySize = 5; // �ő�5��ނ܂�


    [Header("�����i")]
    [Tooltip("����")]
    public EquipItem weapon;      // ����
    [Tooltip("������")]
    public EquipItem headGear;    // ������
    [Tooltip("���̑���")]
    public EquipItem bodyArmor;    // ���̑���
    [Tooltip("�r����")]
    public EquipItem leg;     // �r����
    [Tooltip("�葕��")]
    public EquipItem hands;    // �葕��
    [Tooltip("�����i�P")]
    public EquipItem accessory1;  // �����i1
    [Tooltip("�����i�Q")]
    public EquipItem accessory2;  // �����i2

    // ���̃��x���ɕK�v�Ȍo���l���v�Z���郁�\�b�h
    public int GetXPToNextLevel()
    {
        return Mathf.FloorToInt(baseXP * Mathf.Pow(growthRate, level - 1));
    }
}
