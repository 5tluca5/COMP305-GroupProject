using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    int spriteIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickLeftArrowBtn()
    {
        string[] hullSpriteNames = { "HeavyHullA", "HeavyHullB", "HeavyHullC", "HeavyHullD", "MediumHullA", "MediumHullB", "MediumHullC", "SmallHullA", "SmallHullB", "SmallHullC" };
        var totalSprites = hullSpriteNames.Length;

        var sprite = AtlasLoader.Instance.GetSprite(hullSpriteNames[Mathf.Abs(spriteIndex-- % totalSprites)]);

        StartCoroutine(ChangeTankPartHorizontally(true, TankParts.Hull, sprite));
    }

    public void OnClickRightArrowBtn()
    {
        string[] hullSpriteNames = { "HeavyHullA", "HeavyHullB", "HeavyHullC", "HeavyHullD", "MediumHullA", "MediumHullB", "MediumHullC", "SmallHullA", "SmallHullB", "SmallHullC" };
        var totalSprites = hullSpriteNames.Length;

        var sprite = AtlasLoader.Instance.GetSprite(hullSpriteNames[Mathf.Abs(spriteIndex-- % totalSprites)]);

        StartCoroutine(ChangeTankPartHorizontally(false, TankParts.Hull, sprite));
    }

    IEnumerator ChangeTankPartHorizontally(bool isLeft, TankParts tankPart, Sprite s)
    {
        SetArrowVisible(false);

        float aniTime = 0.2f;
        float distance = 300f;
        float direction = isLeft ? -1 : 1;
        Image target = null;

        switch(tankPart)
        {
            case TankParts.Hull:
                target = hullImg;
                break;
        }

        target.transform.DOLocalMoveX(distance * direction, aniTime);

        yield return new WaitForSeconds(aniTime);

        target.sprite = s;

        target.transform.DOLocalMoveX(distance * 2 * -direction, 0);
        target.transform.DOLocalMoveX(0, aniTime);

        yield return new WaitForSeconds(aniTime);

        SetArrowVisible(true);
    }

    void SetArrowVisible(bool set)
    {
        leftArrowBtn.SetActive(set);
        rightArrowBtn.SetActive(set);
    }
}
