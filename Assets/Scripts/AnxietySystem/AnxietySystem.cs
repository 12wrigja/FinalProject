using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnxietySystem : MonoBehaviour {

    public int StaminaMaxVal = 100;
    public int RefreshInterval = 1;
    public int StaminaDecreaseOverTime = 5;
    public int StaminaIncreaseOverTime = 2;

    public Slider socialStaminaSlider;
    public Slider socialAnxietyMeter;

    private float intervalTime = 0f;

    void Start()
    {
        socialStaminaSlider.maxValue = 100;
        socialStaminaSlider.minValue = 0;
        socialStaminaSlider.value = 100;

        socialAnxietyMeter.maxValue = 100;
        socialAnxietyMeter.value = 25;
        socialAnxietyMeter.minValue = 0;

        intervalTime = 0f;
    }

    void Update()
    {
        intervalTime += Time.deltaTime;
        if (intervalTime > RefreshInterval)
        {
            intervalTime = 0f;
            if (socialAnxietyMeter.value == socialAnxietyMeter.maxValue)
            {
                //Trigger minigames here
                Debug.Log("Triggering Minigames.");
                return;
            }
            int difference = (int)Mathf.Abs(socialAnxietyMeter.value - 50);
            if (socialAnxietyMeter.value > 50)
            {
                //The bar is above the critical level. Decrease the social endurance
                socialStaminaSlider.value -= difference;
            }
            else if (socialAnxietyMeter.value < 50)
            {
                socialStaminaSlider.value += difference;
            }
        }
    }

    public void increaseAnxiety(int increaseValue)
    {
        if (socialAnxietyMeter.value + increaseValue <= socialAnxietyMeter.maxValue)
        {
            socialAnxietyMeter.value += increaseValue;
        }
    }

    public void decreaseAnxiety(int decreaseValue)
    {
        if (socialAnxietyMeter.value - decreaseValue <= socialAnxietyMeter.minValue)
        {
            socialAnxietyMeter.value -= decreaseValue;
        }
    }

    public void setSocialStaminaValue(int value)
    {
        if (value <= socialStaminaSlider.maxValue)
        {
            socialStaminaSlider.value = value;
        }
    }
}
