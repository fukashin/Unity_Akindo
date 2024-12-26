using UnityEngine;
using TMPro; // TMP_Textを使用するための名前空間
using System;

public class RealTimeUI : MonoBehaviour
{
    public TMP_Text timeText; // TMP_Textコンポーネントの参照

    void Update()
    {
        // 現在の年月日と時間を取得して表示
        timeText.text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }
}
