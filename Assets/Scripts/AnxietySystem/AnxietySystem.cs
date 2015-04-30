using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

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

    private static AnxietySystem instance;
    private static readonly string anxietyKey = "Anxiety";
    private static readonly string enduranceKey = "Endurance";
    private static readonly string teleportKey = "Teleport";

    public static void SaveValues()
    {

        PlayerPrefs.SetInt(anxietyKey, instance.currentAnxiety);
        PlayerPrefs.SetInt(enduranceKey, instance.currentEndurance);
        PlayerPrefs.SetInt(teleportKey, (instance.shouldTeleport)?1:0);
    }

    void Start()
    {
        instance = this;
        enduranceSlider.minValue = 0;
        anxietySlider.minValue = 0;
        enduranceSlider.maxValue = maxAnxiety;
        anxietySlider.maxValue = maxAnxiety;

        enduranceSlider.value = 0.75f * maxEndurance;
        anxietySlider.value = 0.25f * maxAnxiety;
        intervalTime = 0f;
        if (PlayerPrefs.HasKey(teleportKey))
        {
            shouldTeleport = Convert.ToBoolean(PlayerPrefs.GetInt(teleportKey));
            PlayerPrefs.DeleteKey(teleportKey);
        }
        if (PlayerPrefs.HasKey(anxietyKey))
        {
            anxietySlider.value = PlayerPrefs.GetInt(anxietyKey);
            PlayerPrefs.DeleteKey(anxietyKey);
        }
        if (PlayerPrefs.HasKey(enduranceKey))
        {
            enduranceSlider.value = PlayerPrefs.GetInt(enduranceKey);
            PlayerPrefs.DeleteKey(enduranceKey);
        }

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
            StartCoroutine(TeleportPlayerOutside());
        }
        else if (currentAnxiety < maxAnxiety * .9 && !shouldTeleport)
        {
            Debug.Log("ShouldAllowTeleportsAgain.");
            shouldTeleport = true;
            UINotifier.DismissLock(this.gameObject);
        }
    }

    IEnumerator TeleportPlayerOutside()
    {
        Debug.Log("Should be teleporting player!");
            shouldTeleport = false;
            SaveValues();
            Application.LoadLevel("House_OutsideScene");
            yield return new WaitForSeconds(3);
            Debug.Log("Showing Notification.");
            UINotifier.NotifyLock("You became too anxious, and you have stepped outside for a while. Play some minigames to relax! (Press Escape to clear this message)",this.gameObject);
    }

    void decreaseEndurance(int value)
    {
        enduranceSlider.value -= (Mathf.Abs(anxietyThreshold - currentAnxiety))/maxAnxiety * value;
    }

    void increaseEndurance(int value)
    {
        enduranceSlider.value += (Mathf.Abs(anxietyThreshold - currentAnxiety))/maxAnxiety * value;
    }

    public static void setAnxiety(int value)
    {
        if (instance != null)
        {
            instance.anxietySlider.value = value;
        }
        else
        {
            PlayerPrefs.SetInt(anxietyKey, value);
        }
    }

    public static void increaseAnxiety(int value)
    {
        instance.anxietySlider.value += value;
    }

    public static void decreaseAnxiety(int value)
    {
        instance.anxietySlider.value -= value;
    }
}
