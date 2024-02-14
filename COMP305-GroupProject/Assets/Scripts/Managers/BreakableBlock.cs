using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField]
    private int _hp = 1;

    public void BeingHit(ProjectileData data)
    {
        // do the logic here

        //Unbreakable Block
        if (gameObject.layer == 7)
        {
            return;
        }

        _hp--;
        if (_hp == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
