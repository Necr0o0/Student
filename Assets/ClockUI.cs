using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockUI : MonoBehaviour
{

    //how many seconds have game hour
    public  int SecToInGameHour = 3600;

    private const float HOURS_PER_DAY = 24.0f;
    private const float ROTATION_DEGREES_PER_DAY = 360f;
    private const float MIN_IN_DAY = 60f;

    public DisplayTime displayTime;
    public Transform clockMinutes;
    public Transform clockHours;


    public TextMeshProUGUI timeText;

    private float day;
    private float dayNormalized;

    string minutes;
    string hours;

    private void Update()
    {



        //minute handle rotates 24 times faster than hours
        clockMinutes.eulerAngles = new Vector3(0, 0, -displayTime.minutes *6);
        clockHours.eulerAngles = new Vector3(0, 0, -displayTime.hours * 12 );
        //Debug.Log("Display test " + displayTime.minutes);

    }
}
