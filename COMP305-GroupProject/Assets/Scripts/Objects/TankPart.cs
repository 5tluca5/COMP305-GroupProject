using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TankParts : int
{
    Light = 0,
    Track,
    Hull,
    Tower,
    Gun
}
public enum AttributeType : int
{
    Damage = 0,
    FireRate,
    MovementSpeed,
    Health
}

public struct TankStat
{
    public static float TankMaxDamage = 100;
    public static float TankMaxFireRate = 50;
    public static float TankMaxMovementSpeed = 15;
    public static float TankMaxHealth = 150;

    [Range(1, 100)] public float damage;
    [Range(1, 50)] public float fireRate;
    [Range(1, 15)] public float movementSpeed;
    [Range(1, 50)] public float health;
    
    public TankStat(float dmg, float fr, float ms, float hp)
    {
        this.damage = dmg;
        this.fireRate = fr;
        this.movementSpeed = ms;
        this.health = hp;
    }

    public static TankStat operator +(TankStat a, TankStat b)
    {
        return new TankStat(a.damage + b.damage, a.fireRate + b.fireRate, a.movementSpeed + b.movementSpeed, a.health+b.health);
    }
}

public class TankPart
{
    public int id;      // primary key id
    public int subId;   // id within specific tank parts
    public TankParts parts;
    public TankStat stat;
    public string spriteName;
    public string associateSpriteName;  // For the guns, need to specify which gun connector its using
    public Color32 color; // for light color
    public int cost;
    // Extra abilites
    // ...

    public TankPart(int id, int subId, TankParts parts, Color32 color)
    {
        this.id = id;
        this.subId = subId;
        this.color = color;
        this.parts = parts;
    }

    public TankPart(int id, int subId, TankParts parts, TankStat stat, int cost, string spriteName, string assoSpriteName = "")
    {
        this.id = id;
        this.subId = subId;
        this.parts = parts;
        this.stat = stat;
        this.cost = cost;
        this.spriteName = spriteName;
        this.associateSpriteName = assoSpriteName;
    }
}
