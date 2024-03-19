using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform UISelect;
    private Transform UIBuySelect;
    private Transform UIIcon;
    private Transform UINew;
    private Transform UIPrice;

    // Data
    private ShopLocalItem shopLocalData;
    private ShopItem shopItem;
    private ShopPanel uiParent;

    private void Awake()
    {
        InitUIName();
    }

    private void InitUIName()
    {
        UISelect = transform.Find("Select");
        UIBuySelect = transform.Find("BuySelect");
        UIIcon = transform.Find("Top/Icon");
        UINew = transform.Find("Top/New");
        UIPrice = transform.Find("Bottom/Price");
    }

    public void SetShopItem(ShopItem item)
    {
        shopItem = item;

        // Initialize UI elements based on the shop item
        //UINew.gameObject.SetActive(shopLocalData != null);

        // Load item image
        Texture2D t = (Texture2D)Resources.Load(shopItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = temp;

        // Display item price
        UIPrice.GetComponent<Text>().text = "$" + shopItem.price.ToString();
    }

    //Mouse Click Event
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
    }
}
