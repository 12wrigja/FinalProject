using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnxietySystem : MonoBehaviour {

    public int maxEndurance;
    public int maxAnxiety;
    public int anxietyThreshold;
    public int enduranceDecreaseOverTime;
    public int enduranceIncreaseOverTime;

    public int updateTime;

    public Color anxietyGoodColor;
    public Color anxietyBadColor;

    public int currentAnxiety
    {
        get
        {
            return (int)anxietySlider.value;
        }
    }

    public int currentEndurance
    {
        get
        {
            return (int)enduranceSlider.value;
        }
    }

    public Slider enduranceSlider;
    public Slider anxietySlider;

    public Image anxietySliderBackground;

    private float intervalTime;

    void Start()
    {
        enduranceSlider.minValue = 0;
        anxietySlider.minValue = 0;
        enduranceSlider.maxValue = maxAnxiety;
        anxietySlider.maxValue = maxAnxiety;

        enduranceSlider.value = 0.75f * maxEndurance;
        anxietySlider.value = 0.25f * maxAnxiety;
        intervalTime = 0f;
        UIElement ele = GetComponent<UIElement>();
        UIManager.ShowUIElement(ele);
    }


    void Update()
    {
        intervalTime += Time.deltaTime;
        if (intervalTime >= updateTime)
        {
            intervalTime = 0f;
            if (currentAnxiety > anxietyThreshold)
            {
                decreaseEndurance(enduranceDecreaseOverTime);
            }
            else if (currentAnxiety < anxietyThreshold)
            {
                increaseEndurance(enduranceIncreaseOverTime);
            }
        }

        if (currentAnxiety > anxietyThreshold)
        {
            anxietySliderBackground.color = anxietyBadColor;
        }
        else
        {
            anxietySliderBackground.color = anxietyGoodColor;
        }

    }

    void decreaseEndurance(int value)
    {
        enduranceSlider.value -= (Mathf.Abs(anxietyThreshold - currentAnxiety))/maxAnxiety * value;
    }

    void increaseEndurance(int value)
    {
        enduranceSlider.value += (Mathf.Abs(anxietyThreshold - currentAnxiety))/maxAnxiety * value;
    }

    void increaseAnxiety(int value)
    {
        anxietySlider.value += value;
    }

    void decreaseAnxiety(int value)
    {
        anxietySlider.value -= value;
    }
}
