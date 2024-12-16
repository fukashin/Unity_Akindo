using UnityEngine;

public class BaseItem : ScriptableObject
{

    public int ID;          // プライマリキー
    public string 商品名;    // 商品名
    public ItemType 大分類;  //アイテム種別
    public ItemType2 小分類;  //種類
    public int 所持数;             // 所持数
    public int 価格;               // 価格
    public int 需要;               // 需要
    public float 熟練度;           // 熟練度
    public Sprite 商品画像;         // 商品画像
}
