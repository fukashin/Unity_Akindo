using UnityEngine;
using System.Collections.Generic;

public class BaseItem : ScriptableObject
{

    public int ID;          // プライマリキー
    public string 商品名;    // 商品名
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
}
