using UnityEngine;

public class BaseItem : ScriptableObject
{

    public int ID;          // プライマリキー
    public string 商品名;    // 商品名
    public ItemType 大分類;  //アイテム種別
    public ItemType2 小分類;  //種類
    public int 相場;               // 価格
    public int 需要;               // 需要
    public Sprite 商品画像;         // 商品画像
}
