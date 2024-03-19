using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

public class ShopPage : CommonPage
{
    [Header("Scrollview")]
    public ScrollRect scrollview;
    public GameObject shopUIItemPrefab;

    [Header("Selecting Part")]
    public ShopSelectingPart selectingPartDesc;
    public Text selectingPartStat;
    public Button buyButton;

    List<TankPart> availablePartList;
    TankPart selectingTankPart;

    [Header("Menu")]
    public TabMenu tabMenu;
    public TMPro.TextMeshProUGUI playerCoinAmount;

    TankParts currentTab;


    private void Start()
    {
        currentTab = TankParts.Track;
        availablePartList = TankStatManager.Instance.GetTankPart(currentTab);
        SetupScrollview();
        selectingTankPart = availablePartList.First();
        OnClickTankPart(selectingTankPart);

        tabMenu.selected.Subscribe(tab =>
        {
            if (tab == null) return;

            currentTab = (TankParts)tab.GetTabIndex();
            availablePartList = TankStatManager.Instance.GetTankPart(currentTab);

            SetupScrollview();

        }).AddTo(this);

        GameManager.Instance.PlayerCoins.Subscribe(x =>
        {
            playerCoinAmount.text = x.ToString();
        }).AddTo(this);
    }

    void SetupScrollview()
    {
        foreach(Transform child in scrollview.content)
        {
            Destroy(child.gameObject);
        }

        foreach(var part in availablePartList)
        {
            var isOwned = GameManager.Instance.IsTankPartUnlocked(part);
            var tp = Instantiate(shopUIItemPrefab, scrollview.content).GetComponent<ShopUIItem>();
            tp.Setup(part, isOwned, OnClickTankPart);
        }
    }

    void OnClickTankPart(TankPart tp)
    {
        selectingTankPart = tp;
        selectingPartDesc.Setup(tp);

        var t = "";

        if (tp.stat.damage != 0)
        {
            t += "DMG: " + tp.stat.damage + "\n";
        }
        if (tp.stat.fireRate != 0)
        {
            t += "FR: " + tp.stat.fireRate + "\n";
        }
        if (tp.stat.movementSpeed != 0)
        {
            t += "MS: " + tp.stat.movementSpeed + "\n";
        }
        if (tp.stat.health != 0)
        {
            t += "HP: " + tp.stat.health;
        }

        selectingPartStat.text = t;

        buyButton.interactable = GameManager.Instance.CanPurchaseTankPart(selectingTankPart);
    }

    public void OnClickBuy()
    {
        if(GameManager.Instance.PurchaseTankPart(selectingTankPart))
        {
            RefreshPage();
        }
    }

    void RefreshPage()
    {
        OnClickTankPart(selectingTankPart);
        SetupScrollview();
    }
}
