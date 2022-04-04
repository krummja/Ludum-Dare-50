using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewDayDialogue : MonoBehaviour
{
    public SpriteRenderer crossBike;
    public SpriteRenderer crossGames;
    public SpriteRenderer crossBaseball;
    public SpriteRenderer crossMovies;
    public SpriteRenderer crossIceCream;
    public SpriteRenderer crossBeach;

    private SpriteRenderer backgroundSprite;
    public SpriteRenderer whiteSprite;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI radioText;
    public Transform notebook;

    public int day; // Day of the week starting with sunday
    public int disaster = 0; // What happened to close school today 0 - Nothing

    private string dayString = "";
    private string radioString1 = "";
    private string radioString2 = "";
    private string combinedRadio;

    private bool skip = false;
    private bool skipped = false; // check to see if button was pressed

    private int nextScene = 2;

    private void Start()
    {
        day = Gamestate.day;
        disaster = Gamestate.disaster;

        if (day == 0) { dayString = "Sunday"; }
        if (day == 1) { dayString = "Monday"; }
        if (day == 2) { dayString = "Tuesday"; }
        if (day == 3) { dayString = "Wednesday"; }
        if (day == 4) { dayString = "Thursday"; }
        if (day == 5) { dayString = "Friday"; }
        if (day == 6) { dayString = "Saturday"; }

        dayText.text = dayString;

        if (Gamestate.bicycle == true) crossBike.enabled = true;
        if (Gamestate.videoGames == true) crossGames.enabled = true;
        if (Gamestate.baseball== true) crossBaseball.enabled = true;
        if (Gamestate.movie == true) crossMovies.enabled = true;
        if (Gamestate.iceCream == true) crossIceCream.enabled = true;
        if (Gamestate.beach == true) crossBeach.enabled = true;

        backgroundSprite = gameObject.GetComponent<SpriteRenderer>();
        backgroundSprite.color = new Color(0, 0, 0, 1);
        dayText.color = new Color32(0, 0, 0, 1);
        StartCoroutine(DayFade());
    }

    private void Update()
    {
        if (Input.anyKeyDown && skipped == false) { skip = true; skipped = true; }
        if (Input.anyKeyDown == false && skipped == true) { skipped = false; }
    }

    IEnumerator DayFade()
    {
        //Fade In Day
        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 150f)
        {
            if (skip == true) { skip = false; alpha = 255; }
            dayText.color = new Color32((byte)(alpha), (byte)(alpha), (byte)(alpha), 255);
            yield return null;

        }
        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 250f)
        {
            if (skip == true) { skip = false; alpha = 255; }
            backgroundSprite.color = new Color(alpha / 255, alpha / 255, alpha / 255, 1);
            yield return null;
            
        }

        yield return new WaitForSeconds(1f);

        //Fade out day text
        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f)
        {
            if (alpha > 255) { alpha = 255; }
            if (skip == true) { skip = false; alpha = 255; }
                backgroundSprite.color = new Color((350 - (alpha))/ 255, (350 - (alpha)) / 255, (350 - (alpha)) / 255, 1); // don't look. It's too horrible
            dayText.color = new Color32((byte)(255-alpha), (byte)(255 - alpha), (byte)(255 - alpha), (byte)((255-alpha)));
            yield return null;

        }

        //Create Radio String

        if (day == 0) {
            radioString1 =
        "Good morning listeners! \n \n " +
        "The skies are clear and it is a beautiful Sunday morning. \n \n " +
        "Coming up this week: Monday will mark the first day back to school for all the young students in our city.\n \n " +
        "That's right, folks, it's the final day of summer vaction. \n \n Make the most of it.";
        }
        
        if (day == 1) {
            radioString1 =
            "Good morning listeners! \n \n " +
            "The sun is out on this fine Monday morning. \n \n " +
            "It's the first day of classes for many today, and the city is bustling with that back-to-school energy"; }
        if (day == 2) { radioString1 =
            "Good morning listeners! \n \n " +
            "It's Tuesday. The week is still just getting started so hang in there, folks!";
        }
        if (day == 3) { radioString1 =
            "Good morning listeners! \n \n " +
            "It is Wednesday and the weather outside is great!";
        }
        if (day == 4) { radioString1 =
            "Good morning listeners! \n \n " +
            "It's a bit cloudy on this Thursday morning but it should be sunny again by noon.";
        }
        if (day == 5) { radioString1 =
            "Good morning listeners! \n \n " +
            "It's Friday! \n \n" +
            "And I know everyone is tuning in today to hear the same story. Here it is:";
        }
         if (day == 6) { radioString1 =
            "Good morning listeners! \n \n " +
            "it's Saturday.";
        }

        if (disaster == 0 && day == 1) { radioString2 = "Good luck to all those returning students out there \n \n I'm sure they're thinking that the summer went by way too fast"; }
        if (disaster == 0 && day == 2) { radioString2 = "After having their first day of school delayed yesterday, I'm sure the students of Valley Ridge School are happy to be returning today."; }
        if (disaster == 0 && day > 3) {
            radioString2 = "Today is the first day back to class for the students at Valley Ridge School. They've had a bit of an interesting week in terms of delays, but it sounds like things are finally back on track. \n \n " +
"I guess it goes to show that even if you'd like summer to last forever... \n \n " +
"you can't escape the inevitable";
        }
        if (disaster == 1) { radioString2 = "I do have one school closure to tell you about this morning. Apparently Valley Ridge School is dealing with some plumbing issues today and so the start of classes will be delayed until tomorrow."; }
        if (disaster == 2) { radioString2 = "In the news this morning, a local school is dealing with a clean up after the building's fire sprinklers were triggered yesterday. The official start of classes for Valley Ridge School will be delayed until tomorrow;"; }
        if (disaster == 3) { radioString2 = "A breaking story today: Valley Ridge School is once again unable to open its doors for students due to unsolved electrical issues."; }
        if (disaster == 4) { radioString2 = "An important announcement today: The faculty of Valley Ridge School has decided to ask students to come to school on Saturday to make up for lost time. Unfortunately today they are having trouble unlocking certain classrooms and so the first day of classes has been delayed once again."; }
        if (day == 6 && Gamestate.scienceBakingSoda == true && Gamestate.scienceChemical == true && Gamestate.scienceFan == true && Gamestate.scienceFanCase == true && Gamestate.scienceRefrigerant == true && Gamestate.scienceWater == true)
        {
            radioString2 =
            "I'm sure I'm not the only one who couldn't believe my eyes this morning. \n \n " +
            "Snow, folks! \n \n " +
            "That's right, snow in September. Apparently due to some sort of an explosion in the science lab of Valley Ridge School! \n\n" +
            "Expert meteorologists have predicted that the snow will probably stick around for at least a week, and the unexpected winter conditions will see schools closed across the city for at least as long. \n \n " +
            "it's unbelievable, folks. it's simply unbelievable";

            Gamestate.accomplishment = 9;
        }
        

        combinedRadio = radioString1 + "\n \n" + radioString2;



        //Type out text

        foreach (char c in combinedRadio)
        {
            if (skip == true) {
                radioText.text = combinedRadio;
                skip = false;
                break; 
            }
            radioText.text += c;
            yield return new WaitForSeconds(0.055f);
        }

        while (!skip) //wait for key press
            yield return null;

        skip = false;

        //Fade out radio text and scroll in notebook
        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f)
        {
            if (alpha > 255) { alpha = 255; }
            if (skip == true) { skip = false; alpha = 255; }
            notebook.position = new Vector3(0, -12 + ((alpha / 255) * 12), 0);
            radioText.color = new Color32((byte)(255 - alpha), (byte)(255 - alpha), (byte)(255 - alpha), (byte)((255 - alpha) / 255));
            yield return null;

        }
        radioText.text = ""; //clear text to prevent ghosting after fade out

        while (!skip) //wait for key press
            yield return null;
        skip = false;

        //Fade to white
        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f)
        {
            if (alpha > 255) { alpha = 255; } 
            
            whiteSprite.color = new Color(1, 1, 1, (alpha/255));

            yield return null;

        }
        if (Gamestate.accomplishment == 0 && day != 0) {
            Gamestate.accomplishment = 8;
            nextScene = 3;
        }

        if (Gamestate.accomplishment == 9)
        {
            nextScene=3;
        }


        SceneManager.LoadScene(nextScene);
    }
}
