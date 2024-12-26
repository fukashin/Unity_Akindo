using UnityEngine;
using TMPro; // TMP_Textを使用するための名前空間

public class InGameTimer : MonoBehaviour
{
    public int hour = 0;        // 現在の時刻（時）
    public int minute = 0;     // 現在の時刻（分）
    public float timeScale = 1f; // ゲーム内時間の進行速度 (1秒で1秒進む、60なら1秒で1分進む)

    private float timeAccumulator = 0f;

    public TMP_Text timeText2; // TMP_Textコンポーネントの参照

    void Update()
    {
        // ゲーム内時間を更新
        timeAccumulator += Time.deltaTime * timeScale;
        if (timeAccumulator >= 60f) // 60秒経過ごとに1分追加
        {
            timeAccumulator -= 60f;
            minute++;

            if (minute >= 60) // 分が60を超えたら時間を進める
            {
                minute = 0;
                hour++;

                if (hour >= 24) // 24時を超えたらリセット
                {
                    hour = 0;
                }
            }
        }
        timeText2.text = hour + "時:" + minute + "分";
    }

    public string GetFormattedTime()
    {
        // 時間を "HH:MM" の形式で返す
        return string.Format("{0:D2}時:{1:D2}分", hour, minute);
    }
}
