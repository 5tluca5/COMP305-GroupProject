using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class GMCmd
{
    [MenuItem("GMCmd/GetShopItems")]
    public static void ReadTable()
    {
        ShopTable shopTable = Resources.Load<ShopTable>("TableData/ShopItemData");
        foreach (ShopItem shopItem in shopTable.DataList)
        {
            Debug.Log(string.Format("[id]: {0}, [name]: {1}", shopItem.id, shopItem.name));
        }
    }

    [MenuItem("GMCmd/CreateShopTestData")]
    public static void CreateLocalShopData()
    {
        ShopLocalData.Instance.items = new List<ShopLocalItem>();
        for (int i = 1; i < 9; i++)
        {
            ShopLocalItem shopLocalItem = new()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
        
            };
            ShopLocalData.Instance.items.Add(shopLocalItem);
        }
        ShopLocalData.Instance.SaveShop();
    }

    [MenuItem("GMCmd/ReadShopTestData")]
    public static void ReadLocalShopData()
    {
        List<ShopLocalItem> readItems = ShopLocalData.Instance.LoadShop();
        foreach (ShopLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }
}

