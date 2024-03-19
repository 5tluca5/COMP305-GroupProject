using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateShopTable", fileName = "ShopTable")]
public class ShopTable : ScriptableObject
{
    // List of shop items in this table
    public List<ShopItem> DataList = new List<ShopItem>();
}

[System.Serializable]
//Shop item elements
public class ShopItem
{
    public int id;
    public string name;
    public string imagePath;
    public string type;
    public string assoPartName;
    public float damage;
    public float health;
    public float fireRate;
    public float movementSpeed;
    public int price;
}
