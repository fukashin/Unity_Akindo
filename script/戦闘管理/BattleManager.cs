using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public GameObject gridCellPrefab;  // グリッドセルのPrefab
    public RectTransform gridContainer;  // グリッドを配置する親のUIコンテナ

    [Header("グリッド設定")]
    public int gridWidth = 8;  // 横幅
    public int gridHeight = 4; // 縦幅
    private UnitData[,] battleGrid;  // グリッドはUnitData型で管理する

    [Header("キャラクター管理")]
    public PartyData playerPartyData;  // プレイヤーパーティのスクリプタブルオブジェクト
    public EnemyPartyData enemyPartyData;   // エネミーパーティのスクリプタブルオブジェクト

    private List<UnitData> turnOrder;  // ターン順リスト

    private int currentTurnIndex = 0;  // 現在のターン順序

    [Header("キャラクター位置管理")]
    public List<UnitData> playerParty = new List<UnitData>(); // プレイヤーと仲間のリスト（最大4人）
    public List<UnitData> enemyParty = new List<UnitData>();  // 敵キャラクターのリスト（最大6体）

    // グリッドセルの幅と高さ なんでフロート型で定義するのかよくわからん。上でイント型で定義してるじゃんか。
    private float cellWidth;
    private float cellHeight;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // 戦闘の初期化
    private void InitializeBattle()
    {
        Debug.Log("Battle Initialized");

        // 必要なら他の初期化処理をここに追加
    }

    // プレイヤーパーティとエネミーパーティのデータを設定
    public void SetPartyData(PartyData playerData, EnemyPartyData enemyData)
    {
        playerPartyData = playerData;
        enemyPartyData = enemyData;

        // プレイヤーパーティのメンバーを追加
        playerParty.Clear();  // 初期化（必要なら）
        playerParty.AddRange(playerPartyData.GetAllPartyMembers());

        // エネミーパーティのメンバーを追加
        enemyParty.Clear();  // 初期化（必要なら）
        enemyParty.AddRange(enemyData.GetAllPartyMembers());  // EnemyPartyDataからメンバーを追加

        // 各キャラクターの位置を設定
        InitializePositions();

        // 必要ならば戦闘の初期化なども行う
        InitializeBattle();
    }

    // キャラクターの位置を初期化する
    private void InitializePositions()
    {
        // プレイヤーパーティの位置設定
        for (int i = 0; i < playerParty.Count; i++)
        {
            UnitData character = playerParty[i];
            // プレイヤーの位置を決定（例: 最初は(0, i)に配置）
            Position position = new Position(0, i);
            playerPartyData.SetPosition(character, position);
        }

        // エネミーパーティの位置設定
        for (int i = 0; i < enemyParty.Count; i++)
        {
            UnitData character = enemyParty[i];
            // 敵の位置を決定（例: 最初は(7, i)に配置）
            Position position = new Position(gridWidth - 1, i);
            enemyPartyData.SetEnemyPosition(character, position);
        }
    }

    // グリッド初期化
    private void InitializeGrid()
    {
        battleGrid = new UnitData[gridWidth, gridHeight];

        // プレイヤーと仲間の初期配置
        for (int i = 0; i < playerParty.Count; i++)
        {
            Position position = playerPartyData.GetPosition(playerParty[i]);
            PlaceCharacter(playerParty[i], position.x, position.y, true);  // UIを更新しながら配置
        }

        // 敵の初期配置
        for (int i = 0; i < enemyParty.Count; i++)
        {
            Position position = enemyPartyData.GetEnemyPosition(enemyParty[i]);
            PlaceCharacter(enemyParty[i], position.x, position.y, true);  // UIを更新しながら配置
        }
    }


    // キャラクターを配置する
    public bool PlaceCharacter(UnitData character, int x, int y, bool updateUI = false)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight && battleGrid[x, y] == null)
        {
            battleGrid[x, y] = character;

            if (updateUI)
            {
                UpdateGridCellUI(x, y, character.Unitname); // UI更新
            }

            return true;
        }
        return false;
    }

    // グリッドセルのUIを更新するメソッド例
    void UpdateGridCellUI(int x, int y, string unitName)
    {
        // グリッドセルの位置を取得
        Transform cellTransform = gridContainer.GetChild(y * gridWidth + x);

        // TMP_Text を取得してテキストを更新
        TMP_Text cellText = cellTransform.GetComponentInChildren<TMP_Text>();
        if (cellText != null)
        {
            cellText.text = unitName;
        }
    }

    // グリッドを描画するメソッド
    void DrawGrid()
    {
        // グリッドを構築
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // グリッドセルを作成
                GameObject gridCell = Instantiate(gridCellPrefab, gridContainer);
                RectTransform rectTransform = gridCell.GetComponent<RectTransform>();

                // グリッドセルの位置設定
                rectTransform.anchoredPosition = new Vector2(x * cellWidth, -y * cellHeight); // 座標計算

                // 必要に応じてセルにIDや色を設定する
                // gridCell.GetComponent<Image>().color = ... など
            }
        }
    }

    // グリッド状態を取得
    public UnitData GetCharacterAtPosition(int x, int y)
    {
        return battleGrid[x, y];
    }

    // デバッグ用グリッド表示
    public void DisplayGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                UnitData unit = GetCharacterAtPosition(x, y);
                CreateGridCell(x, y, unit != null ? unit.Unitname : "Empty");
            }
        }
    }

    public void CreateGridCell(int x, int y, string unitName)
    {
        // グリッドセルの作成
        GameObject gridCell = Instantiate(gridCellPrefab, gridContainer);  // 既存のPrefabを使ってセルを作成

        // セルの位置を設定
        RectTransform rectTransform = gridCell.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x * cellWidth, -y * cellHeight);  // グリッドの位置に合わせて調整

        // テキストを設定（TMP対応）
        TextMeshProUGUI cellText = gridCell.GetComponentInChildren<TextMeshProUGUI>();
        if (cellText != null)
        {
            cellText.text = unitName;  // ユニット名を設定
        }
    }

    // ターン開始
    public void StartBattle()
    {
        turnOrder = new List<UnitData>();

        // プレイヤーと仲間、エネミーをターン順リストに追加
        turnOrder.AddRange(playerParty);
        turnOrder.AddRange(enemyParty);

        // アジリティ順にソート（同じ場合はランダム）
        turnOrder.Sort((a, b) => b.Unitagility.CompareTo(a.Unitagility));  // agilityが高い順

        // 初期ターン開始
        StartTurn();
    }

    // ターン処理
    private void StartTurn()
    {
        UnitData currentCharacter = turnOrder[currentTurnIndex];
        currentCharacter.TakeTurn();  // 現在のキャラクターがターンを取る

        // 次のターンに進む
        currentTurnIndex++;
        if (currentTurnIndex >= turnOrder.Count)
        {
            currentTurnIndex = 0;  // 一巡したら最初に戻る
        }

        // 次のターンを開始するための処理（例えば、UIを更新したり、次のキャラクターのアクションを表示したり）
    }

    // 戦闘終了の判定（全滅判定など）
    private void CheckBattleEnd()
    {
        // プレイヤーと敵キャラクターが倒されたかを確認
        bool allEnemiesDefeated = enemyParty.TrueForAll(enemy => enemy.IsDead);  // IsDead は UnitData クラスのフィールドと仮定
        bool allPlayersDefeated = playerParty.TrueForAll(player => player.IsDead);  // 同様にプレイヤーの死亡確認

        if (allEnemiesDefeated)
        {
            Debug.Log("プレイヤーの勝利！");
            // 勝利処理
        }
        else if (allPlayersDefeated)
        {
            Debug.Log("エネミーの勝利！");
            // 敗北処理
        }
    }

    // グリッドをクリア
    public void ClearGridPosition(int x, int y)
    {
        battleGrid[x, y] = null;
    }

    private void Start()
    {
        // グリッドセルの幅と高さを計算
        cellWidth = gridContainer.rect.width / gridWidth;
        cellHeight = gridContainer.rect.height / gridHeight;

        // グリッドを描画
        DrawGrid();
    }
}
