using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public EnemyData currentEnemy;  // 現在の敵データ
    public CharacterData currentPlayer;  // 現在のプレイヤーデータ（CharacterData）
    private bool isPlayerTurn;  // プレイヤーのターンかどうか

    public TextMeshProUGUI turnIndicatorText;  // ターンの結果を表示するTextMeshProUGUI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを超えてデータを保持
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 敵データの設定
    public void SetEnemyData(EnemyData enemy)
    {
        currentEnemy = enemy;
    }

    // プレイヤーデータの設定
    public void SetPlayerData(CharacterData player)
    {
        currentPlayer = player;
    }

    // 先制攻撃を決定
    public void DetermineFirstTurn()
    {
        // 俊敏性を元に先制攻撃者を決定
        int playerSpeed = currentPlayer.agility;  // プレイヤーの俊敏性
        int enemySpeed = currentEnemy.agility;    // 敵の俊敏性

        string playerName = currentPlayer.characterName;  // プレイヤー名
        string enemyName = currentEnemy.enemyName;        // 敵名

        // 俊敏性の高い方が先攻、同じ場合は確率で決定
        if (playerSpeed > enemySpeed)
        {
            isPlayerTurn = true; // プレイヤーが先攻
            UpdateTurnUI($"{playerName} goes first.");  // UIに表示
        }
        else if (playerSpeed < enemySpeed)
        {
            isPlayerTurn = false; // 敵が先攻
            UpdateTurnUI($"{enemyName} goes first.");  // UIに表示
        }
        else
        {
            // 俊敏性が同じ場合はランダムで決定
            float randomChance = Random.Range(0f, 1f);
            if (randomChance >= 0.5f)
            {
                isPlayerTurn = true; // プレイヤーが先攻
                UpdateTurnUI($"{playerName} goes first.");  // UIに表示
            }
            else
            {
                isPlayerTurn = false; // 敵が先攻
                UpdateTurnUI($"{enemyName} goes first.");  // UIに表示
            }
        }
    }

    // UIのテキストを更新するメソッド
    private void UpdateTurnUI(string message)
    {
        if (turnIndicatorText != null)
        {
            turnIndicatorText.text = message;  // ターンの結果をUIに表示
        }
    }

    // ゲームのターン開始時に呼ばれるメソッド
    public void StartBattle()
    {
        DetermineFirstTurn();  // 先制攻撃の決定
        // その後、ターンに応じたアクションを開始する処理を追加
    }
}
