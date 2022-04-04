using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndOfDay : MonoBehaviour
{
    public SpriteRenderer WhiteSprite;
    public TextMeshProUGUI DayText;

    private string dayString = "";
    private string typedDayString = "";

    private bool skip = false;
    private bool skipped = false; // check to see if button was pressed 

    // Start is called before the first frame update
    private void Start()
    {
        if (Gamestate.accomplishment == 1) { dayString = "You spend the rest of the day swimming and relaxing at the beach."; }
        if (Gamestate.accomplishment == 2) { dayString = "You spend the rest of the day eating ice cream and hanging out with your friends."; }
        if (Gamestate.accomplishment == 3) { dayString = "You spend the rest of the day watching back-to-back summer blockbuster films."; }
        if (Gamestate.accomplishment == 4) { dayString = "You spend the rest of the day playing baseball."; }
        if (Gamestate.accomplishment == 5) { dayString = "You spend the rest of the day playing video games with your buddies."; }
        if (Gamestate.accomplishment == 6) { dayString = "You spend the rest of the day riding your bike."; }
        if (Gamestate.accomplishment == 0) { dayString = "The streetlights turn on and it is time to go home for the night"; }
        if (Gamestate.accomplishment == 8) { dayString = "The first day of school has arrived. As you once again head to class you think about the things you did over the summer break. Did you make the most of your time? \n\nGAME OVER"; }
        if (Gamestate.accomplishment == 9) { dayString = "As you step outside you feel the cool breeze on your skin. The city is covered in a fluffy layer of fresh snow. The time will come when you'll inevitably have to return to school, but for now, at least, you can enjoy a full week of snow days. \n\n CONGRATULATIONS \n\n THANKS FOR PLAYING!"; }

        StartCoroutine(ShowResults());
    }

    // Update is called once per frame
    private void Update()
    {

        if ( Input.anyKeyDown && skipped == false )
        {
            skip = true;
            skipped = true;
        }

        if ( skipped )
        {
            skipped = false;
        }
    }

    private IEnumerator ShowResults()
    {

        foreach (char c in dayString)
        {
            if (skip == true)
            {
                typedDayString = dayString;
                DayText.text = typedDayString;
                skip = false;
                break;
            }
            typedDayString += c;
            DayText.text = typedDayString;
            yield return new WaitForSeconds(0.055f);
        }

        while (!skip) //wait for key press
            yield return null;


        //Fade to black
        WhiteSprite.enabled = true;
        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f)
        {
            if (alpha > 255) { alpha = 255; }

            WhiteSprite.color = new Color(0, 0, 0, (alpha / 255));

            yield return null;

        }

        // 0: BootScene
        // 1: RadioScene
        // 2: GameLogicTest
        // 3: EndOfDay
        Gamestate.day++;

        if (Gamestate.accomplishment != 8 && Gamestate.accomplishment != 9)
            SceneManager.LoadScene(1);
    }
}
