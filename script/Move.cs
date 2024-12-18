using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float walkSpeed = 3.0f;  // �ʏ�ړ����x
    public float runSpeed = 6.0f;   // �V�t�g�������̑��鑬�x
    private Animator animator;      // Animator�R���|�[�l���g
    private Vector2 lastDirection;  // �Ō�ɓ���������

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator�R���|�[�l���g���擾
        lastDirection = Vector2.down;        // �����l�Ƃ��ĉ�������ݒ�
    }

    void Update()
    {
        // ���͎擾
        float x = Input.GetAxisRaw("Horizontal"); // �������̓���
        float y = Input.GetAxisRaw("Vertical");   // �c�����̓���

        // �V�t�g�L�[������Ԃ��m�F
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // �ړ����x��ݒ�
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // �ړ��������v�Z
        Vector2 moveDirection = new Vector2(x, y).normalized;

        // �L�����N�^�[�̈ړ�
        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * currentSpeed * Time.deltaTime;

        // �A�j���[�V�����p�̃t���O�ݒ�
        bool isMoving = moveDirection.magnitude > 0;

        if (isMoving)
        {
            // �ړ����Ă���ꍇ�͌������X�V
            lastDirection = moveDirection;
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
        }

        // �A�j���[�V�����̍X�V
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("LastMoveX", lastDirection.x);
        animator.SetFloat("LastMoveY", lastDirection.y);

        // ���邩�ǂ����̏�Ԃ��A�j���[�^�[�ɑ��M
        animator.SetBool("IsRunning", isRunning);
    }
}