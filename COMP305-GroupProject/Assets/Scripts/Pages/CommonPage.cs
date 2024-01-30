using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

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

        Destroy(gameObject);
    }
}
