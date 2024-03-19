using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelectingPart : MonoBehaviour
{
    public Image img;
    public Text nameText;
    public Text introText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(TankPart tp)
    {
        img.sprite = AtlasLoader.Instance.GetSprite(tp.spriteName);

        nameText.text = tp.spriteName;
        introText.text = "$" + tp.cost.ToString();
    }

}
