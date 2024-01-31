using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AttributeBar : MonoBehaviour
{
    [SerializeField] AttributeType type;
    [SerializeField] Slider slider;

    [Header("Transitions")]
    [SerializeField] float currentValue = 0f;
    [SerializeField] float targetValue = 0f;
    [SerializeField] float duration = 1f;
    float timer = 0f;

    void Start()
    {
        slider = GetComponent<Slider>();

        switch(type)
        {
            case AttributeType.Damage:
                slider.maxValue = TankStat.TankMaxDamage;
                break;
            case AttributeType.FireRate:
                 slider.maxValue = TankStat.TankMaxFireRate;
                break;
            case AttributeType.MovementSpeed:
                slider.maxValue = TankStat.TankMaxMovementSpeed;
                break;
            case AttributeType.Health:
                slider.maxValue = TankStat.TankMaxHealth; 
                break;

            default:
                slider.maxValue = 100f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentValue != targetValue)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, timer / duration);
            timer += Time.deltaTime;

            if (timer >= duration)
                currentValue = targetValue;
        }
        else
        {
            timer = 0f;
        }

        slider.value = currentValue;
    }

    public AttributeType GetAttributeType() => type;

    public void SetValue(float value)
    {
        if (currentValue != targetValue)
            currentValue = targetValue;

        timer = 0;

        targetValue = value;
    }
}
