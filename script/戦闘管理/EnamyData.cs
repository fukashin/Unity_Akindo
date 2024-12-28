[System.Serializable]
public class EnemyData
{
    public string name;
    public int hp;
    public int attack;
    public int defense;

    public EnemyData(string name, int hp, int attack, int defense)
    {
        this.name = name;
        this.hp = hp;
        this.attack = attack;
        this.defense = defense;
    }
}
