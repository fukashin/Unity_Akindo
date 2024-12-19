using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // �L�����N�^�[��Transform
    public Vector3 offset;   // �J�����̃I�t�Z�b�g
    public float smoothSpeed = 0.125f; // �Ǐ]�̃X���[�Y��
    public float zoomAmount = 1f; // ���E���L�����
    public float zoomSpeed = 0.1f; // ���E�̕ω����x
    public float maxZoom = 3f; // �ő压�E�̍L����i�I�t�Z�b�g�̍ő�l�j
    public float resetSpeed = 0.1f; // ���E�������ɖ߂鑬�x
    private Vector3 previousPosition; // �O��̃L�����̈ʒu
    private Vector3 previousDirection; // �O��̈ړ�����

    void Start()
    {
        previousPosition = target.position; // �����ʒu���L�^
        previousDirection = Vector3.zero; // �����ړ��������[���ɐݒ�
    }

    void FixedUpdate() // FixedUpdate �̂܂�
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z; // Z���W���Œ�

            // �L�����̈ړ��������`�F�b�N
            Vector3 moveDirection = target.position - previousPosition;

            // �L�����̈ړ��������ς�����ꍇ�Ɏ��E�𒆉��ɖ߂�
            if (moveDirection != Vector3.zero && moveDirection != previousDirection)
            {
                // �ړ��������ς�����王�E�𒆉��ɖ߂�
                offset = Vector3.Lerp(offset, Vector3.zero, resetSpeed);
            }

            // �L�������E�Ɉړ����Ă���ꍇ�A�J�����̎��E���L����
            if (moveDirection.x > 0)
            {
                offset.x += zoomAmount * zoomSpeed; // �E�ɓ��������Ɏ��E���L����
            }
            // �L���������Ɉړ����Ă���ꍇ�A�J�����̎��E�����߂�
            else if (moveDirection.x < 0)
            {
                offset.x -= zoomAmount * zoomSpeed; // ���ɓ��������Ɏ��E�����߂�
            }

            // �L��������Ɉړ����Ă���ꍇ�A�J�����̎��E���L����
            if (moveDirection.y > 0)
            {
                offset.y += zoomAmount * zoomSpeed; // ��ɓ��������Ɏ��E���L����
            }
            // �L���������Ɉړ����Ă���ꍇ�A�J�����̎��E�����߂�
            else if (moveDirection.y < 0)
            {
                offset.y -= zoomAmount * zoomSpeed; // ���ɓ��������Ɏ��E�����߂�
            }

            // ���E�̍L������ő�Ő���
            offset.x = Mathf.Clamp(offset.x, -maxZoom, maxZoom);
            offset.y = Mathf.Clamp(offset.y, -maxZoom, maxZoom);

            // �O��̈ʒu�ƈړ��������X�V
            previousPosition = target.position;
            previousDirection = moveDirection;

            // �Ǐ]����J�����̈ʒu
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
