using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDetail : MonoBehaviour
{
    private Transform UITitle;
    private Transform UIDescription;
    private Transform UIIcon;
    private Transform UILongDescription;

    private ShopLocalItem shopLocalItem;
    private ShopItem shopItem;
    private ShopPanel shopPanel;
    
    private void InitUIName()
    {
        UITitle = transform.Find("Top/Title");
        UIDescription = transform.Find("Center/Description");
        UIIcon = transform.Find("Center/Icon");
        UILongDescription = transform.Find("Bottom/Description");
    }

    public void Refresh(ShopLocalItem shopLocalData, ShopPanel uiParent)
    {
        //initialize data
        //UITitle
        //Description
        //UIIcon
        //UILongDescription
    }

}
