using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    [Header("グリッド設定")]
    public int gridWidth = 8;  // 横幅
    public int gridHeight = 4; // 縦幅
    private UnitData[,] battleGrid;  // グリッドはUnitData型で管理する

    [Header("キャラクター管理")]
    public PartyData playerPartyData;  // プレイヤーパーティのスクリプタブルオブジェクト
    public EnemyPartyData enemyPartyData;   // エネミーパーティのスクリプタブルオブジェクト

    public List<UnitData> playerParty = new List<UnitData>(); //プレイヤーパーティ
    public List<UnitData> enemyParty = new List<UnitData>();　//エネミーパーティ

    private List<UnitData> allUnits = new List<UnitData>();  // プレイヤー・エネミー全キャラクターのリスト
    private float timePerTurn = 1.0f; // １ターンの時間（基本的な時間管理）

    private TurnOrderUI turnOrderUI;
    private List<Vector2Int> validMovePositions = new List<Vector2Int>(); // 移動可能な位置

    [SerializeField]
    private BattleGridUI battleGridUI;

    private UnitData currentUnit;


    private void Start()
    {
        // CharaController からデータを取得
        if (CharaController.Instance != null)
        {
            Initialize(CharaController.Instance.playerPartyData, CharaController.Instance.enemyPartyData);
        }
        else
        {
            Debug.LogError("CharaController.Instance が存在しません");
        }

        allUnits.AddRange(playerParty);
        allUnits.AddRange(enemyParty);

        turnOrderUI = Object.FindAnyObjectByType<TurnOrderUI>();
        if (turnOrderUI == null)
        {
            Debug.LogError("TurnOrderUI がシーン内に見つかりません");
        }
        else
        {
            List<UnitData> sortedTurnOrder = GetTurnOrder();  // ターン順を取得
            UpdateTurnOrderUI(sortedTurnOrder);
        }

        // すべてのユニットの次に行動するまでのカウントを俊敏性に基づいて初期化
        foreach (var unit in allUnits)
        {
            unit.nextActionTime = 0f;
        }

        // グリッドとバトル初期化
        InitializeGrid();
        InitializePositions();
        OnEnterBattleScene();

        // グリッドUI更新
        Object.FindAnyObjectByType<BattleGridUI>()?.UpdateGridUI(battleGrid);
    }

    public void Initialize(PartyData playerData, EnemyPartyData enemyData)
    {
        playerPartyData = playerData;
        enemyPartyData = enemyData;

        playerParty.Clear();
        enemyParty.Clear();

        if (playerData != null)
            playerParty.AddRange(playerData.GetAllPartyMembers());

        if (enemyData != null)
            enemyParty.AddRange(enemyData.GetAllPartyMembers());
    }

    private void InitializeGrid()
    {
        battleGrid = new UnitData[gridWidth, gridHeight];
    }

    private void InitializePositions()
    {
        // プレイヤーキャラクター配置
        for (int i = 0; i < playerParty.Count; i++)
        {
            UnitData character = playerParty[i];
            Position position = playerPartyData.GetPosition(character); // PartyDataから位置を取得
            PlaceCharacter(character, position.x, position.y);  // キャラクターを配置
        }

        // エネミーキャラクター配置
        for (int i = 0; i < enemyParty.Count; i++)
        {
            UnitData enemy = enemyParty[i];
            Position position = enemyPartyData.GetEnemyPosition(enemy); // EnemyPartyDataから位置を取得
            PlaceCharacter(enemy, position.x, position.y);  // エネミーを配置
        }

        // グリッドUIを更新
        Object.FindAnyObjectByType<BattleGridUI>()?.UpdateGridUI(battleGrid);
    }

    public bool PlaceCharacter(UnitData character, int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight && battleGrid[x, y] == null)
        {
            battleGrid[x, y] = character;
            return true;
        }
        return false;
    }

    void Update()
    {
        // 戦闘終了チェック
        if (!IsBattleOngoing())
        {
            CheckBattleEnd();
            return;
        }

        // 現在のユニットの処理
        if (currentUnit != null)
        {
            if (currentUnit.unitType == UnitType.Player)
            {
                currentUnit.ProcessPlayerTurn(battleGrid);
            }
            else if (currentUnit.unitType == UnitType.Enemy)
            {
                currentUnit.TakeTurn(battleGrid);
            }
        }

        // ユニット行動進行
        foreach (var unit in allUnits)
        {
            if (!unit.IsDead)
            {
                unit.nextActionTime += unit.Unitagility * timePerTurn;
            }
        }

        // 次の行動ユニットを取得
        UnitData nextUnit = GetNextUnitToAct();

        if (nextUnit != null && nextUnit.nextActionTime >= 100f)
        {
            currentUnit = nextUnit;  // 次のユニットを設定
            validMovePositions = GetValidMovePositions(currentUnit);  // 移動可能範囲を更新
            nextUnit.nextActionTime = 0f;  // 次のアクションタイムをリセット
            UpdateTurnOrderUI(GetTurnOrder());  // ターン順 UI を更新
        }
    }

    private Vector2Int WorldToGridPos(Vector2 worldPos)
    {
        // 世界座標をグリッド座標に変換
        return new Vector2Int(Mathf.FloorToInt(worldPos.x), Mathf.FloorToInt(worldPos.y));
    }

    public List<Vector2Int> GetValidMovePositions(UnitData unit)
    {
        List<Vector2Int> validPositions = new List<Vector2Int>();
        int moveRange = unit.Unitagility;  // 仮の移動範囲（アジリティに基づいて調整）

        for (int x = -moveRange; x <= moveRange; x++)
        {
            for (int y = -moveRange; y <= moveRange; y++)
            {
                int targetX = unit.currentX + x;
                int targetY = unit.currentY + y;

                if (targetX >= 0 && targetX < gridWidth && targetY >= 0 && targetY < gridHeight)
                {
                    validPositions.Add(new Vector2Int(targetX, targetY));
                }
            }
        }

        return validPositions;
    }


    public void MoveUnit(UnitData unit, Vector2Int targetPos)
    {
        battleGrid[unit.currentX, unit.currentY] = null;  // 現在の位置を空にする
        unit.currentX = targetPos.x;
        unit.currentY = targetPos.y;
        battleGrid[unit.currentX, unit.currentY] = unit;  // 新しい位置にユニットを配置
    }

    public List<UnitData> GetTurnOrder()
    {
        return allUnits.OrderBy(unit => unit.nextActionTime).ToList();
    }

    UnitData GetNextUnitToAct()
    {
        UnitData nextUnit = null;
        float maxActionTime = float.MinValue;  // 最大のnextActionTimeを探す

        foreach (var unit in allUnits)
        {
            if (!unit.IsDead && unit.nextActionTime > maxActionTime)
            {
                maxActionTime = unit.nextActionTime;
                nextUnit = unit;
            }
        }

        return nextUnit;
    }

    // 戦闘が進行中かを確認するメソッド
    private bool IsBattleOngoing()
    {
        bool arePlayersAlive = allUnits.Exists(unit => unit.unitType == UnitType.Player && !unit.IsDead);
        bool areEnemiesAlive = allUnits.Exists(unit => unit.unitType == UnitType.Enemy && !unit.IsDead);

        return arePlayersAlive && areEnemiesAlive;
    }

    //戦闘終了時の状態をチェックし、勝敗を判定する。
    private void CheckBattleEnd()
    {
        bool allEnemiesDefeated = enemyParty.TrueForAll(e => e.IsDead);
        bool allPlayersDefeated = playerParty.TrueForAll(p => p.IsDead);

        if (allEnemiesDefeated)
        {
            Debug.Log("プレイヤーの勝利！");
        }
        else if (allPlayersDefeated)
        {
            Debug.Log("エネミーの勝利！");
        }
    }

    public void UpdateTurnOrderUI(List<UnitData> turnOrder)
    {
        // 引数 turnOrder が null でないことを確認
        if (turnOrder == null || turnOrder.Count == 0)
        {
            Debug.LogError("Turn order list is null or empty");
            return;
        }

        // ターン順UIを更新
        turnOrderUI.UpdateTurnOrderUI(turnOrder);
    }

    void OnEnterBattleScene()
    {
        foreach (var obj in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (obj.CompareTag("Player"))
            {
                Destroy(obj); // プレイヤーオブジェクトを削除
            }
        }
    }
}
