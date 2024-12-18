using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float walkSpeed = 3.0f;  // 通常移動速度
    public float runSpeed = 6.0f;   // シフト押下時の走る速度
    private Animator animator;      // Animatorコンポーネント
    private Vector2 lastDirection;  // 最後に動いた方向

    void Start()
    {
        animator = GetComponent<Animator>(); // Animatorコンポーネントを取得
        lastDirection = Vector2.down;        // 初期値として下方向を設定
    }

    void Update()
    {
        // 入力取得
        float x = Input.GetAxisRaw("Horizontal"); // 横方向の入力
        float y = Input.GetAxisRaw("Vertical");   // 縦方向の入力

        // シフトキー押下状態を確認
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // 移動速度を設定
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // 移動方向を計算
        Vector2 moveDirection = new Vector2(x, y).normalized;

        // キャラクターの移動
        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * currentSpeed * Time.deltaTime;

        // アニメーション用のフラグ設定
        bool isMoving = moveDirection.magnitude > 0;

        if (isMoving)
        {
            // 移動している場合は向きを更新
            lastDirection = moveDirection;
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
        }

        // アニメーションの更新
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("LastMoveX", lastDirection.x);
        animator.SetFloat("LastMoveY", lastDirection.y);

        // 走るかどうかの状態をアニメーターに送信
        animator.SetBool("IsRunning", isRunning);
    }
}