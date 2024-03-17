using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField]
    private float _hp = 100f;

    public void BeingHit(ProjectileData data)
    {
        // do the logic here

        //Unbreakable Block only break by enemy
        if (data.isPlayer)
        {
            return;
        }

        _hp -= data.damage;

        if (_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
