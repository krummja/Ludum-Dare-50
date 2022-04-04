using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


namespace Gameplay
{
    public class NewDay : MonoBehaviour
    {
        public float WriteSpeed = 0.055f;

        public SpriteRenderer CrossBike;
        public SpriteRenderer CrossGames;
        public SpriteRenderer CrossBaseball;
        public SpriteRenderer CrossMovies;
        public SpriteRenderer CrossIceCream;
        public SpriteRenderer CrossBeach;

        public SpriteRenderer WhiteSprite;
        public TextMeshProUGUI DayText;
        public TextMeshProUGUI RadioText;
        public Transform Notebook;

        private SpriteRenderer background;

        private string dayString = "";
        private string radioString1 = "";
        private string radioString2 = "";
        private string combinedRadio = "";

        private bool wasAnyKeyPressed = false;
        private bool isSkipped = false;

        private void Start()
        {
            switch ( GameManager.Instance.Day )
            {
                case DaysEnum.SUNDAY:
                    dayString = "Sunday";
                    break;
                case DaysEnum.MONDAY:
                    dayString = "Monday";
                    break;
                case DaysEnum.TUESDAY:
                    dayString = "Tuesday";
                    break;
                case DaysEnum.WEDNESDAY:
                    dayString = "Wednesday";
                    break;
                case DaysEnum.THURSDAY:
                    dayString = "Thursday";
                    break;
                case DaysEnum.FRIDAY:
                    dayString = "Friday";
                    break;
                case DaysEnum.SATURDAY:
                    dayString = "Saturday";
                    break;
            }

            DayText.text = dayString;

            switch ( GameManager.Instance.Achievement )
            {
                case AchievementsEnum.BICYCLE:
                    CrossBike.enabled = true;
                    break;
                case AchievementsEnum.VIDEO_GAMES:
                    CrossGames.enabled = true;
                    break;
                case AchievementsEnum.BASEBALL:
                    CrossBaseball.enabled = true;
                    break;
                case AchievementsEnum.MOVIE:
                    CrossMovies.enabled = true;
                    break;
                case AchievementsEnum.ICE_CREAM:
                    CrossIceCream.enabled = true;
                    break;
                case AchievementsEnum.BEACH:
                    CrossBeach.enabled = true;
                    break;
            }

            background = GetComponent<SpriteRenderer>();
            background.color = new Color(0, 0, 0, 1);
            DayText.color = new Color(0, 0, 0, 1);

            StartCoroutine(RadioCoroutine());
        }

        private void Update()
        {
            if ( Input.anyKeyDown && !isSkipped )
            {
                // User hit a key to skip the text crawl.
                wasAnyKeyPressed = true;

                // Set the flag so that ShowResults can continue.
                isSkipped = true;
            }

            // Reset the flag.
            if ( isSkipped ) isSkipped = false;
        }

        private IEnumerator RadioCoroutine()
        {
            // Fade in Day
            for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 150f)
            {
                if ( wasAnyKeyPressed )
                {
                    wasAnyKeyPressed = false;
                    alpha = 255;
                }

                DayText.color = new Color32((byte)(alpha), (byte)(alpha), (byte)(alpha), 255);
                yield return null;
            }

            for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 250f)
            {
                if ( wasAnyKeyPressed ) wasAnyKeyPressed = false;

                alpha = Mathf.Clamp(alpha, 0, 255);
                background.color = new Color(alpha / 255, alpha / 255, alpha / 255, 1);
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            // Fade out Day
            for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f)
            {
                if ( wasAnyKeyPressed )
                {
                    wasAnyKeyPressed = false;
                    alpha = 255;
                }

                background.color = new Color((350 - alpha) / 255, (350 - alpha) / 255, (350 - alpha) / 255, 1);
                DayText.color = new Color(255 - alpha, 255 - alpha, 255 - alpha, (255 - alpha) / 255);
                yield return null;
            }

            // Process radio text
            ProcessRadioText();

            // Type out text
            foreach ( char c in combinedRadio )
            {
                if ( wasAnyKeyPressed )
                {
                    RadioText.text = combinedRadio;
                    wasAnyKeyPressed = false;
                    break;
                }

                RadioText.text += c;
                yield return new WaitForSeconds(WriteSpeed);
            }

            // Accept any key skip for radio text.
            while ( !wasAnyKeyPressed ) yield return null;
            wasAnyKeyPressed = false;

            // Fade out radio text, scroll in notebook.
            for ( float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f )
            {
                if ( wasAnyKeyPressed )
                {
                    wasAnyKeyPressed = false;
                    alpha = 255;
                }

                Notebook.position = new Vector3(0, -12 + ((alpha / 255) * 12), 0);
                RadioText.color = new Color(255 - alpha, 255 - alpha, 255 - alpha, (255 - alpha) / 255);
                yield return null;
            }

            // Hacky -- clear text to prevent ghosting after fade.
            RadioText.text = "";

            // Narrative text has been fully processed.
            // Await player input to exit the radio scene.
            while ( !wasAnyKeyPressed ) yield return null;
            wasAnyKeyPressed = false;

            // Fade to white
            for ( float alpha = 0f; alpha < 255; alpha += Mathf.Clamp(Time.deltaTime * 200f, 0, 255) )
            {
                WhiteSprite.color = new Color(1, 1, 1, alpha / 255);
                yield return null;
            }

