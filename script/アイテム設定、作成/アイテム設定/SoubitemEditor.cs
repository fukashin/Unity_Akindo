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
        // ID
        equipItem.ID = EditorGUILayout.IntField("iD", equipItem.ID);

        equipItem.小分類 = (ItemType2)EditorGUILayout.ObjectField("小分類", equipItem.小分類, typeof(ItemType2), false);
        if (equipItem.小分類 != null)
        {
            equipItem.大分類 = equipItem.小分類.大分類;
        }

        EditorGUI.BeginDisabledGroup(true);
        equipItem.大分類 = (ItemType)EditorGUILayout.ObjectField("大分類", equipItem.大分類, typeof(ItemType), false);
        EditorGUI.EndDisabledGroup();

        equipItem.相場 = EditorGUILayout.IntField("相場", equipItem.相場);
        equipItem.需要 = EditorGUILayout.IntField("需要", equipItem.需要);
        equipItem.商品画像 = (Sprite)EditorGUILayout.ObjectField("商品画像", equipItem.商品画像, typeof(Sprite), false);

        equipItem.攻撃力 = EditorGUILayout.IntField("攻撃力", equipItem.攻撃力);
        equipItem.防御力 = EditorGUILayout.IntField("防御力", equipItem.防御力);
        equipItem.耐久力 = EditorGUILayout.IntField("耐久力", equipItem.耐久力);

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
        if (GUILayout.Button("素材を追加"))
        {
            equipItem.必要素材.Add(new EquipItem.MaterialRequirement());
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(equipItem);
        }
    }
}
