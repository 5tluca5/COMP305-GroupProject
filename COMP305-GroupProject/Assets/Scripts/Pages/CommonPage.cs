using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CommonPage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBackBtn()
    {
        StartCoroutine(ClosePage());
    }

    IEnumerator ClosePage()
    {
        transform.DOLocalMoveX(-GetComponent<RectTransform>().rect.width, 0.5f);

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
