using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // キャラクターのTransform テスト
    public Vector3 offset;   // カメラのオフセット
    public float smoothSpeed = 0.125f; // 追従のスムーズさ
    public float zoomAmount = 1f; // 視界を広げる量
    public float zoomSpeed = 0.1f; // 視界の変化速度
    public float maxZoom = 3f; // 最大視界の広がり（オフセットの最大値）
    public float resetSpeed = 0.1f; // 視界が中央に戻る速度
    private Vector3 previousPosition; // 前回のキャラの位置
    private Vector3 previousDirection; // 前回の移動方向

    void Start()
    {
        previousPosition = target.position; // 初期位置を記録
        previousDirection = Vector3.zero; // 初期移動方向をゼロに設定
    }

    void FixedUpdate() 
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z; // Z座標を固定

            // キャラの移動方向をチェック
            Vector3 moveDirection = target.position - previousPosition;

            // キャラの移動方向が変わった場合に視界を中央に戻す
            if (moveDirection != Vector3.zero && moveDirection != previousDirection)
            {
                // 移動方向が変わったら視界を中央に戻す
                offset = Vector3.Lerp(offset, Vector3.zero, resetSpeed);
            }

            // キャラが右に移動している場合、カメラの視界を広げる
            if (moveDirection.x > 0)
            {
                offset.x += zoomAmount * zoomSpeed; // 右に動いた時に視界を広げる
            }
            // キャラが左に移動している場合、カメラの視界を狭める
            else if (moveDirection.x < 0)
            {
                offset.x -= zoomAmount * zoomSpeed; // 左に動いた時に視界を狭める
            }

            // キャラが上に移動している場合、カメラの視界を広げる
            if (moveDirection.y > 0)
            {
                offset.y += zoomAmount * zoomSpeed; // 上に動いた時に視界を広げる
            }
            // キャラが下に移動している場合、カメラの視界を狭める
            else if (moveDirection.y < 0)
            {
                offset.y -= zoomAmount * zoomSpeed; // 下に動いた時に視界を狭める
            }

            // 視界の広がりを最大で制限
            offset.x = Mathf.Clamp(offset.x, -maxZoom, maxZoom);
            offset.y = Mathf.Clamp(offset.y, -maxZoom, maxZoom);

            // 前回の位置と移動方向を更新
            previousPosition = target.position;
            previousDirection = moveDirection;

            // 追従するカメラの位置
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    //シーン遷移したときに、ターゲット再設定
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
