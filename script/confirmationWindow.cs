using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetSceneName; // 次のシーンの名前
    public GameObject confirmationWindow; // 確認ウィンドウのゲームオブジェクト
    public Transform player; // プレイヤーの Transform
    public Canvas canvas; // キャンバス

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (confirmationWindow != null && player != null && canvas != null)
            {
                // プレイヤーのワールド座標をスクリーン座標に変換
                Vector2 screenPos = Camera.main.WorldToScreenPoint(player.position);

                // 確認ウィンドウの位置を設定
                RectTransform rectTransform = confirmationWindow.GetComponent<RectTransform>();
                rectTransform.position = screenPos;

                confirmationWindow.SetActive(true); // 確認ウィンドウを表示
            }
        }
    }

    public void EnterTown()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName); // シーンをロード
        }
    }

    public void Cancel()
    {
        if (confirmationWindow != null)
        {
            confirmationWindow.SetActive(false); // 確認ウィンドウを非表示
        }
    }
}
