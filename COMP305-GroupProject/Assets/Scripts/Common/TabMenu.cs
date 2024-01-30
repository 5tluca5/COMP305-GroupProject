using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class TabMenu : MonoBehaviour
{
    public ReactiveProperty<TabMenuChild> selected = new ReactiveProperty<TabMenuChild>();

    List<TabMenuChild> childs = new List<TabMenuChild>();
    // Start is called before the first frame update
    void Start()
    {
        childs = GetComponentsInChildren<TabMenuChild>().ToList();

        
    }

    public void OnSelectTab(TabMenuChild tab)
    {
        foreach(var child in childs)
        {
            child.SetSelected(child == tab);
        }

        selected.Value = tab;
    }
}
