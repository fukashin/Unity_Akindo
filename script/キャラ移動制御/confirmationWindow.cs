using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���؂�ւ��ɕK�v

public class SceneTransition : MonoBehaviour
{
    public string targetSceneName; // ���̃V�[���̖��O
    public GameObject confirmationWindow; // �m�F�E�B���h�E�̃Q�[���I�u�W�F�N�g
    private bool isPlayerInTrigger = false; // �v���C���[���g���K�[���ɂ��邩�ǂ���

    private void Start()
    {
        if (confirmationWindow != null)
        {
            confirmationWindow.SetActive(false); // �Q�[���J�n���Ɋm�F�E�B���h�E���\���ɂ���
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[���g���K�[���ɓ������ꍇ
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // �g���K�[���ɂ����Ԃ��L�^
            if (confirmationWindow != null)
            {
                confirmationWindow.SetActive(true); // �m�F�E�B���h�E��\��
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �v���C���[���g���K�[���痣�ꂽ�ꍇ
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // �g���K�[���痣�ꂽ��Ԃ��L�^
            if (confirmationWindow != null)
            {
                confirmationWindow.SetActive(false); // �m�F�E�B���h�E���\��
            }
        }
    }

    // �{�^������Ăяo���֐��F�X�ɓ���
    public void EnterTown()
    {
        if (isPlayerInTrigger && !string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName); // �V�[�������[�h
        }
    }

    // �{�^������Ăяo���֐��F�L�����Z��
    public void Cancel()
    {
        if (confirmationWindow != null)
        {
            confirmationWindow.SetActive(false); // �m�F�E�B���h�E���\��
        }
    }
}
