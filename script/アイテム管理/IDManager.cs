using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IDManager", menuName = "Item/IDManager")]
public class IDManager : ScriptableObject
{
    public int currentMaxID = 0;                // 現在の最大ID
    public List<int> allItemIDs = new List<int>();  // すべてのアイテムIDを保持するリスト
    public List<BaseItem> アイテムリスト = new List<BaseItem>(); // アイテムリスト（BaseItemのインスタンス）

    // シーンが開始されたときにIDを読み込む（プレイヤーが終了した後でもIDが維持される）
    private void OnEnable()
    {
        // PlayerPrefsに保存されたIDがあれば読み込む
        if (PlayerPrefs.HasKey("CurrentMaxID"))
        {
            currentMaxID = PlayerPrefs.GetInt("CurrentMaxID");
        }
    }

    // シーンが終了したときにIDを保存
    private void OnDisable()
    {
        PlayerPrefs.SetInt("CurrentMaxID", currentMaxID);
        PlayerPrefs.Save();
    }

    // 新しいIDを取得
    public int GetNewID()
    {
        currentMaxID++;  // 新しいIDを増やす
        allItemIDs.Add(currentMaxID); // 新しいIDをリストに追加
        return currentMaxID; // 増えたIDを返す
    }

    // アイテムをリストに追加
    public void AddItem(BaseItem item)
    {
        アイテムリスト.Add(item);  // アイテムをリストに追加
    }

    // IDが重複しないか確認
    public bool IsIDUnique(int id)
    {
        return !allItemIDs.Contains(id); // IDが重複していなければtrue
    }

    // IDをリセットするメソッド（必要なときにのみ使用）
    public void ResetID()
    {
        currentMaxID = 0;
        allItemIDs.Clear(); // 一覧をリセット
        アイテムリスト.Clear(); // アイテムリストをリセット
    }

}
