using UnityEngine;
using UnityEngine.SceneManagement;

public class TownSceneTransition : MonoBehaviour
{
    public string targetSceneName = "街"; // シーン名を指定

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("街" + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
