using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(EquipItem))]
public class EquipItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EquipItem equipItem = (EquipItem)target;

        equipItem.商品名 = EditorGUILayout.TextField("商品名", equipItem.商品名);

        // ID表示
        equipItem.ID = EditorGUILayout.IntField("iD", equipItem.ID);

        // 小分類、大分類の設定
        equipItem.小分類 = (ItemType2)EditorGUILayout.ObjectField("小分類", equipItem.小分類, typeof(ItemType2), false);
        if (equipItem.小分類 != null)
        {
            equipItem.大分類 = equipItem.小分類.大分類;
        }

        EditorGUI.BeginDisabledGroup(true);
        equipItem.大分類 = (ItemType)EditorGUILayout.ObjectField("大分類", equipItem.大分類, typeof(ItemType), false);
        EditorGUI.EndDisabledGroup();

        // その他のフィールド設定
        equipItem.相場 = EditorGUILayout.IntField("相場", equipItem.相場);
        equipItem.需要 = EditorGUILayout.IntField("需要", equipItem.需要);
        equipItem.商品画像 = (Sprite)EditorGUILayout.ObjectField("商品画像", equipItem.商品画像, typeof(Sprite), false);

        // 装備に関する設定
        equipItem.攻撃力 = EditorGUILayout.IntField("攻撃力", equipItem.攻撃力);
        equipItem.防御力 = EditorGUILayout.IntField("防御力", equipItem.防御力);
        equipItem.耐久力 = EditorGUILayout.IntField("耐久力", equipItem.耐久力);

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
            //ResourceフォルダにあるIDManagerを使う
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
