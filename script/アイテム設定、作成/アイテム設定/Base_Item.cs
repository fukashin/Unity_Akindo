using UnityEngine;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;  // UnityEditor名前空間をインポート
#endif

public class BaseItem : ScriptableObject, IComparable<BaseItem>
{
    public int ID;              // アイテムID
    public string 商品名;        // 商品名
    public string 説明欄;        // 説明欄
    public ItemType 大分類;      // アイテム種別
    public ItemType2 小分類;     // 種類
    public int 相場;            // 価格
    public int 需要;            // 需要
    private bool 耐久値有無;　　//耐久値の有無
    public int 耐久値;　　　　　//耐久値
    public Sprite 商品画像;     // 商品画像

    [System.Serializable]
    public class MaterialRequirement
    {
        public BaseItem 素材;  // 素材アイテム
        public int 必要数;     // 素材必要数
    }

    public List<MaterialRequirement> 必要素材; // 必要な素材リスト

    private IDManager idManager;  // IDManagerの参照

    // 初期化時にユニークIDを生成
    private void OnEnable()
    {
        if (idManager == null)
        {
            idManager = FindFirstObjectByType<IDManager>();
            if (idManager == null)
            {
                idManager = Resources.Load<IDManager>("IDManager");
                if (idManager == null)
                {
                    Debug.LogError("IDManagerがシーン内またはResourcesから見つかりません！");
                    return;
                }
            }
        }

        if (ID == 0)
        {
            ID = idManager.GetNewID(); // IDManagerから新しいIDを取得
            idManager.AddItem(this);    // アイテムをIDManagerに追加

#if UNITY_EDITOR
            EditorUtility.SetDirty(idManager);  // IDManagerを更新
#endif
        }
    }

    // IComparableを実装
    public int CompareTo(BaseItem other)
    {
        if (other == null) return 1;

        // 例えばIDでソートする場合
        return this.ID.CompareTo(other.ID); // IDが小さい順に並べる
    }

    // IDを取得するプロパティ
    public int GetID()
    {
        return ID;
    }

    // IDをリセットするメソッド
    public void ResetID()
    {
        ID = 0;
    }
}
