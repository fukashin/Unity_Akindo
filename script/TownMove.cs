using UnityEngine;
using UnityEngine.SceneManagement;

public class TownSceneTransition : MonoBehaviour
{
    public string targetSceneName = "�X"; // �V�[�������w��

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�X" + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
