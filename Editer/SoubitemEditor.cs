using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using soubiSystem;

[CustomEditor(typeof(EquipItem))]
public class EquipItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EquipItem equipItem = (EquipItem)target;

        // 初期化時にIDが0なら、IDManagerからIDを取得
        if (equipItem.ID == 0)
        {
            IDManager idManager = Resources.Load<IDManager>("IDManager");
            if (idManager != null)
            {
                equipItem.ID = idManager.GetNewID();

                idManager.AddItem(equipItem);  // アイテムをIDManagerに追加
                EditorUtility.SetDirty(equipItem);  // 変更をエディタに通知
                Debug.Log("変更がエディタに通知されました。");
            }
        }

        // ID表示
        equipItem.ID = EditorGUILayout.IntField("iD", equipItem.ID);

        // 商品名表示
        equipItem.商品名 = EditorGUILayout.TextField("商品名", equipItem.商品名);

        // 説明欄表示（BaseItemに定義された説明欄を表示）
        EditorGUILayout.LabelField("説明欄");
        equipItem.説明欄 = EditorGUILayout.TextArea(equipItem.説明欄, EditorStyles.textArea, GUILayout.Height(60));

        // アイテムタイプの設定
        equipItem.アイテムタイプ = (ItemType)EditorGUILayout.EnumPopup("アイテムタイプ", equipItem.アイテムタイプ);

        // 装備品カテゴリ設定（新しい設定）
        equipItem.category = (EquipItem.EquipCategory)EditorGUILayout.EnumPopup("装備カテゴリ", equipItem.category);

        // 装備カテゴリーが「武器」のときだけ、武器カテゴリを表示
        if (equipItem.category == EquipItem.EquipCategory.武器)
        {
            equipItem.weaponCategory = (WeaponCategory)EditorGUILayout.EnumPopup("武器カテゴリ", equipItem.weaponCategory);
        }

        // アイテムタイプに応じた設定を追加（例：装備品専用の設定）
        if (equipItem.アイテムタイプ == ItemType.装備)
        {
            equipItem.攻撃力 = EditorGUILayout.IntField("攻撃力", equipItem.攻撃力);
            equipItem.防御力 = EditorGUILayout.IntField("防御力", equipItem.防御力);
            equipItem.耐久力 = EditorGUILayout.IntField("耐久力", equipItem.耐久力);
        }

        // その他のフィールド設定
        equipItem.相場価格 = EditorGUILayout.IntField("相場価格", equipItem.相場価格);
        equipItem.需要 = EditorGUILayout.IntField("需要", equipItem.需要);
        equipItem.商品画像 = (Sprite)EditorGUILayout.ObjectField("商品画像", equipItem.商品画像, typeof(Sprite), false);

        // 必要素材リスト
        EditorGUILayout.LabelField("必要素材リスト", EditorStyles.boldLabel);
        if (equipItem.必要素材 == null)
            equipItem.必要素材 = new List<EquipItem.MaterialRequirement>();

        for (int i = 0; i < equipItem.必要素材.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            equipItem.必要素材[i].素材 = (BaseItem)EditorGUILayout.ObjectField("素材", equipItem.必要素材[i].素材, typeof(BaseItem), false);
            equipItem.必要素材[i].必要数 = EditorGUILayout.IntField("必要数", equipItem.必要素材[i].必要数);
            if (GUILayout.Button("削除", GUILayout.Width(60)))
            {
                equipItem.必要素材.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }

        // 素材追加ボタン
        if (GUILayout.Button("素材を追加"))
        {
            equipItem.必要素材.Add(new EquipItem.MaterialRequirement());
        }

        // IDリセットボタン
        if (GUILayout.Button("IDリセット"))
        {
            // ResourceフォルダにあるIDManagerを使う
            IDManager idManager = Resources.Load<IDManager>("IDManager");
            if (idManager != null)
            {
                idManager.RemoveItemID(equipItem);  // 現在のIDをIDManagerから削除
                equipItem.ResetID();  // アイテムのIDをリセット
                equipItem.ID = idManager.GetNewID();  // 新しいIDを設定
                EditorUtility.SetDirty(equipItem);  // 変更をエディタに通知
            }
        }

        // 変更があった場合、インスペクターに通知
        if (GUI.changed)
        {
            EditorUtility.SetDirty(equipItem);
        }
    }
}
