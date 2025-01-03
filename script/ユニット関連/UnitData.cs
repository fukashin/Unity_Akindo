using UnityEngine;
using soubiSystem;

public enum UnitType
{
    Player,
    Enemy
}

public enum PlayerTurnState
{
    Waiting,  // ターン待ち
    Moving,   // 移動中
    Acting    // 行動選択中
}

public abstract class UnitData : ScriptableObject
{
    public string Unitname;    // 名前（共通のフィールド）
    public int Unitagility;    // アジリティ（共通のフィールド）
    public int HP;             // ヒットポイント（例: HPが0になると死亡）
    public Sprite Icon;        // アイコン画像（新しく追加）

    public bool IsDead => HP <= 0;  // HPが0以下の場合、死亡と判定

    public float nextActionTime;  // 次に行動するまでのカウント（俊敏性に基づいて増加する）

    public int MoveRange = 3;     // 移動範囲（例えば最大3マス）

    public UnitType unitType;

    // ユニットの現在位置
    public int currentX;
    public int currentY;

    // 攻撃対象（仮定）
    public UnitData targetUnit;

    public PlayerTurnState playerTurnState = PlayerTurnState.Waiting; // 初期状態

    // 各キャラクターごとの行動メソッド（仮想メソッド）
    public virtual void TakeTurn(UnitData[,] battleGrid)
    {
        if (IsDead) return;

        if (unitType == UnitType.Player)
        {
            switch (playerTurnState)
            {
                case PlayerTurnState.Waiting:
                    Debug.Log($"{Unitname} のターンです。移動を選んでください。");
                    playerTurnState = PlayerTurnState.Moving;
                    return;

                case PlayerTurnState.Moving:
                    // ユーザーが移動を選択して完了したら次の状態に進む
                    MoveToTarget(battleGrid);
                    Debug.Log("移動完了。次は行動を選んでください。");
                    playerTurnState = PlayerTurnState.Acting;
                    return;

                case PlayerTurnState.Acting:
                    // 行動が選択されたらターン終了
                    Debug.Log("行動が完了しました。");
                    playerTurnState = PlayerTurnState.Waiting; // 状態をリセット
                    return;
            }
        }
        else if (unitType == UnitType.Enemy)
        {
            //エネミーの場合は自動行動
            MoveToTarget(battleGrid);
            PerformAttack(battleGrid);
        }
        // 行動後に次のターンのカウントを設定
    }

    public void StartPlayerTurn()
    {
        playerTurnState = PlayerTurnState.Waiting;
        Debug.Log($"{Unitname} のターン開始。移動を選んでください。");
    }

    public void ProcessPlayerTurn(UnitData[,] battleGrid)
    {
        switch (playerTurnState)
        {
            case PlayerTurnState.Waiting:
                // 移動を待機
                Debug.Log("移動先を選んでください。");
                break;

            case PlayerTurnState.Moving:
                // 移動処理
                MoveToTarget(battleGrid);
                Debug.Log("移動完了。次は行動を選んでください。");
                playerTurnState = PlayerTurnState.Acting;
                break;

            case PlayerTurnState.Acting:
                // 行動処理（仮に攻撃とする）
                PerformAttack(battleGrid);
                Debug.Log("行動が完了しました。ターン終了です。");
                playerTurnState = PlayerTurnState.Waiting; // 状態をリセット
                EndPlayerTurn();
                break;
        }
    }
    public void OnMoveButtonPressed()
    {
        if (playerTurnState == PlayerTurnState.Waiting)
        {
            playerTurnState = PlayerTurnState.Moving;
        }
    }

    public void OnActionButtonPressed()
    {
        if (playerTurnState == PlayerTurnState.Moving)
        {
            playerTurnState = PlayerTurnState.Acting;
        }
    }

    public void EndPlayerTurn()
    {
        // プレイヤーのターン終了処理を実装
        Debug.Log($"{Unitname} のターンが終了しました。");
        playerTurnState = PlayerTurnState.Waiting; // 状態をリセット
    }

    public void PerformAttack(UnitData[,] battleGrid)
    {
        // 攻撃ロジック（例: 周囲のユニットを攻撃）
        Debug.Log($"{Unitname} が攻撃しました。");
    }

    // 移動処理
    public void MoveToTarget(UnitData[,] battleGrid)
    {
        // 例として移動先座標を決定
        int targetX = Random.Range(0, battleGrid.GetLength(0));
        int targetY = Random.Range(0, battleGrid.GetLength(1));

        // 移動範囲内かどうかを確認
        int distance = Mathf.Abs(targetX - currentX) + Mathf.Abs(targetY - currentY);
        if (distance > MoveRange)
        {
            Debug.LogWarning("移動範囲外です！");
            return; // 移動範囲外なら移動しない
        }

        // 空いているセルかどうか確認（移動先に他のキャラがいないかチェック）
        if (battleGrid[targetX, targetY] != null)
        {
            Debug.LogWarning("移動先にキャラクターがいます！");
            return; // 移動先にキャラクターがいる場合は移動しない
        }

        // 現在位置を更新
        battleGrid[currentX, currentY] = null; // 元の位置を空にする
        currentX = targetX;
        currentY = targetY;
        battleGrid[currentX, currentY] = this; // 新しい位置にキャラクターをセット

        Debug.Log($"{Unitname}は({targetX}, {targetY})に移動しました！");
    }


    // 攻撃処理
    public void AttackTarget(UnitData[,] battleGrid)
    {
        if (targetUnit == null || targetUnit.IsDead)
        {
            Debug.LogWarning("攻撃対象が無効です！");
            return;
        }

        // 攻撃のロジック（例: HPを減らす）
        targetUnit.HP -= 10;  // 仮に10ダメージを与える

        Debug.Log($"{Unitname}は{targetUnit.Unitname}に攻撃し、{targetUnit.Unitname}のHPは{targetUnit.HP}になりました！");
    }


    //soubi_itemで定義した武器範囲を参照
    public virtual AttackRange GetAttackRange()
    {
        // デフォルトの攻撃範囲（例: 近接1マスのみ）
        return new AttackRange { MinRange = 1, MaxRange = 1, IsArea = false };
    }
}