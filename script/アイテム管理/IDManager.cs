using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;  // UnityEditor名前空間をインポート
#endif

//[CreateAssetMenu(fileName = "IDManager", menuName = "Item/IDManager")]
public class IDManager : ScriptableObject
{
    public int currentMaxID = 0;                // 現在の最大ID
    public List<int> allItemIDs = new List<int>();  // すべてのアイテムIDを保持するリスト
    public List<BaseItem> アイテムリスト = new List<BaseItem>(); // アイテムリスト（BaseItemのインスタンス）
    public static IDManager idManager; // 静的なインスタンスを保持するフィールド

    // IDManagerがシーンで最初に呼ばれたときに初期化処理
    private void OnEnable()
    {
        if (idManager == null)
        {
            idManager = Resources.Load<IDManager>("IDManager");
            if (idManager == null)
            {
                Debug.LogError("IDManager.asset が Resources フォルダに存在しません！");
            }
        }
    }

    // シーンが終了したときにIDを保存
    private void OnDisable()
    {
        // PlayerPrefsへの保存を削除（IDはIDManagerで管理する）
        // アイテムリストをリセットして、ResourcesからすべてのBaseItemを取得
        //アイテムリスト = Resources.FindObjectsOfTypeAll<BaseItem>().ToList();
        Debug.Log($"アイテムリストのサイズ: {アイテムリスト.Count}");
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
        アイテムリスト.Sort();
        //item.ID = GetNewID();  // アイテムに新しいIDを設定
    }

    // IDが重複しないか確認
    public bool IsIDUnique(int id)
    {
        return !allItemIDs.Contains(id); // IDが重複していなければtrue
    }

    // アイテムのIDを削除
    public void RemoveItemID(BaseItem item)
    {
        if (item != null)
        {
            int oldID = item.ID;  // IDを一時的に保存
            アイテムリスト.Remove(item);  // アイテムリストから削除
            allItemIDs.Remove(oldID);  // allItemIDsからIDを削除   
            Debug.Log("アイテムリストからアイテムを削除しました。");

            item.ResetID();  // アイテムIDをリセット

            // リセット後に再度新しいIDを設定（または古いIDを復元）
            item.ID = oldID;  // 古いIDを復元（または再割り当て）

            EditorUtility.SetDirty(item);  // 変更をエディタに通知
        }
    }

    //allItemIDsの要素数がおかしいとき用
    public void EnsureAllItemIDs()
    {
        // アイテムリストを確認し、allItemIDs の要素数が足りていない場合は追加する
        foreach (var item in アイテムリスト)
        {
            if (item != null && !allItemIDs.Contains(item.ID))
            {
                allItemIDs.Add(item.ID); // IDを追加
            }
        }

        // 必要に応じてallItemIDsをID順に並べ替え
        allItemIDs = allItemIDs.OrderBy(id => id).ToList();
        Debug.Log("allItemIDsがアイテムIDに基づいて更新されました。");
    }


    // IDをリセットするメソッド（必要なときにのみ使用）
    public void ResetID()
    {
#if UNITY_EDITOR
    Debug.Log("IDのリセットを開始");

    // アイテムIDを一時的に保存するためのディクショナリ
    Dictionary<BaseItem, int> itemIDBackup = new Dictionary<BaseItem, int>();

    // すべてのBaseItemを検索して、そのIDをバックアップ
    string[] guids = AssetDatabase.FindAssets("t:BaseItem");
    List<BaseItem> foundItems = guids
        .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
        .Select(path => AssetDatabase.LoadAssetAtPath<BaseItem>(path))
        .Where(item => item != null) // nullチェック
        .ToList();

    currentMaxID = アイテムリスト.Max(item => item.ID);  // 現在のIDリストの最大値を設定
    if (currentMaxID < 0) currentMaxID = 0;  // 最大IDが負の値の場合は0に設定

    allItemIDs.Clear(); // allItemIDsをクリア
    アイテムリスト.Clear(); // アイテムリストをクリア

    // すでに使われているIDを記録
    HashSet<int> usedIDs = new HashSet<int>(allItemIDs);

    // 欠番をチェック
    List<int> availableIDs = Enumerable.Range(1, usedIDs.Count + foundItems.Count)
                                        .Where(id => !usedIDs.Contains(id))
                                        .ToList();

    int nextAvailableIndex = 0;

    // IDをリセットし、バックアップしたIDを再設定
    foreach (var item in foundItems)
    {
        // リセット前のIDをバックアップ
        itemIDBackup[item] = item.ID;

        // アイテムのIDをリセット
        item.ResetID();

        // 利用可能なIDがあれば、再利用する
        if (nextAvailableIndex < availableIDs.Count)
        {
            item.ID = availableIDs[nextAvailableIndex++];
        }
        else
        {
            // 次の新しいIDを割り当て
            item.ID = GetNewID();
            // アイテムIDリストに追加
            allItemIDs.Add(item.ID); 

        }

        // 最大IDを更新
        if (item.ID > currentMaxID)
        {
            currentMaxID = item.ID;
        }

        アイテムリスト.Add(item);

        // 変更をエディターに通知
        EditorUtility.SetDirty(item);
    }

    // リストをID順にソート
    アイテムリスト = アイテムリスト.OrderBy(item => item.ID).ToList();
    
    //allItemIDsがアイテムIDに基づいて更新される
    EnsureAllItemIDs();  // allItemIDsにIDを追加

    // IDManager自体の変更をエディタに通知
    EditorUtility.SetDirty(this);

    // アセットを保存しエディタを更新
    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();

    Debug.Log("IDがリセットされ、再割り当てされました。");
#else
        Debug.LogError("このメソッドはエディタでのみ動作します。");
#endif
    }

}