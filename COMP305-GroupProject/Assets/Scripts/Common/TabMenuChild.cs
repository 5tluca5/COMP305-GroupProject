using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class TabMenuChild : MonoBehaviour
{
    [SerializeField] int tabIndex;
    [SerializeField] TabMenu tabMenu;
    [SerializeField] Image buttonImage;
    [SerializeField] Button onClickButton;
    [SerializeField] Text buttonText;

    [Header("For color driven")]
    [SerializeField] Color onNormalColor;
    [SerializeField] Color onSelectedColor;

    [Header("For Sprite driven")]
    [SerializeField] Sprite onNormalSprite;
    [SerializeField] Sprite onSelectedSprite;

    private void Awake()
    {
        tabMenu = GetComponentInParent<TabMenu>();
        buttonImage = GetComponent<Image>();
        onClickButton = GetComponent<Button>();
        if(buttonText == null) buttonText = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        onClickButton.OnClickAsObservable().Subscribe(_ =>
        {
            tabMenu.OnSelectTab(this);
        }).AddTo(this);  
    }

    public void SetSelected(bool set)
    {
        if(onNormalSprite && onSelectedSprite)
        {
            buttonImage.sprite = set ? onSelectedSprite : onNormalSprite;
        }
        else
        {
            buttonImage.color = set ? onSelectedColor : onNormalColor;

            if(buttonText)
                buttonText.color = set ? onSelectedColor : onNormalColor;
        }
    }


    public int GetTabIndex() => tabIndex;
}
