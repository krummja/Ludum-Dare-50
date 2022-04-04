using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


namespace Gameplay
{
    public class EndOfDay : MonoBehaviour
    {
        public SpriteRenderer WhiteSprite;
        public TextMeshProUGUI DayText;

        public float WriteSpeed = 0.055f;

        private string dayString = "";
        private string typedDayString = "";

        private bool wasAnyKeyPressed = false;
        private bool isSkipped = false;

        private void Start()
        {
            switch ( GameManager.Instance.Achievement )
            {
                case AchievementsEnum.HOME:
                    dayString = "The streetlights turn on and it is time to go home for the night";
                    break;
                case AchievementsEnum.BEACH:
                    dayString = "You spend the rest of the day swimming and relaxing at the beach.";
                    break;
                case AchievementsEnum.ICE_CREAM:
                    dayString = "You spend the rest of the day eating ice cream and hanging out with your friends.";
                    break;
                case AchievementsEnum.MOVIE:
                    dayString = "You spend the rest of the day watching back-to-back summer blockbuster films.";
                    break;
                case AchievementsEnum.BASEBALL:
                    dayString = "You spend the rest of the day playing baseball.";
                    break;
                case AchievementsEnum.VIDEO_GAMES:
                    dayString = "You spend the rest of the day playing video games with your buddies.";
                    break;
                case AchievementsEnum.BICYCLE:
                    dayString = "You spend the rest of the day riding your bike.";
                    break;
                case AchievementsEnum.SCHOOL:
                    dayString = "The first day of school has arrived. As you once again head to class you think " +
                                "about the things you did over the summer break. Did you make the most of your time? " +
                                "\n\nGAME OVER";
                    break;
                case AchievementsEnum.SNOW_DAY:
                    dayString = "As you step outside you feel the cool breeze on your skin. The city is covered " +
                                "in a fluffy layer of fresh snow. The time will come when you'll inevitably have " +
                                "to return to school, but for now, at least, you can enjoy a full week of snow days. " +
                                "\n\n CONGRATULATIONS " +
                                "\n\n THANKS FOR PLAYING!";
                    break;
            }

            StartCoroutine(ResultsCoroutine());
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

        private IEnumerator ResultsCoroutine()
        {
            foreach ( char c in dayString )
            {
                // If the player presses any key, immediately set the typed
                // text to the complete narrative string.
                if ( wasAnyKeyPressed )
                {
                    typedDayString = dayString;
                    DayText.text = typedDayString;
                    wasAnyKeyPressed = false;
                    break;
                }

                typedDayString += c;
                DayText.text = typedDayString;
                yield return new WaitForSeconds(WriteSpeed);
            }

            // Narrative text has been fully processed.
            // Await player input to exit the radio scene.
            while ( !wasAnyKeyPressed )
                yield return null;

            WhiteSprite.enabled = true;
            for ( float alpha = 0f; alpha < 255; alpha += Time.time * 200f )
            {
                alpha = Mathf.Clamp(alpha, 0, 255);
                WhiteSprite.color = new Color(0, 0, 0, (alpha / 255));
                yield return null;
            }

            if ( GameManager.Instance.Achievement != AchievementsEnum.SCHOOL &&
                 GameManager.Instance.Achievement != AchievementsEnum.SNOW_DAY )
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
