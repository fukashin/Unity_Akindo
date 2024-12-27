using UnityEngine;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;  // UnityEditor名前空間をインポート
#endif

public enum ItemType
{
    通常, //素材・消耗品系アイテム
    装備, //装備品（武器・防具・装飾品）系アイテム
    その他 //その他（必要に応じて拡張）
}

public class BaseItem : ScriptableObject, IComparable<BaseItem>
{
    public int ID;              // アイテムID
    public string 商品名;        // 商品名
    public string 説明欄;        // 説明欄
    public ItemType アイテムタイプ;
    public int 相場価格;            // 価格
    public int 需要;            // 需要
    private bool 耐久値有無;　　//耐久値の有無
    public int 耐久値;　　　　　//耐久値
    public Sprite 商品画像;     // 商品画像
    public int 在庫; //在庫数
    public int 所持数; // 所持数を追加

    public BaseItem Initialize(string 商品名, int 在庫, int 相場価格, int 需要)
    {
        this.商品名 = 商品名;
        this.在庫 = 在庫;
        this.相場価格 = 相場価格;
        this.需要 = 需要;
        return this;
    }

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
        // Resources.Load で IDManager アセットを読み込む
        if (idManager == null)
        {
            idManager = Resources.Load<IDManager>("IDManager");
            if (idManager == null)
            {
                Debug.LogError("IDManagerアセットがResourcesフォルダ内に見つかりません！" +
                               "Assets/Resources/IDManager.asset が存在することを確認してください。");
                return;
            }
        }

        // IDが未設定の場合、新しいIDを生成
        if (ID == 0)
        {
            ID = idManager.GetNewID(); // IDManagerから新しいIDを取得
            idManager.AddItem(this);   // アイテムをIDManagerに追加

#if UNITY_EDITOR
        // エディターで変更があれば保存
        EditorUtility.SetDirty(idManager);  
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

// ShopItemはBaseItemクラスの外に出す
[CreateAssetMenu(fileName = "ShopItem", menuName = "Inventory/陳列棚アイテム")]
public class ShopItem : BaseItem
{
    public int 売値;
    public int 在庫数;
}
