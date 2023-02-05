using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Image timeBar;
    public Text timeText;
    

    // Update is called once per frame
    void Update()
    {
        timeText.text = $"{GameManager.Instance.timer}";
        float timeRate = GameManager.Instance.timer / GameManager.Instance.start_time;
        timeBar.fillAmount = timeRate;
    }
}
