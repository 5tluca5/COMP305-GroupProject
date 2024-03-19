using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    private Transform UIMenu;
    private Transform UICenter;
    private Transform UIScrollView;
    private Transform UIDetailPanel;
    private Transform UIBottomMenu;
    private Transform UITrackBtn;
    private Transform UIHullBtn;
    private Transform UITowerBtn;
    private Transform UIGunBtn;
    private Transform UIBuyBtn;

    private void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        InitName();
        InitClick();
    }

    private void InitName()
    {
        UIMenu = transform.Find("TopCenter/Menus");
        UICenter = transform.Find("Center");
        UIScrollView = transform.Find("Center/Scroll View");
        UIDetailPanel = transform.Find("Center/DetailPanel");
        UIBottomMenu = transform.Find("Bottom/BottomMenus");
        UITrackBtn = transform.Find("TopCenter/Menus/TrackBtn");
        UIHullBtn = transform.Find("TopCenter/Menus/HullBtn");
        UITowerBtn = transform.Find("TopCenter/Menus/TowerBtn");
        UIGunBtn = transform.Find("TopCenter/Menus/GunBtn");
        UIBuyBtn = transform.Find("Bottom/BottomMenus/BuyBtn");
    }

    private void InitClick()
    {
        UITrackBtn.GetComponent<Button>().onClick.AddListener(OnClickTrack);
        UIHullBtn.GetComponent<Button>().onClick.AddListener(OnClickHull);
        UITowerBtn.GetComponent<Button>().onClick.AddListener(OnClickTower);
        UIGunBtn.GetComponent<Button>().onClick.AddListener(OnClickGun);
        UIBuyBtn.GetComponent<Button>().onClick.AddListener(OnClickBuy);
    }

    private void OnClickTrack()
    {
        print("Open Track SHop");
    }

    private void OnClickHull()
    {
        print("Open Hull SHop");
    }

    private void OnClickTower()
    {
        print("Open Tower SHop");
    }

    private void OnClickGun()
    {
        print("Open Gun SHop");
    }

    private void OnClickBuy()
    {
        print("Buy it");
    }

}
