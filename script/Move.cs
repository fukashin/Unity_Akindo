using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 3.0f;              // 通常の移動速度
    public float runMultiplier = 1.5f;      // 走るときの速度倍率
    private Rigidbody2D rb;                 // Rigidbody2D コンポーネント
    private Vector2 inputAxis;              // 入力の方向
    private bool isRunning;                 // 走っているかの状態
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       // Rigidbody2D の取得
        anim = GetComponent<Animator>();        // Animator の取得
    }

    void Update()
    {
        // 移動入力を取得
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");

        // シフトキーが押されているか確認
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // アニメーションの設定
        setAnim(inputAxis);
    }

    private void FixedUpdate()
    {
        // 移動速度を決定
        float currentSpeed = isRunning ? speed * runMultiplier : speed;

        // Rigidbody2D を使って移動
        rb.linearVelocity = inputAxis.normalized * currentSpeed;
    }

    public void setAnim(Vector2 vec2)
    {
        if (vec2 == Vector2.zero)
        {
            anim.speed = 0.0f;    // アニメーションを停止
            return;
        }

        anim.speed = 1.0f;        // アニメーションを再生
        anim.SetFloat("X", vec2.x);
        anim.SetFloat("Y", vec2.y);
    }
}
