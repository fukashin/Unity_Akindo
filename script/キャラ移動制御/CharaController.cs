using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class CharaController : MonoBehaviour
{

    public static CharaController Instance { get; private set; }

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
    public EnemyPartyData[] enemyPartyPool; // エンカウント時に選ばれる敵パーティのプール

    [Header("プレイヤーパーティデータ")]
    public PartyData playerPartyData;      // プレイヤーパーティデータの設定

    [HideInInspector]
    public EnemyPartyData enemyPartyData;

    [HideInInspector]
    public string 街北平野; // キャラが表示されるシーン名

    // インスタンス初期化
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;  // インスタンスを設定
            DontDestroyOnLoad(gameObject);  // シーン遷移後も保持
        }
        else
        {
            Destroy(gameObject);  // 既存のインスタンスがあれば、このオブジェクトを削除
        }
    }

    private void Start()
    {

        // シーンが変更された時に呼ばれるイベントに登録 フィールドシーン以外では非表示になるように。
        SceneManager.sceneLoaded += OnSceneLoaded;

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

    //ほかのシーンでキャラが歩くの防止用
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーン名を確認して表示/非表示を切り替える
        gameObject.SetActive(scene.name == 街北平野);

        // 新しいシーンの読み込み後に複製して保持
        if (scene.name != "街北平野")
        {
            // 現在のオブジェクトを複製
            GameObject duplicate = Instantiate(gameObject);

            // 複製されたオブジェクトを新しいシーンに移動
            DontDestroyOnLoad(duplicate);

            // 複製オブジェクトのカメラ追従を設定
            //Camera.main.GetComponent<CameraFollow>().SetTarget(duplicate.transform);

            // 元のオブジェクトは新しいシーンには不要なので削除
            Destroy(gameObject);
        }
        else
        {
            // 街北平野のシーンではカメラ追従を元に戻す
            Camera.main.GetComponent<CameraFollow>().SetTarget(gameObject.transform);
        }

    }

    //上の解除用
    void OnDestroy()
    {
        // イベント登録解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

    void TriggerEncounter()
    {
        totalDistance = 0f; // 総移動距離をリセット

        // エンカウント判定（ランダム性を追加）
        if (Random.Range(0f, 1f) <= encounterProbability) // インスペクターで調整可能
        {
            // エネミーパーティをランダムに選択
            if (enemyPartyPool.Length > 0)
            {
                int randomIndex = Random.Range(0, enemyPartyPool.Length);
                EnemyPartyData selectedEnemyParty = enemyPartyPool[randomIndex];

                // 選ばれたエネミーパーティのメンバーをログに出力（デバッグ用）
                foreach (var enemy in selectedEnemyParty.enemies)
                {
                    Debug.Log("Encountered: " + enemy.enemyName);
                }

                CharaController.Instance.playerPartyData = playerPartyData;
                CharaController.Instance.enemyPartyData = selectedEnemyParty;

                // 戦闘シーンへ移行
                SceneManager.LoadScene("戦闘シーン");
            }
            else
            {
                Debug.LogError("エネミーパーティのプールが空です");
            }
        }
        else
        {
            // エンカウントしなかった場合
            Debug.Log("エンカウントなし");
        }
    }
}
