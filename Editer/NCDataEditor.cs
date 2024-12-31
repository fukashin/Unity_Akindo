using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NCData))]
public class NCDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // デフォルトのインスペクター描画
        DrawDefaultInspector();

        // 対象のスクリプタブルオブジェクト
        NCData ncData = (NCData)target;

        // レイアウト開始
        EditorGUILayout.Space(); // 余白を追加
        GUILayout.BeginHorizontal();

        // 太字のスタイル
        GUIStyle boldStyle = new GUIStyle(EditorStyles.label);
        boldStyle.fontStyle = FontStyle.Bold;

        // ラベルと値の表示
        EditorGUILayout.LabelField("維持費", boldStyle, GUILayout.Width(50)); // 太字ラベル
        EditorGUILayout.LabelField(ncData.MaintenanceCost.ToString(), GUILayout.Width(50)); // 値

        GUILayout.EndHorizontal();
        EditorGUILayout.Space(); // 余白を追加
    }
}
