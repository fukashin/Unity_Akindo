using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // �L�����N�^�[��Transform
    public Vector3 offset;   // �J�����̃I�t�Z�b�g
    public float smoothSpeed = 0.125f; // �Ǐ]�̃X���[�Y��

    void FixedUpdate() // LateUpdate �� FixedUpdate �ɕύX
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z; // Z���W���Œ�
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}