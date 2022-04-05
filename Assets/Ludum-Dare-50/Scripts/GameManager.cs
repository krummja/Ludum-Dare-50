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
    SMOKE,
    SCIENCE
}


public class AchievementFlags
{
    public bool Beach = false;
    public bool IceCream = false;
    public bool Movie = false;
    public bool Baseball = false;
    public bool VideoGames = false;
    public bool Bicycle = false;
}


public class ShutdownFlags
{
    public bool Roaches = false;
    public bool Electrical = false;
    public bool Plumbing = false;
    public bool Keys = false;
    public bool Smoke = false;
}


public class ExperimentFlags
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
    [HideInInspector]
    public Player Player;

    public Clock Clock;
    public GameObject MessageObject;

    public GameItems[] InventorySlots;

    public AchievementFlags Achievements;
    public ShutdownFlags Shutdowns;
    public ExperimentFlags Experiment;

    public AchievementsEnum Achievement;

    public DaysEnum Day;
    public ClosureEnum Disaster;


    public int Score { get; private set; }
    public int LastDayHunted { get; private set; }
    public bool IsGamePaused;

    private PlayerCharacterInputs Inputs;
    private int currentMoves = 10;

    private bool wasAnyKeyPressed = false;
    private bool wasSpacePressed = false;
    private bool isPopUpVisible = false;

    private bool isSkipped = false;
    private bool initialized = false;

    public void AddScore(int num)
    {
        Score += num;
    }

    public void SetAchievement(AchievementsEnum achievement)
    {
        Achievement = achievement;
    }

    public void Sabotage(ClosureEnum planNumber)
    {
        switch ( planNumber )
        {
            case ClosureEnum.PLUMBING:
                RequestNewMessage(18, "You see the large valve that supplies the school with water. You turn it a few" +
                                      "times to stop the flow.");
                Disaster = ClosureEnum.PLUMBING;
                Achievement = AchievementsEnum.BASEBALL;
                Achievements.Baseball = true;
                Shutdowns.Plumbing = true;
                LevelHandler.Instance.Complete();
                break;

            case ClosureEnum.SMOKE:
                bool haveSmokeBomb = Inventory.Instance.CheckInventory(GameItems.SMOKE_BOMB);
                bool haveSlingshot = Inventory.Instance.CheckInventory(GameItems.SLINGSHOT);
                if ( haveSmokeBomb && haveSlingshot )
                {
                    RequestNewMessage(13, "You shoot your smoke bomb into the open window. You can see a cloud of " +
                                          "smoke starting to build inside the classroom!");
                    Inventory.Instance.RemoveFromInventory(GameItems.SMOKE_BOMB);
                    Inventory.Instance.RemoveFromInventory(GameItems.SLINGSHOT);
                    Disaster = ClosureEnum.SMOKE;
                    Achievement = AchievementsEnum.BICYCLE;
                    Achievements.Bicycle = true;
                    Shutdowns.Smoke = true;
                    LevelHandler.Instance.Complete();
                }
                else
                {
                    RequestNewMessage(18, "You see an open window about you. You can't reach it, but you might be " +
                                          "able to throw something into it...");
                }
                break;

            case ClosureEnum.ROACHES:
                bool haveRoaches1 = Inventory.Instance.CheckInventory(GameItems.ROACHES_1);
                bool haveRoaches2 = Inventory.Instance.CheckInventory(GameItems.ROACHES_1);
                bool haveRoaches3 = Inventory.Instance.CheckInventory(GameItems.ROACHES_1);
                if ( haveRoaches1 && haveRoaches2 && haveRoaches3 )
                {
                    RequestNewMessage(16, "You sneak back to the school that night and release your horrible horde of" +
                                          "highly horrifying bugs into the school lobby. Muahaha!");
                    Inventory.Instance.RemoveFromInventory(GameItems.ROACHES_1);
                    Inventory.Instance.RemoveFromInventory(GameItems.ROACHES_2);
                    Inventory.Instance.RemoveFromInventory(GameItems.ROACHES_3);
                    Disaster = ClosureEnum.ROACHES;
                    Achievement = AchievementsEnum.ICE_CREAM;
                    Achievements.IceCream = true;
                    Shutdowns.Roaches = true;
                    LevelHandler.Instance.Complete();
                }
                else
                {
                    RequestNewMessage(18, "You try to sneak back to the school that night to release what few roaches" +
                                          "you collected, but they scattered away. The guard catches you.");
                }
                break;

            case ClosureEnum.KEYS:
                if ( Inventory.Instance.CheckInventory(GameItems.KEY_CUT) )
                {
                    RequestNewMessage(10, "You bury the key in the sand. No school if the doors can't be unlocked!");
                    Inventory.Instance.RemoveFromInventory(GameItems.KEY_CUT);
                    Disaster = ClosureEnum.KEYS;
                    Achievement = AchievementsEnum.BEACH;
                    Achievements.Beach = true;
                    Shutdowns.Keys = true;
                    LevelHandler.Instance.Complete();
                }
                else
                {
                    RequestNewMessage(18, "It's just a hole. You might be able to hide something valuable in here...");
                }
                break;

            case ClosureEnum.ELECTRICAL:
                RequestNewMessage(12, "You pull the fuse out of the fuse box. The lights flicker and shut off.");
                Disaster = ClosureEnum.ELECTRICAL;
                Achievement = AchievementsEnum.VIDEO_GAMES;
                Achievements.VideoGames = true;
                Shutdowns.Electrical = true;
                LevelHandler.Instance.Complete();
                break;

            case ClosureEnum.SCIENCE:
                // Check for all science items.
                bool haveBakingSoda = Inventory.Instance.CheckInventory(GameItems.BAKING_SODA);
                bool haveRefrigerant = Inventory.Instance.CheckInventory(GameItems.REFRIGERANT);
                bool haveWaterBottle = Inventory.Instance.CheckInventory(GameItems.WATER);
                bool haveOldFan = Inventory.Instance.CheckInventory(GameItems.FAN);
                if ( haveBakingSoda && haveRefrigerant && haveWaterBottle && haveOldFan )
                {
                    RequestNewMessage(4, "You attach the fan, plugin the refrigerant, drain the bottle of water, and" +
                                          "mix in the baking soda. It starts to bubble and shake... Better run!");
                    Disaster = ClosureEnum.SCIENCE;
                    Achievement = AchievementsEnum.SNOW_DAY;
                    LevelHandler.Instance.Complete();
                }
                else
                {
                    RequestNewMessage(18, "Some nerd's science project is here. If only you had some sciency baubles " +
                                          "to stick to it... that could be interesting.");
                }

                break;
        }
    }

    public void DecrementMoves()
    {
        currentMoves--;
        Clock.UpdateClock(false);
    }

    public void RequestNewMessage(int icon, string message)
    {
        NewMessage(icon, message);
        isPopUpVisible = true;
        wasSpacePressed = false;
    }

    protected override void OnAwake()
    {
        Achievements = new AchievementFlags();
        Shutdowns = new ShutdownFlags();
        Experiment = new ExperimentFlags();
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
            InventorySlots = Inventory.Instance.InventorySlots;
        }

        LevelHandler.Instance.LoadRadioScene();
    }

    private void Update()
    {
        if ( Input.GetKeyDown("space") && isPopUpVisible )
        {
            isPopUpVisible = false;
            MessageObject.SetActive(false);
        }

        if ( Input.GetKeyDown("space") && LevelHandler.Instance.goalComplete )
        {
            wasAnyKeyPressed = true;
            LevelHandler.Instance.NextDay();
        }

        if ( Input.GetKeyDown(KeyCode.R) )
        {
            LevelHandler.Instance.ReloadScene();
            Inventory.Instance.ClearInventory();
        }

        if ( isSkipped ) isSkipped = false;
    }

    private void FixedUpdate()
    {
        if (Player)
        {
            Inputs = InputManager.Instance.Inputs;
            Player.TryMove(Inputs.MoveInput);
        }
    }

    private void NewMessage(int icon, string message)
    {
        MessageObject.SetActive(true);
        MessageObject.GetComponent<MessageBoxIcon>().icon = icon;
        MessageObject.GetComponent<MessageBoxIcon>().messageTextMesh.text = message;
    }
}
