using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dateText;

    [HideInInspector]
    public float seconds, minutes, hours, days, month;

    public static DisplayTime singleton;


    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        month = 6;
        days = 15;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTime();
    }

    void CalculateTime() {

        seconds += Time.deltaTime * 3000;

        if(seconds>=60)
            {
            seconds = 0;
            minutes++;
        }
        else if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }
        else if (hours >= 24)
        {
            hours = 0;
            days++;
        }
        else if (days >= 31)
        {
            days = 1;
            month++;
        }
        else if (month >= 13)
        {
            month = 1;
        }

        timeText.text = hours.ToString("00") + ":" + minutes.ToString("00");
        dateText.text = days.ToString("00") + "-" + month.ToString("00") + "-2019";

    }
}
