using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attribute : MonoBehaviour
{
    List<AttributeBar> attributeBars = new List<AttributeBar>();

    void Start()
    {
        attributeBars = GetComponentsInChildren<AttributeBar>().ToList();
    }

    public void SetStat(TankStat stat)
    {
        foreach (var bar in attributeBars)
        {
            switch (bar.GetAttributeType())
            {
                case AttributeType.Damage:
                    bar.SetValue(stat.damage);
                    break;
                case AttributeType.FireRate:
                    bar.SetValue(stat.fireRate);
                    break;
                case AttributeType.MovementSpeed:
                    bar.SetValue(stat.movementSpeed);
                    break;
                case AttributeType.Health:
                    bar.SetValue(stat.health);
                    break;
            }
        }
    }
}
