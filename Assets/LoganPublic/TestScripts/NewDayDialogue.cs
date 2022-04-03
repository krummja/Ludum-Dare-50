using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewDayDialogue : MonoBehaviour
{

    private SpriteRenderer backgroundSprite;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI radioText;
    
    string radioString1 = "";
    string radioString2 = "";
    string combinedRadio;

    private bool skip = false;


    public int day = 1; // Day of the week starting with sunday
    public int disaster = 0; // What happened to close school today 0 - Nothing

    // Start is called before the first frame update
    void Start()
    {
        backgroundSprite = gameObject.GetComponent<SpriteRenderer>();
        backgroundSprite.color = new Color(0, 0, 0, 1);
        dayText.color = new Color32(0, 0, 0, 1);
        StartCoroutine(DayFade());
    }

    // Update is called once per frame
    void Update()
    {

        skip = false;
        // if (Input.GetButtonDown("Submit")) { skip = true; }

    }

    IEnumerator DayFade()
    {



        //Fade In Day
        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 150f)
        {
            if (skip == true) { alpha = 255; }
            dayText.color = new Color32((byte)(alpha), (byte)(alpha), (byte)(alpha), 1);
            yield return null;

        }
        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 250f)
        {
            if (skip == true) { alpha = 255; }
            backgroundSprite.color = new Color(alpha / 255, alpha / 255, alpha / 255, 1);
            yield return null;
            
        }

        yield return new WaitForSeconds(1f);

        //Fade out day text
        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f)
        {
            if (alpha > 255) { alpha = 255; }
            if (skip == true) { alpha = 255; }
                backgroundSprite.color = new Color((350 - (alpha))/ 255, (350 - (alpha)) / 255, (350 - (alpha)) / 255, 1); // don't look. It's too horrible
            dayText.color = new Color32((byte)(255-alpha), (byte)(255 - alpha), (byte)(255 - alpha), 1);
            yield return null;

        }

        //Create Radio String

        if (day == 1) {
            radioString1 =
        "Good morning listeners! \n \n " +
        "The skies are clear and it is a beautiful Sunday morning. \n \n " +
        "Coming up this week: Monday will mark the first day back at school for all the young students out there.\n \n " +
        "That's right, folks, it's the final day of summer vaction. \n \n Make it count.";
        }
        
        if (day == 2) { radioString1 = ""; }
        if (day == 3) { radioString1 = ""; }
        if (day == 4) { radioString1 = ""; }
        if (day == 5) { radioString1 = ""; }
        if (day == 6) { radioString1 = ""; }
        if (day == 7) { radioString1 = ""; }

        if (disaster == 0) { radioString2 = ""; }
        if (disaster == 1) { radioString2 = ""; }
        if (disaster == 2) { radioString2 = ""; }
        if (disaster == 3) { radioString2 = ""; }
        if (disaster == 4) { radioString2 = ""; }
        if (disaster == 5) { radioString2 = ""; }
        if (disaster == 6) 
        {
            radioString2 =
            "I'm sure I'm not the only one who couldn't believe my eyes this morning. \n \n " +
            "snow, folks. \n \n " +
            "That's right, snow in September. Expert meteorologists have predicted that the snow will probably stick around for at least a week, and the unexpected winter conditions will see schools close across the city for at least as long. \n \n " +
            "it's unbelievable, folks. \n \n " +
            "it's simply unbelievable";
        }
        if (disaster == 7) {
            radioString2 =
            "Today is the first day of school for the students of ValleyRidge School. They've had a bit of an interesting week in terms of delays, but it sounds like things are finally back on track. \n \n " +
            "I guess it goes to show that even if you'd like summer to last forever... \n \n " +
            "you can't escape the inevitable";
        } // Game Over

        combinedRadio = radioString1 + "\n \n" + radioString2;



        //Type out text

        foreach (char c in combinedRadio)
        {
            if (skip == true) {
                radioText.text = combinedRadio;
                break; 
            }
            radioText.text += c;
            yield return new WaitForSeconds(0.075f);
        }
    }
}
