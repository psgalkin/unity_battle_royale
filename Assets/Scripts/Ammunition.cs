using UnityEngine;

public class Ammunition : MonoBehaviour
{

    public int BulletCount { private set; get; } = 50;

    public void BulletDecrement()
    {
        if (BulletCount >= 0) { BulletCount--; }
    }

    public void AddBullets(int p_count)
    {
        if (BulletCount + p_count > 50) { BulletCount = 50; }
        else BulletCount += p_count;
    }
}
