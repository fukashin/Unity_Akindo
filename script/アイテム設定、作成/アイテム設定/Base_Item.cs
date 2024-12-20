using UnityEngine;
using System.Collections.Generic;

public class BaseItem : ScriptableObject
{

    public int ID;          // プライマリキー
    public string 商品名;    // 商品名
    public string 説明欄; 
    public ItemType 大分類;  //アイテム種別
    public ItemType2 小分類;  //種類
    public int 相場;               // 価格
    public int 需要;               // 需要
    public Sprite 商品画像;         // 商品画像

    [System.Serializable]
    public class MaterialRequirement
    {
        public BaseItem 素材;      // 素材アイテム
        public int 必要数;         // 素材必要数
    }

    public List<MaterialRequirement> 必要素材; // 必要な素材リスト

    // 初期化時にユニークIDを生成
    private void OnEnable()
    {
        if (ID == 0) // 既にIDが設定されていない場合のみ生成
        {
            ID = GenerateUniqueID();
        }
    }

    // ユニークIDを生成する関数
    private int GenerateUniqueID()
    {
        return System.Guid.NewGuid().GetHashCode();
    }

    // IDを取得するプロパティ
    public int GetID()
    {
        return ID;
    }
}
