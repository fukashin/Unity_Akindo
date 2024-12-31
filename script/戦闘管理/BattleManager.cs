using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [Header("グリッド設定")]
    public int gridWidth = 8;  // 横幅
    public int gridHeight = 4; // 縦幅
    private CharacterData[,] battleGrid; // グリッド

    [Header("キャラクター位置管理")]
    public Vector2Int playerPosition; // プレイヤーの初期位置
    public Vector2Int enemyPosition;  // 敵の初期位置

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

        InitializeGrid();
    }

    // グリッド初期化
    private void InitializeGrid()
    {
        battleGrid = new CharacterData[gridWidth, gridHeight];
        playerPosition = new Vector2Int(0, gridHeight / 2);
        enemyPosition = new Vector2Int(gridWidth - 1, gridHeight / 2);
    }

    // キャラクターを配置する
    public bool PlaceCharacter(CharacterData character, int x, int y)
    {
        if (battleGrid[x, y] == null)
        {
            battleGrid[x, y] = character;
            return true;
        }
        return false;
    }

    // グリッドをクリア
    public void ClearGridPosition(int x, int y)
    {
        battleGrid[x, y] = null;
    }

    // グリッド状態を取得
    public CharacterData GetCharacterAtPosition(int x, int y)
    {
        return battleGrid[x, y];
    }

    // デバッグ用グリッド表示
    public void DisplayGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            string row = "";
            for (int x = 0; x < gridWidth; x++)
            {
                row += battleGrid[x, y] != null ? battleGrid[x, y].characterName : "Empty";
                row += "\t";
            }
            Debug.Log(row);
        }
    }
}