            if ( GameManager.Instance.Achievement == AchievementsEnum.HOME
                 && GameManager.Instance.Day != DaysEnum.SUNDAY
                 || GameManager.Instance.Achievement == AchievementsEnum.SNOW_DAY )
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                switch ( GameManager.Instance.Day )
                {
                    case DaysEnum.SUNDAY:
                        LevelHandler.Instance.DayOne();
                        break;
                    case DaysEnum.MONDAY:
                        LevelHandler.Instance.DayTwo();
                        break;
                    case DaysEnum.TUESDAY:
                        break;
                    case DaysEnum.WEDNESDAY:
                        break;
                    case DaysEnum.THURSDAY:
                        break;
                    case DaysEnum.FRIDAY:
                        break;
                }
            }
        }

        private void ProcessRadioText()
        {
            switch ( GameManager.Instance.Day )
            {
                case DaysEnum.SUNDAY:
                {
                    radioString1 = "Good morning listeners! \n \n " +
                                   "The skies are clear and it is a beautiful Sunday morning. \n \n " +
                                   "Coming up this week: Monday will mark the first day back to school for all the " +
                                   "young students in our city.\n \n " +
                                   "That's right, folks, it's the final day of summer vacation. \n \n " +
                                   "Make the most of it.";
                    break;
                }

                case DaysEnum.MONDAY:
                {
                    radioString1 = "Good morning listeners! \n \n " +
                                   "The sun is out on this fine Monday morning. \n \n " +
                                   "It's the first day of classes for many today, and the city is bustling with " +
                                   "that back-to-school energy";
                    break;
                }

                case DaysEnum.TUESDAY:
                {
                    radioString1 = "Good morning listeners! \n \n " +
                                   "It's Tuesday. The week is still just getting started so hang in there, folks!";
                    break;
                }

                case DaysEnum.WEDNESDAY:
                {
                    radioString1 = "Good morning listeners! \n \n " +
                                   "It is Wednesday and the weather outside is great!";
                    break;
                }

                case DaysEnum.THURSDAY:
                {
                    radioString1 = "Good morning listeners! \n \n " +
                                   "It's a bit cloudy on this Thursday morning but it should be sunny again by noon.";
                    break;
                }

                case DaysEnum.FRIDAY:
                {
                    radioString1 = "Good morning listeners! \n \n " +
                                   "It's Friday! \n \n" +
                                   "And I know everyone is tuning in today to hear the same story. Here it is:";
                    break;
                }

                case DaysEnum.SATURDAY:
                {
                    radioString1 = "Good morning listeners! \n \n " +
                                   "it's Saturday.";
                    break;
                }
            }


            if ( GameManager.Instance.Disaster == ClosureEnum.NONE
                 && GameManager.Instance.Day != DaysEnum.SUNDAY )
            {
                if ( GameManager.Instance.Day == DaysEnum.MONDAY )
                {
                    radioString2 = "Good luck to all those returning students out there \n \n " +
                                   "I'm sure they're thinking that the summer went by way too fast";
                }
                else if ( GameManager.Instance.Day == DaysEnum.TUESDAY )
                {
                    radioString2 = "After having their first day of school delayed yesterday, I'm sure the " +
                                   "students of Valley Ridge School are happy to be returning today.";
                }
                else
                {
                    radioString2 = "Today is the first day back to class for the students at Valley Ridge School. " +
                                   "They've had a bit of an interesting week in terms of delays, but it sounds " +
                                   "like things are finally back on track. \n \n " +
                                   "I guess it goes to show that even if you'd like summer to last forever... \n \n " +
                                   "you can't escape the inevitable";
                }
            }

            if ( GameManager.Instance.Disaster == ClosureEnum.ROACHES )
                return;
            if ( GameManager.Instance.Disaster == ClosureEnum.PLUMBING )
                radioString2 = "I do have one school closure to tell you about this morning. Apparently Valley " +
                               "Ridge School is dealing with some plumbing issues today and so the start of classes " +
                               "will be delayed until tomorrow.";
            if ( GameManager.Instance.Disaster == ClosureEnum.SMOKE )
                radioString2 = "In the news this morning, a local school is dealing with a clean up after the " +
                               "building's fire sprinklers were triggered yesterday. The official start of " +
                               "classes for Valley Ridge School will be delayed until tomorrow";
            if ( GameManager.Instance.Disaster == ClosureEnum.ELECTRICAL )
                radioString2 = "A breaking story today: Valley Ridge School is once again unable to open its doors " +
                               "for students due to unsolved electrical issues.";
            if ( GameManager.Instance.Disaster == ClosureEnum.KEYS )
                radioString2 = "An important announcement today: The faculty of Valley Ridge School has decided to " +
                               "ask students to come to school on Saturday to make up for lost time. Unfortunately " +
                               "today they are having trouble unlocking certain classrooms and so the first day of " +
                               "classes has been delayed once again.";

            // Science!
            if ( GameManager.Instance.Day == DaysEnum.FRIDAY )
            {
                radioString2 =
                    "I'm sure I'm not the only one who couldn't believe my eyes this morning. \n \n " +
                    "Snow, folks! \n \n " +
                    "That's right, snow in September. Apparently due to some sort of an explosion in the science " +
                    "lab of Valley Ridge School! \n\n" +
                    "Expert meteorologists have predicted that the snow will probably stick around for at " +
                    "least a week, and the unexpected winter conditions will see schools closed across the city " +
                    "for at least as long. \n \n " +
                    "It's unbelievable, folks. it's simply unbelievable";
            }

            combinedRadio = radioString1 + "\n \n" + radioString2;
        }
    }
}
