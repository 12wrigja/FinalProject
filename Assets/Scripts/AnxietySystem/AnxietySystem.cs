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

    private bool shouldTeleport = true;

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
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            increaseAnxiety(5);
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            decreaseAnxiety(5);
        }

        if (UINotifier.hasLock(this.gameObject) && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Dismissing message because of escape button.");
            UINotifier.DismissLock(this.gameObject);
        }

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

        if (currentAnxiety == maxAnxiety && shouldTeleport)
        {
            shouldTeleport = false;
            DontDestroyOnLoad(HumanControlScript.GetHuman());
            DontDestroyOnLoad(GetComponentInParent<Canvas>().gameObject);
            Application.LoadLevel("House_OutsideScene");
            UINotifier.NotifyLock("You became too anxious, and you have stepped outside for a while. Play some minigames to relax! (Press Escape to clear this message)",this.gameObject);
        }
        else if (currentAnxiety < maxAnxiety * .9)
        {
            shouldTeleport = true;
            UINotifier.DismissLock(this.gameObject);
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
