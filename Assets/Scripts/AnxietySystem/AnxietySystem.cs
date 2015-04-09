using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnxietySystem : MonoBehaviour {

    public int maxEndurance;
    public int maxAnxiety;
    public int anxietyThreshold;
    public int enduranceDecreaseOverTime;
    public int enduranceIncreaseOverTime;

    public float enduranceDecreaseMaxMultiplier;
    public float enduranceIncreaseMaxMultiplier;

    public int updateTime;

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
        
    }

    void decreaseEndurance(int value)
    {
        enduranceSlider.value -= enduranceDecreaseMaxMultiplier * (Mathf.Abs(anxietyThreshold - currentAnxiety)) * value;
    }

    void increaseEndurance(int value)
    {
        enduranceSlider.value += enduranceIncreaseMaxMultiplier * (Mathf.Abs(anxietyThreshold - currentAnxiety)) * value;
    }
}
