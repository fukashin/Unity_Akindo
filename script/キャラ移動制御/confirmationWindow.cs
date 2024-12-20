using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに必要

public class SceneTransition : MonoBehaviour
{
    public string targetSceneName; // 次のシーンの名前
    public GameObject confirmationWindow; // 確認ウィンドウのゲームオブジェクト
    private bool isPlayerInTrigger = false; // プレイヤーがトリガー内にいるかどうか

    private void Start()
    {
        if (confirmationWindow != null)
        {
            confirmationWindow.SetActive(false); // ゲーム開始時に確認ウィンドウを非表示にする
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーがトリガー内に入った場合
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // トリガー内にいる状態を記録
            if (confirmationWindow != null)
            {
                confirmationWindow.SetActive(true); // 確認ウィンドウを表示
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // プレイヤーがトリガーから離れた場合
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // トリガーから離れた状態を記録
            if (confirmationWindow != null)
            {
                confirmationWindow.SetActive(false); // 確認ウィンドウを非表示
            }
        }
    }

    // ボタンから呼び出す関数：街に入る
    public void EnterTown()
    {
        if (isPlayerInTrigger && !string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName); // シーンをロード
        }
    }

    // ボタンから呼び出す関数：キャンセル
    public void Cancel()
    {
        if (confirmationWindow != null)
        {
            confirmationWindow.SetActive(false); // 確認ウィンドウを非表示　テスト
        }
    }
}
