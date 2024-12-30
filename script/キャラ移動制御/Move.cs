using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaController : MonoBehaviour
{
    [Header("通常の移動速度")]
    public float speed = 3.0f;              // 通常の移動速度
    [Header("走るときの速度倍率")]
    public float runMultiplier = 1.5f;      // 走るときの速度倍率
    private Rigidbody2D rb;                 // Rigidbody2D コンポーネント
    private Vector2 inputAxis;              // 入力の方向
    private bool isRunning;                 // 走っているかの状態
    private Animator anim;                  // Animator

    [Header("エンカウントまでの距離")]
    public float encounterDistance = 10f;   // エンカウントまでに進む距離
    private float totalDistance = 0f;       // 総移動距離

    [Header("エンカウント確率")]
    [SerializeField, Range(0f, 1f)]         // インスペクターで調整可能、0〜1の範囲
    private float encounterProbability = 0.5f; // エンカウントの確率

    [Header("エンカウント出現モンスター")]
    public EnemyData[] enemyPool; // エンカウント時に選ばれる敵のプール

    [Header("キャラ")]
    public CharacterData playerData; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // Rigidbody2D の取得
        anim = GetComponent<Animator>();    // Animator の取得

        // Rigidbody2D 設定の確認
        rb.linearDamping = 0f;              // 抵抗値を無効化
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        // 移動入力を取得
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");

        // 小さな入力を無視
        if (inputAxis.magnitude < 0.1f)
        {
            inputAxis = Vector2.zero;
        }

        // シフトキーが押されているか確認
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // アニメーションの設定
        setAnim(inputAxis);

        // 総移動距離を加算
        totalDistance += inputAxis.magnitude * Time.deltaTime * (isRunning ? runMultiplier : 1);

        // エンカウントチェック
        if (totalDistance >= encounterDistance)
        {
            TriggerEncounter();
        }
    }

    void FixedUpdate()
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

    private CharacterData GetPlayerData()
    {
        return playerData;
    }

    void TriggerEncounter()
    {
        totalDistance = 0f; // 総移動距離をリセット

        // エンカウント判定（ランダム性を追加）
        if (Random.Range(0f, 1f) <= encounterProbability) // インスペクターで調整可能
        {
            // 敵をランダムに選択
            if (enemyPool.Length > 0)
            {

                CharacterData player = GetPlayerData();
                int randomIndex = Random.Range(0, enemyPool.Length);
                EnemyData selectedEnemy = enemyPool[randomIndex];

                // 選ばれた敵データ、キャラデータを戦闘シーンへ設定
                BattleManager.Instance.SetEnemyData(selectedEnemy);
                BattleManager.Instance.SetPlayerData(playerData);
                SceneManager.LoadScene("戦闘シーン");
            }
            else
            {
                Debug.LogError("敵のプールが空です");
            }
        }
        else
        {
            // エンカウントしなかった場合
            Debug.Log("エンカウントなし");
        }
    }
}
