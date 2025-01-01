using UnityEngine;

[System.Serializable]
public struct Position
{
    public int x; // X座標（列）
    public int y; // Y座標（行）

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
