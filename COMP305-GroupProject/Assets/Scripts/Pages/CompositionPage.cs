using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class CompositionPage : CommonPage
{
    [Header("Tank Part Images")]
    public Image lightImg;
    public Image trackLImg;
    public Image trackRImg;
    public Image hullImg;
    public Image towerImg;
    public Image gunConnectorImg;
    public Image gunImg;

    [Header("Arrows")]
    [SerializeField] GameObject leftArrowBtn;
    [SerializeField] GameObject rightArrowBtn;

    [Header("Menu")]
    [SerializeField] TabMenu tabMenu;

    // data
    TankParts currentTab = TankParts.Light;
    List<TankPart> availablePartList;
    Dictionary<TankParts, TankPart> selectingTankParts = new Dictionary<TankParts, TankPart>();

    // Start is called before the first frame update
    void Start()
    {
        selectingTankParts = GameManager.Instance.GetCurrentTankParts();
        LoadTank();

        tabMenu.selected.Subscribe(tab =>
        {
            if (tab == null) return;

            currentTab = (TankParts)tab.GetTabIndex();
            availablePartList = TankStatManager.Instance.GetObtainedTankPart(currentTab);
        }).AddTo(this);

        onCloseCallback += OnClose;
    }

    void LoadTank()
    {
        trackLImg.sprite = trackRImg.sprite = AtlasLoader.Instance.GetSprite(selectingTankParts[TankParts.Track].spriteName);
        towerImg.sprite = AtlasLoader.Instance.GetSprite(selectingTankParts[TankParts.Tower].spriteName);
        hullImg.sprite = AtlasLoader.Instance.GetSprite(selectingTankParts[TankParts.Hull].spriteName);
        gunImg.sprite = AtlasLoader.Instance.GetSprite(selectingTankParts[TankParts.Gun].spriteName);
        gunConnectorImg.sprite = AtlasLoader.Instance.GetSprite(selectingTankParts[TankParts.Gun].associateSpriteName);
    }

    public void OnClickLeftArrowBtn()
    {
        var part = availablePartList[Mathf.Abs(--selectingTankParts[currentTab].subId % availablePartList.Count)];
        var sprite = AtlasLoader.Instance.GetSprite(part.spriteName);
        Sprite sprite2 = null;

        if(!string.IsNullOrEmpty(part.associateSpriteName))
        {
            sprite2 = AtlasLoader.Instance.GetSprite(part.associateSpriteName);
        }

        if(currentTab == TankParts.Gun || currentTab == TankParts.Track)
            StartCoroutine(ChangeTankPartVertically(true, currentTab, sprite, sprite2));
        else
            StartCoroutine(ChangeTankPartHorizontally(true, currentTab, sprite));
    }

    public void OnClickRightArrowBtn()
    {
        var part = availablePartList[Mathf.Abs(++selectingTankParts[currentTab].subId % availablePartList.Count)];
        var sprite = AtlasLoader.Instance.GetSprite(part.spriteName);
        Sprite sprite2 = null;

        if (!string.IsNullOrEmpty(part.associateSpriteName))
        {
            sprite2 = AtlasLoader.Instance.GetSprite(part.associateSpriteName);
        }

        if (currentTab == TankParts.Gun || currentTab == TankParts.Track)
            StartCoroutine(ChangeTankPartVertically(false, currentTab, sprite, sprite2));
        else
            StartCoroutine(ChangeTankPartHorizontally(false, currentTab, sprite));
    }

    IEnumerator ChangeTankPartHorizontally(bool isLeft, TankParts tankPart, Sprite s)
    {
        SetArrowVisible(false);

        float aniTime = 0.25f;
        float distance = 500f;
        float direction = isLeft ? -1 : 1;
        Image target = null;

        switch(tankPart)
        {
            case TankParts.Light:
                target = lightImg;
                break;
            case TankParts.Tower:
                target = towerImg;
                break;
            case TankParts.Hull:
                target = hullImg;
                break;
        }

        target.transform.DOLocalMoveX(distance * direction, aniTime).SetEase(Ease.InQuint);

        yield return new WaitForSeconds(aniTime);

        target.sprite = s;

        target.transform.DOLocalMoveX(distance * 2 * -direction, 0);
        target.transform.DOLocalMoveX(0, aniTime).SetEase(Ease.OutQuint);

        yield return new WaitForSeconds(aniTime);

        SetArrowVisible(true);
    }

    IEnumerator ChangeTankPartVertically(bool isLeft, TankParts tankPart, Sprite s, Sprite s2 = null)
    {
        SetArrowVisible(false);

        float aniTime = 0.25f;
        float distance = 500f;
        Image target = null;
        Image target2 = null;

        switch (tankPart)
        {
            case TankParts.Track:
                target = trackLImg;
                target2 = trackRImg;
                break;
            case TankParts.Gun:
                target = gunImg;
                target2 = gunConnectorImg;
                break;
        }

        float oriPosY = target.transform.localPosition.y;
        float oriPosY2 = target2.transform.localPosition.y;

        target.transform.DOLocalMoveY(oriPosY + distance, aniTime).SetEase(Ease.InQuint);
        target2.transform.DOLocalMoveY(oriPosY + distance, aniTime).SetEase(Ease.InQuint);

        yield return new WaitForSeconds(aniTime);

        target.sprite = s;
        target2.sprite = s2 == null ? s : s2;

        target.transform.DOLocalMoveY(oriPosY, aniTime).SetEase(Ease.OutQuint);
        target2.transform.DOLocalMoveY(oriPosY2, aniTime).SetEase(Ease.OutQuint);

        yield return new WaitForSeconds(aniTime);

        SetArrowVisible(true);
    }

    void SetArrowVisible(bool set)
    {
        leftArrowBtn.SetActive(set);
        rightArrowBtn.SetActive(set);
    }

    void OnClose()
    {

    }
}
