using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // �L�����N�^�[��Transform
    public Vector3 offset;   // �J�����̃I�t�Z�b�g
    public float smoothSpeed = 0.125f; // �Ǐ]�̃X���[�Y��

    void LateUpdate()
    {
        if (target != null)
        {
            // �L�����N�^�[�̈ʒu + �I�t�Z�b�g���v�Z
            Vector3 desiredPosition = target.position + offset;

            // ���݂̈ʒu����ڕW�ʒu�փX���[�Y�Ɉړ�
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // �J�����̈ʒu���X�V
            transform.position = smoothedPosition;
        }
    }
}