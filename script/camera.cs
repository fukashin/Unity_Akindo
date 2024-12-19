using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // キャラクターのTransform
    public Vector3 offset;   // カメラのオフセット
    public float smoothSpeed = 0.125f; // 追従のスムーズさ

    void FixedUpdate() // LateUpdate → FixedUpdate に変更
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z; // Z座標を固定
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}