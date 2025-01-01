using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public CharacterData characterData; // ScriptableObjectの参照
    public Slider healthSlider;          // UIのSlider
    public Slider workpowerSlider;      // UIのSlider
    public TextMeshProUGUI 体力テキスト;
    public TextMeshProUGUI 行動力テキスト;
    public TextMeshProUGUI 攻撃力テキスト;
    public TextMeshProUGUI 防御力テキスト;

    void Start()
    {
        // 初期状態でバーを更新
        UpdateHealthBar();
        UpdateWorkpowerBar();

        // 初期の攻撃力と防御力を表示
        attackuptext();
        defencetext();
    }

    void Update()
    {
        //キャラステータス＋装備ステータス情報の更新 （確認用なので、装備のたびに更新してくれれば、これいらない）
        characterData.UpdateStatsFromEquipments();

        // 時間経過による行動力の回復
        characterData.UpdateWorkpower();

        // 定期的にバーを更新
        UpdateHealthBar();
        UpdateWorkpowerBar();

        // 攻撃力と防御力を更新
        attackuptext();
        defencetext();

    }

    // 体力バーを更新
    public void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)characterData.currentHP / characterData.maxHP;
        }
        if (体力テキスト != null)
        {
            体力テキスト.text = $"体力：{characterData.currentHP} / {characterData.maxHP}";
        }
    }

    // 行動力バーを更新
    public void UpdateWorkpowerBar()
    {
        if (workpowerSlider != null)
        {
            // 補間を使ってスライダーをスムーズに更新
            workpowerSlider.value = Mathf.Lerp(workpowerSlider.value,
                                               (float)characterData.currentWorkpower / characterData.workpower,
                                               Time.deltaTime * 10);
        }
        if (行動力テキスト != null)
        {
            行動力テキスト.text = $"行動力：{characterData.currentWorkpower} / {characterData.workpower}";
        }
    }
    // 攻撃力表示
    public void attackuptext()
    {
        攻撃力テキスト.text = $"攻撃力：{characterData.totalAttack}";
    }

    // 防御力表示
    public void defencetext()
    {
        防御力テキスト.text = $"防御力：{characterData.totalDefense}";
    }
}
