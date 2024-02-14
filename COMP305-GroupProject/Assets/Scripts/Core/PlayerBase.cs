using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    void BeingHit(ProjectileData data)
    {
        if (!data.isPlayer)
        {
            GameManager.Instance.SetGameOver(true);
        }
    }
}
