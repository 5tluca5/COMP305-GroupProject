using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank
{
    [Header("Tank Part")]
    protected Dictionary<TankParts, TankPart> tankParts = new Dictionary<TankParts, TankPart>();

    protected override void SetupTank()
    {
        stat = GameManager.Instance.GetCurrentTankStat();
        tankParts = GameManager.Instance.GetCurrentTankParts();

        curHealth.Value = stat.health;
        fireRate = 1 / (stat.fireRate * 0.1f);

        trackLImg.sprite = trackRImg.sprite = AtlasLoader.Instance.GetSprite(tankParts[TankParts.Track].spriteName);
        towerImg.sprite = AtlasLoader.Instance.GetSprite(tankParts[TankParts.Tower].spriteName);
        hullImg.sprite = AtlasLoader.Instance.GetSprite(tankParts[TankParts.Hull].spriteName);
        gunImg.sprite = AtlasLoader.Instance.GetSprite(tankParts[TankParts.Gun].spriteName);
        gunConnectorImg.sprite = AtlasLoader.Instance.GetSprite(tankParts[TankParts.Gun].associateSpriteName);
    }

    protected override void BeingHit(ProjectileData data)
    {
        if (!data.isPlayer)
        {
            curHealth.Value -= data.damage;
        }

        if (curHealth.Value <= 0f)
        {
            // GG
            Debug.Log("Gameover");
            Time.timeScale = 0f;
        }
    }
}
