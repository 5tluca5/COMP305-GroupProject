using UnityEngine;
using System.Collections.Generic;

public class ShopLocalData
{
    private static ShopLocalData instance;
    public static ShopLocalData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ShopLocalData();
            }
            return instance;
        }
    }

    public List<ShopLocalItem> items;

    public void SaveShop()
    {
        string inventoryJson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("ShopLocalData", inventoryJson);
        PlayerPrefs.Save();
    }

    public List<ShopLocalItem> LoadShop()
    {
        if (items != null)
        {
            return items;
        }
        if (PlayerPrefs.HasKey("ShopLocalData"))
        {
            string inventoryJson = PlayerPrefs.GetString("ShopLocalData");
            ShopLocalData shopLocalData = JsonUtility.FromJson<ShopLocalData>(inventoryJson);
            items = shopLocalData.items;
            return items;
        }
        else
        {
            items = new List<ShopLocalItem>();
            return items;
        }
    }
}

[System.Serializable]
public class ShopLocalItem
{
    public string uid;
    public int id;

    public override string ToString()
    {
        return string.Format("ShopLocalItem - ID: {0}", id);
    }
}

