using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // キャラクターのTransform
    public Vector3 offset;   // カメラのオフセット
    public float smoothSpeed = 0.125f; // 追従のスムーズさ

    void LateUpdate()
    {
        if (target != null)
        {
            // キャラクターの位置 + オフセットを計算
            Vector3 desiredPosition = target.position + offset;

            // 現在の位置から目標位置へスムーズに移動
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // カメラの位置を更新
            transform.position = smoothedPosition;
        }
    }
}