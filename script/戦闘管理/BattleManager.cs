using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public EnemyData currentEnemy; // 現在の敵データ

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

    public void SetEnemyData(EnemyData enemy)
    {
        currentEnemy = enemy;
    }
}
