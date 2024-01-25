using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TankParts
{
    Track,
    Hulk,
    Tower,
    Gun
}

public struct TankStat
{
    public float damage;
    public float fireRate;
    public float movementSpeed;

    public TankStat(float dmg, float fr, float ms)
    {
        this.damage = dmg;
        this.fireRate = fr;
        this.movementSpeed = ms;
    }

    public static TankStat operator +(TankStat a, TankStat b)
    {
        return new TankStat(a.damage + b.damage, a.fireRate + b.fireRate, a.movementSpeed + b.movementSpeed);
    }
}

public class TankPart
{
    public int id;
    public TankParts parts;
    public TankStat stat;
    public string spriteName;

    // Extra abilites
}
