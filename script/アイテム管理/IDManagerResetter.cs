using UnityEngine;
using UnityEngine.UI;

public class IDManagerResetter : MonoBehaviour
{
    public IDManager idManager;  // IDManagerの参照
    public Button resetButton;   // ボタンの参照

    // Start is called before the first frame update
    void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetID);  // ボタンにリセット処理を追加
        }
    }

    // IDをリセットするメソッド
    public void ResetID()
    {
        if (idManager != null)
        {
            idManager.ResetID();  // IDManagerのResetIDメソッドを呼び出し
            Debug.Log("IDがリセットされました。");
        }
        else
        {
            Debug.LogError("IDManagerが設定されていません。");
        }
    }
}
