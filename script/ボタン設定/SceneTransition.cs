using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに必要

public class SceneTransition2 : MonoBehaviour
{
    public string targetSceneName; // 次のシーンの名前

    // ボタンから呼び出す関数：シーン移動
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName); // シーンをロード
        }
    }
}
