using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;  // UnityEditor名前空間をインポート
#endif

[CreateAssetMenu(fileName = "BaseItem", menuName = "Item/BaseItem")]
public class BaseItem : ScriptableObject
{
    public int ID;              // アイテムID
    public string 商品名;        // 商品名
    public string 説明欄;        // 説明欄
    public ItemType 大分類;      // アイテム種別
    public ItemType2 小分類;     // 種類
    public int 相場;            // 価格
    public int 需要;            // 需要
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
        // IDManagerがまだ設定されていない場合、シーンからIDManagerを取得する
        if (idManager == null)
        {
            // シーン内のIDManagerを検索
            idManager = FindObjectOfType<IDManager>();

            // シーン内にIDManagerがない場合、ResourcesからIDManagerを読み込む
            if (idManager == null)
            {
                idManager = Resources.Load<IDManager>("IDManager");

                if (idManager == null)
                {
                    Debug.LogError("IDManagerがシーン内またはResourcesから見つかりません！IDの管理ができません。");
                    return;  // ID生成を中止
                }
            }
        }

        // IDが未設定の場合のみ新しいIDを生成
        if (ID == 0)
        {
            ID = idManager.GetNewID(); // IDManager から新しいIDを取得

            // アイテムリストにアイテムを追加
            idManager.AddItem(this);    // アイテムをIDManagerに追加

            // エディタでIDManagerを更新
#if UNITY_EDITOR
            EditorUtility.SetDirty(idManager);  // IDManagerを更新
#endif
        }
    }

    // IDを取得するプロパティ
    public int GetID()
    {
        return ID;
    }
}
