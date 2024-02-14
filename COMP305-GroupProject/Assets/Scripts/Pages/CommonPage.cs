using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CommonPage : MonoBehaviour
{
    protected UnityAction onCloseCallback;

    public void OnClickBackBtn()
    {
        onCloseCallback?.Invoke();
        StartCoroutine(ClosePage());
    }

    IEnumerator ClosePage()
    {
        transform.DOLocalMoveX(-GetComponent<RectTransform>().rect.width, 0.5f);

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        transform.DOLocalMoveX(0, 0);
        //Destroy(gameObject);
    }
}
