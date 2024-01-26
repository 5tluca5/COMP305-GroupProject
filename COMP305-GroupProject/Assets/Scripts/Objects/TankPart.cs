using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TankParts
{
    Light,
    Track,
    Hull,
    Tower,
    Gun
}

public struct TankStat
{

    public static float TankMaxDamage = 100;
    public static float TankMaxFireRate = 50;
    public static float TankMaxMovementSpeed = 15;

    [Range(1, 100)] public float damage;
    [Range(1, 50)] public float fireRate;
    [Range(1, 15)] public float movementSpeed;

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
    public int id;      // primary key id
    public int subId;   // id within specific tank parts
    public TankParts parts;
    public TankStat stat;
    public string spriteName;
    public string associateSpriteName;
    public Color color; // for light color

    // Extra abilites
    // ...

    public TankPart(int id, int subId, Color color)
    {
        this.id = id;
        this.subId = subId;
        this.color = color;
    }

    public TankPart(int id, int subId, TankParts parts, TankStat stat, string spriteName, string assoSpriteName = "")
    {
        this.id = id;
        this.subId = subId;
        this.parts = parts;
        this.stat = stat;
        this.spriteName = spriteName;
        this.associateSpriteName = assoSpriteName;
    }
}
