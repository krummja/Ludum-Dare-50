using UnityEngine;
using TMPro;


public class Clock : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Text;

    private int currentHour = 9;
    private int currentMinute = 0;

    private string timeSuffix = "AM";

    public void UpdateClock(bool byHour = true)
    {
        if ( byHour ) currentHour += 1;
        else currentMinute += 3;

        if ( currentMinute > 3 )
        {
            currentMinute = 0;
            currentHour += 1;
        }

        if ( currentHour > 12 )
            currentHour = 1;
        if ( currentHour >= 12 )
            timeSuffix = "PM";

        Text.text = currentHour.ToString() + ":" + currentMinute.ToString() + "0 " + timeSuffix;
    }
}
