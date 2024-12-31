using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using TMPro;

[CreateAssetMenu(fileName = "ShopData", menuName = "Scriptable Objects/ShopData")]
public class ShopData : ScriptableObject
{
    [Header("店舗基本情報")]
    public string shopName; // 店舗名
    public int foundingDays; // 創立日数
    public int baseMaintenanceCost = 100; // 基本維持費
    public int totalMaintenanceCost;      // 総維持費
    public int popularity; // 人気度（0～100）
    public float money; //店舗資金　（プレイヤー資金）

    [Header("順位情報")]
    public int currentRank; // 暫定順位
    public int previousRank; // 前期順位

    [Header("財務情報")]
    public float currentRevenue; // 今期売上
    public float currentExpenditure; // 今期支出

    [Header("評価スコア")]
    public float totalScore; // 総合得点

    [Header("スケジュール")]
    public DateTime nextSettlementDate; // 次回決算日時

    //総維持費をテキスト表示する
    public TextMeshProUGUI maintenanceCostText;


    // 総合得点を計算するメソッド
    public void CalculateTotalScore()
    {
        totalScore = (currentRevenue - currentExpenditure) + popularity * 10 - totalMaintenanceCost;
    }

    // ランキングの更新
    public void UpdateRanking(int newRank)
    {
        previousRank = currentRank;
        currentRank = newRank;
    }

    // 決算日時を次回に更新
    public void UpdateSettlementDate()
    {
        nextSettlementDate = DateTime.Now.AddMonths(3); // 3ヶ月後
    }

    [Header("仲間リスト")]
    public List<NCData> teamMembers = new List<NCData>(); // 仲間リスト

    private void Start()
    {
        UpdateMaintenanceCost(); // 初期の維持費を計算
    }

    // 総維持費を更新するメソッド
    public void UpdateMaintenanceCost()
    {
        totalMaintenanceCost = baseMaintenanceCost;

        foreach (var member in teamMembers)
        {
            totalMaintenanceCost += member.MaintenanceCost;
        }

        EditorUtility.SetDirty(this);  // インスペクター更新

        if (maintenanceCostText != null)
        {
            maintenanceCostText.text = $"維持費: {totalMaintenanceCost} G";
        }

        Debug.Log($"店舗の総維持費: {totalMaintenanceCost}");
    }

    // 仲間を追加するメソッド
    public void AddTeamMember(NCData newMember)
    {
        teamMembers.Add(newMember);
        UpdateMaintenanceCost(); // 維持費を更新
        Debug.Log($"{newMember.NCName}が仲間に加わりました！");
    }

    // 仲間を削除するメソッド
    public void RemoveTeamMember(NCData memberToRemove)
    {
        teamMembers.Remove(memberToRemove);
        UpdateMaintenanceCost(); // 維持費を更新
        Debug.Log($"{memberToRemove.NCName}が仲間から外れました。");
    }

}

