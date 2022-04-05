using UnityEngine;
using TMPro;


public class Clock : MonoBehaviour
{
    private TextMeshProUGUI Text;

    private int currentHour = 9;
    private int currentMinute = 0;

    private string timeSuffix = " AM";

    public void UpdateClock(bool byHour = true)
    {
        if ( byHour ) currentHour += 1;
        else currentMinute += 10;

        if ( currentMinute > 50 )
        {
            currentMinute = 00;
            currentHour += 1;
        }

        if ( currentHour > 12 )
            currentHour = 1;
        if ( currentHour >= 12 )
            timeSuffix = " PM";

        string mins;
        if ( currentMinute == 0 )
            mins = "00";
        else
            mins = currentMinute.ToString();


        Text.text = currentHour.ToString() + ":" + mins + timeSuffix;
    }

    private void Start()
    {
        this.Text = GetComponent<TextMeshProUGUI>();
    }
}
