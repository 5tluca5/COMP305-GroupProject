using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtlasLoader
{
    static AtlasLoader instance;

    public static AtlasLoader Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new AtlasLoader();
            }

            return instance;
        }
    }

    public Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

    //Creates new Instance only, Manually call the loadSprite function later on 
    public AtlasLoader()
    {

    }

    //Creates new Instance and Loads the provided sprites
    public AtlasLoader(string fileName)
    {
        LoadAtlas(fileName);
    }

    //Loads the provided sprites
    public void LoadAtlas(string fileName)
    {
        var objects = Resources.Load(fileName);
        Sprite[] allSprites = Resources.LoadAll<Sprite>(fileName);
        if (allSprites == null || allSprites.Length <= 0)
        {
            Debug.LogError("The Provided atlas `" + fileName + "` does not exist!");
            return;
        }

        string result = "";
        for (int i = 0; i < allSprites.Length; i++)
        {
            spriteDic.Add(allSprites[i].name, allSprites[i]);
            //Debug.Log($"{allSprites[i].name} has loaded.");
            result += "\"" + allSprites[i].name + "\", ";
        }

        Debug.Log(result);

    }

    //Get the provided atlas from the loaded sprites
    public Sprite GetSprite(string atlasName)
    {
        Sprite tempSprite;

        if (!spriteDic.TryGetValue(atlasName, out tempSprite))
        {
            Debug.LogError("The Provided atlas `" + atlasName + "` does not exist!");
            return null;
        }
        return tempSprite;
    }

    //Returns number of sprites in the Atlas
    public int AtlasCount()
    {
        return spriteDic.Count;
    }
}
