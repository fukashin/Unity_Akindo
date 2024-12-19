using UnityEngine;
using UnityEngine.SceneManagement;

public class TownSceneTransition : MonoBehaviour
{
    public string targetSceneName = "ŠX"; // ƒV[ƒ“–¼‚ğw’è

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ŠX" + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
