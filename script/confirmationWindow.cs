using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetSceneName; // ���̃V�[���̖��O
    public GameObject confirmationWindow; // �m�F�E�B���h�E�̃Q�[���I�u�W�F�N�g
    public Transform player; // �v���C���[�� Transform
    public Canvas canvas; // �L�����o�X

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (confirmationWindow != null && player != null && canvas != null)
            {
                // �v���C���[�̃��[���h���W���X�N���[�����W�ɕϊ�
                Vector2 screenPos = Camera.main.WorldToScreenPoint(player.position);

                // �m�F�E�B���h�E�̈ʒu��ݒ�
                RectTransform rectTransform = confirmationWindow.GetComponent<RectTransform>();
                rectTransform.position = screenPos;

                confirmationWindow.SetActive(true); // �m�F�E�B���h�E��\��
            }
        }
    }

    public void EnterTown()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName); // �V�[�������[�h
        }
    }

    public void Cancel()
    {
        if (confirmationWindow != null)
        {
            confirmationWindow.SetActive(false); // �m�F�E�B���h�E���\��
        }
    }
}
