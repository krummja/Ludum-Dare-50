using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using UnityEngine.SceneManagement;


public enum AchievementsEnum
{
    HOME,
    BEACH,
    ICE_CREAM,
    MOVIE,
    BASEBALL,
    VIDEO_GAMES,
    BICYCLE,
    SCHOOL,
    SNOW_DAY
}


public enum DaysEnum
{
    SUNDAY,
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY
}


public enum ClosureEnum
{
    NONE,
    ROACHES,
    ELECTRICAL,
    PLUMBING,
    KEYS,
    SMOKE
}


public enum ScenesEnum
{
    BOOT,
    RADIO,
    GAME,
    END
}


public sealed class AchievementFlags
{
    public bool Beach = false;
    public bool IceCream = false;
    public bool Movie = false;
    public bool Baseball = false;
    public bool VideoGames = false;
    public bool Bicycle = false;
}


public sealed class ShutdownFlags
{
    public bool Roaches = false;
    public bool Electrical = false;
    public bool Plumbing = false;
    public bool Keys = false;
    public bool Smoke = false;
}


public sealed class ExperimentFlags
{
    public bool Fan = false;
    public bool FanCase = false;
    public bool Refrigerant = false;
    public bool BakingSoda = false;
    public bool Chemical = false;
    public bool Water = false;
}


public class GameManager : BaseManager<GameManager>
{
    public Transform LevelRoot;
    public Player Player;
    public Clock Clock;
    public GameObject MessageObject;
    public SpriteRenderer WhiteSprite;

    public GameItems[] InventorySlots;
    public bool IsInitialized = false;

    public AchievementFlags Achievements { get; private set; }
    public ShutdownFlags Shutdowns { get; private set; }
    public ExperimentFlags Experiment { get; private set; }

    public AchievementsEnum Achievement { get; private set; }
    public ScenesEnum Scene { get; private set; }

    public DaysEnum Day { get; set; }
    public ClosureEnum Disaster { get; set; }

    public int Score { get; private set; }
    public int LastDayHunted { get; private set; }
    public bool IsGamePaused { get; set; }

    private int currentMoves = 10;

    private bool wasAnyKeyPressed = false;
    private bool isSkipped = false;
    private bool initialized = false;

    private bool goalComplete = false;

    public void AddScore(int num)
    {
        Score += num;
    }

    public void SetAchievement(AchievementsEnum achievement)
    {
        Achievement = achievement;
    }

    public void Sabotage(int planNumber)
    {
        if ( IsGamePaused ) return;

        switch ( planNumber )
        {
            case 1:     // PLUMBING
                if ( Shutdowns.Plumbing )
                {
                    RequestNewMessage(18, "There is a water valve here, but it is stuck and cannot be turned.");
                    break;
                }

                RequestNewMessage(18, "You see the large valve that supplies the school with water. You turn it a few" +
                                      "times to stop the flow.");
                Shutdowns.Plumbing = true;
                Disaster = ClosureEnum.PLUMBING;
                Achievement = AchievementsEnum.BASEBALL;
                goalComplete = true;
                break;

            case 2:     // SMOKE BOMB
                if ( Shutdowns.Smoke )
                {
                    RequestNewMessage(18, "There is nothing more to do here.");
                    break;
                }

                if ( Inventory.Instance.CheckInventory(GameItems.SMOKE_BOMB) )
                {
                    RequestNewMessage(18, "You toss your smoke bomb into the open window. You can see a cloud of " +
                                          "smoke starting to build inside the classroom!");
                    Inventory.Instance.RemoveFromInventory(GameItems.SMOKE_BOMB);
                    Shutdowns.Smoke = true;
                    Disaster = ClosureEnum.SMOKE;
                }
                else
                {
                    RequestNewMessage(18, "You see an open window about you. You can't reach it, but you might be " +
                                          "able to throw something into it...");
                }

                break;

            case 3:     // ELECTRICAL
                if ( Shutdowns.Electrical )
                {
                    RequestNewMessage(18, "There is nothing more to do here.");
                    break;
                }

                // Check for utility key.
                break;

            case 4:     // ROACHES
                break;

            case 5:     // HIT KEYS WITH SLINGSHOT
                break;

            case 6:     // FAN CASE
                break;

            case 7:     // FAN
                break;

            case 8:     // FAN
                break;

            case 9:     // PLUMBING
                break;

            case 10:    // CHEMICALS
                break;

            case 11:    // BAKING SODA
                break;
        }
    }

    public void NextDay()
    {
        Day++;
        goalComplete = false;
        LevelHandler.Instance.NextLevel();
    }

    public void DecrementMoves()
    {
        currentMoves--;
        if ( Clock )
            Clock.UpdateClock(false);
    }

    public void RequestNewMessage(int icon, string message)
    {
        StartCoroutine(NewMessage(icon, message));
    }

    protected override void OnAwake()
    {
        Achievements = new AchievementFlags();
        Shutdowns = new ShutdownFlags();
        Experiment = new ExperimentFlags();
        InventorySlots = Inventory.Instance.InventorySlots;

        Day = DaysEnum.SUNDAY;
        Disaster = ClosureEnum.NONE;
    }

    private void Start()
    {
        Disaster = ClosureEnum.NONE;

        if ( !initialized )
        {
            initialized = true;

            for ( int i = 0; i < 15; i++ )
                Inventory.Instance.InventorySlots[i] = 0;

            Score = 0;
            Achievement = AchievementsEnum.HOME;
            Day = DaysEnum.SUNDAY;
            LastDayHunted = -1;
            Disaster = ClosureEnum.NONE;
        }

        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if ( Input.GetKeyDown("space") && !wasAnyKeyPressed )
        {
            wasAnyKeyPressed = true;
            if ( goalComplete )
            {
                NextDay();
            }
        }

        if ( isSkipped ) isSkipped = false;
    }

    private void FixedUpdate()
    {
        if (Player)
        {
            PlayerCharacterInputs inputs = InputManager.Instance.Inputs;
            Player.TryMove(inputs.MoveInput);
        }
    }

    private IEnumerator NewMessage(int icon, string message)
    {
        MessageObject.SetActive(true);
        IsGamePaused = true;

        MessageObject.GetComponent<MessageBoxIcon>().icon = icon;
        MessageObject.GetComponent<MessageBoxIcon>().messageTextMesh.text = message;

        while ( !wasAnyKeyPressed )
            yield return null;

        wasAnyKeyPressed = false;
        MessageObject.SetActive(false);
        IsGamePaused = false;
    }

    public IEnumerator FadeIn()
    {
        WhiteSprite.enabled = true;

        for ( float alpha = 255f; alpha > 0; alpha -= Time.deltaTime * 200f )
        {
            alpha = Mathf.Clamp(alpha, 0, 255);
            WhiteSprite.color = new Color(1, 1, 1, alpha / 255);
            yield return null;
        }

        WhiteSprite.enabled = false;
    }

    public IEnumerator FadeOut()
    {
        WhiteSprite.enabled = true;

        for ( float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f )
        {
            alpha = Mathf.Clamp(alpha, 0, 255);
            WhiteSprite.color = new Color(0, 0, 0, alpha / 255);
            yield return null;
        }

        SceneManager.LoadScene(3);
    }
}
