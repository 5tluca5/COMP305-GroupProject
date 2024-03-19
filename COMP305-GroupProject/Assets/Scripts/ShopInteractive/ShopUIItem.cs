using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopUIItem : MonoBehaviour
{
    public Image img;
    public Text price;
    public Text owned;
    public Button onClickBtn;

    Action<TankPart> onClickCallback;

    public void Setup(TankPart tp, bool isOwned, Action<TankPart> onClickCallback)
    {
        img.sprite = AtlasLoader.Instance.GetSprite(tp.spriteName);
        price.text = tp.cost.ToString();

        price.gameObject.SetActive(!isOwned);
        owned.gameObject.SetActive(isOwned);

        this.onClickCallback = onClickCallback;
        onClickBtn.onClick.AddListener(() => { this.onClickCallback.Invoke(tp); });
    }

}
