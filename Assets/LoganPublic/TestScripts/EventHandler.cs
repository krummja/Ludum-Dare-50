using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public static EventHandler instance; // Singleton magic

    public TextMeshProUGUI dataLog;
    public SpriteRenderer whiteSprite;
    public SpriteRenderer[] invSprites = new SpriteRenderer [15];

    public GameObject messageObject;

    private bool skip = false;
    private bool skipped = false; // check to see if button was pressed 

    public bool GamePause = false; // **************** You can watch this variable to determine input pause.

    // Start is called before the first frame update
    private void Start()
    {
        Gamestate.disaster = 0; // repair yesterday's disaster;

        if (Gamestate.initialized == false)
        {
            Gamestate.initialized = true;

            //Inventory
            for (int i = 0; i < 15; i++) {
                Gamestate.inventory[i] = 0;
            }

            //Score
            Gamestate.score = 0;

            Gamestate.accomplishment = 0;

            Gamestate.day = 0;

            Gamestate.lastDayHunted = -1;

            Gamestate.disaster = 0;
        }

        StartCoroutine(FadeIn());

    }

    public void AddScore(int num)
    {
        Gamestate.score = +num;
    }

    public void SetAccomplishment(int num)
    {
        Gamestate.accomplishment = num;
    }

    public void SetDisaster(int num)
    {
        Gamestate.disaster = num;
    }

    // Update is called once per frame
    private void Update()
    {
        dataLog.text =
            "Score: " + Gamestate.score + "  " +
            "Accomp: " + Gamestate.accomplishment + "  " +
            "Day: " + Gamestate.day + "  " +
            "Disaster: " + Gamestate.disaster;

        if (Input.GetKeyDown("space") && skipped == false) { skip = true; skipped = true; }
        if (Input.GetKeyDown("space") == false && skipped == true) { skipped = false; }
    }

    public void AddToInventory(int newItem) // This function checks for weird item properties before calling PlaceinInventory
    {
        if (GamePause == true) return;
        if (newItem == 1)//Slingshot
        {
            if (CheckInventory(newItem) == false)
            {
                PlaceInInventory(newItem);
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You found a slingshot!"));
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You already have this!"));
        }
        if (newItem == 2)//Fan Case
        {
            if (CheckInventory(newItem) == false)
            {
                PlaceInInventory(newItem);
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You found a fan case!"));
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You already have this!"));
        }
        if (newItem == 3)//Fan
        {
            if (CheckInventory(newItem) == false)
            {
                PlaceInInventory(newItem);
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You found some fan blades!"));
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You already have this!"));
        }
        if (newItem == 4)//Refridge
        {
            if (CheckInventory(newItem) == false)
            {
                PlaceInInventory(newItem);
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You found a canister of refridgerant!"));
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You don't need any more refridgerant"));
        }
        if (newItem == 5)//Water
        {
            if (CheckInventory(newItem) == false)
            {
                PlaceInInventory(newItem);
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You found a huge bottle of water!"));
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You're already carrying one huge bottle of water. Two would be too heavy!"));
        }
        if (newItem == 6)//Industrial Chemical
        {
            if (CheckInventory(newItem) == false)
            {
                PlaceInInventory(newItem);
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You found an industrial chemical mix!"));
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You don't need any more chemicals!"));
        }
        if (newItem == 7)//Baking Soda
        {
            if (CheckInventory(newItem) == false)
            {
                PlaceInInventory(newItem);
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You found a box of baking soda!"));
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You already have baking soda!"));
        }
        if (newItem == 8)// Gum
        {
            if (CheckInventory(8) == false && CheckInventory(9) == false && CheckInventory(11) == false) //Check for gum in all forms
            {
                PlaceInInventory(newItem);
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You found some chewing gum!"));
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You already have enough gum!"));
        }
        if (newItem == 9)//Attempt to press school key
        {
            if (CheckInventory(newItem) == false)
            {
                if (CheckInventory(8) == true)// Yes you have gum?
                {
                    PlaceInInventory(newItem);
                    if (GamePause == false) StartCoroutine(NewMessage(newItem, "The principle of the school is here. While they are distracted you make a chewing gum impression of the school key that hangs from their belt"));
                    RemoveFromInventory(8); // lose the pristine gum
                }
                else if (CheckInventory(11) == true)// Yes you have gum with a different impression?
                {
                    PlaceInInventory(newItem);
                    if (GamePause == false) StartCoroutine(NewMessage(newItem, "The principle of the school is here. While they are distracted you make a chewing gum impression of the school key that hangs from their belt"));
                    RemoveFromInventory(11); // lose the other impression
                }
                else
                {
                    if (GamePause == false) StartCoroutine(NewMessage(18, "The principle of the school is here. There is a key dangling from their belt. If only you had something to make a quick impression with"));
                }
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You already have in impression of this key!"));
        }
        if (newItem == 10)//Attempt to get a key cut
        {
            if (CheckInventory(9) == true && CheckInventory(10) == false)// Yes you have school impression but no school key?
            {
                PlaceInInventory(10);
                if (GamePause == false) StartCoroutine(NewMessage(10, "You are somehow able to convince the shop to cut you a key based on the gum impression. You now have a Key to the School!"));
                //lose nothing
            }
            else if (CheckInventory(11) == true && CheckInventory(12) == false)// Yes you have utility impression and no utillity key?
            {
                PlaceInInventory(12);
                if (GamePause == false) StartCoroutine(NewMessage(12, "You are somehow able to convince the shop to cut you a key based on the gum impression. You now have a Utility Room Key!"));
                //lose nothing
            }
            else if (CheckInventory(9) == false && CheckInventory(11) == false)// No impressions
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "This shady shop cuts keys. Sometimes they can copy a key even if they don't have an original to look at"));
            }
            else if(GamePause == false) StartCoroutine(NewMessage(18, "You have already cut a copy of this key"));

        }
        if (newItem == 11)// Attempt to press the Utility Key
        {
            if (CheckInventory(newItem) == false)
            {
                if (CheckInventory(8) == true)// Yes you have gum?
                {
                    PlaceInInventory(newItem);
                    if (GamePause == false) StartCoroutine(NewMessage(newItem, "The school janitor is here. While they are distracted you make a chewing gum impression of the utility key that hangs from their belt"));
                    RemoveFromInventory(8); // lose the pristine gum
                }
                else if (CheckInventory(9) == true)// Yes you have gum with a different impression?
                {
                    PlaceInInventory(newItem);
                    if (GamePause == false) StartCoroutine(NewMessage(newItem, "The school janitor is here. While they are distracted you make a chewing gum impression of the utility key that hangs from their belt"));
                    RemoveFromInventory(9); // lose the other impression
                }
                else
                {
                    if (GamePause == false) StartCoroutine(NewMessage(18, "The school janitor is here. There is a key dangling from their belt. If only you had something to make a quick impression with"));
                }
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You already have in impression of this key!"));
        }
        if (newItem == 12)// UNUSED
        {
            RemoveFromInventory(1);
        }
        if (newItem == 13)// Smoke Bomb
        {
            if (CheckInventory(newItem) == false)
            {
                PlaceInInventory(newItem);
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You found one of your homemade smoke bombs!"));
            }
            else if (GamePause == false) StartCoroutine(NewMessage(18, "You already have one of these!"));
        }
        if (newItem == 14)// Attempt to gather roaches! Fun times!
        {
            if (Gamestate.lastDayHunted == Gamestate.day) // was this site already picked through today?
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There are no more roaches left. Try again another day."));
                    return;
            }

            Gamestate.lastDayHunted = Gamestate.day; // mark site as depleted today. 

            if (CheckInventory(14) == false && CheckInventory(15) == false && CheckInventory(16) == false ) // if you have no roach collection
            {
                PlaceInInventory(14); // get a jar full!
                if (GamePause == false) StartCoroutine(NewMessage(newItem, "You collected enough roaches to fill a jar!"));
            }
            else if (CheckInventory(14) == true && CheckInventory(15) == false && CheckInventory(16) == false) // if you have a jar full
            {
                RemoveFromInventory(14); // lose the jar
                PlaceInInventory(15); // get a shoebox full!

                if (GamePause == false) StartCoroutine(NewMessage(15, "You added to your roach collection. Now you have enough to fill a shoebox!"));
            }
            else if (CheckInventory(14) == false && CheckInventory(15) == true && CheckInventory(16) == false) // if you have a shoebox full
            {
                RemoveFromInventory(15); // lose the shoebox
                PlaceInInventory(16); // get a huge box
                if (GamePause == false) StartCoroutine(NewMessage(16, "You added to your roach collection. Now you have enough to fill a giant cardboard box! It's kind of gross!"));
            }
            else if (CheckInventory(14) == false && CheckInventory(15) == false && CheckInventory(16) == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "Listen, kid. You really don't need any more roaches!"));
            }

        }
    }

    public void Sabotage(int planNumber) // This function sets up a school sabotage
    {
        if (GamePause == true) return; //no function while paused
        if (planNumber == 1) // water. Shut off plumbing
        {
           if(Gamestate.closedPlumbing == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is a water valve here, but it is stuck and cannot be turned"));
                return;
            }

            if (GamePause == false) StartCoroutine(NewMessage(18, "You see the large valve that supplies the school with water. You turn it a few times to stop the flow"));
            Gamestate.closedPlumbing = true;
            Gamestate.disaster = 1;
        }

        if (planNumber == 2) // smoke
        {
            if (Gamestate.closedSmoke == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(13) == true) // check for smoke bomb
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "You toss your smoke bomb into the open window. You can see a cloud of smoke starting to build inside the classroom"));
                RemoveFromInventory(13); // remove smoke
                Gamestate.closedSmoke = true;
                Gamestate.disaster = 2;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "You see an open window above you. You can't reach it, but it might be possible to throw something into it."));
            }
        }

        if (planNumber == 3) // electrical
        {
            if (Gamestate.closedElectrical == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(12) == true) // check for utility key
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you open the fusebox and pull out a few of the larger fuses."));
                RemoveFromInventory(12); // remove key
                Gamestate.closedElectrical = true;
                Gamestate.disaster = 3;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "You see a locked fusebox"));
            }
        }

        if (planNumber == 4) // roach
        {
            if (Gamestate.closedRoaches == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(14) == true || CheckInventory(15)) // check for roaches
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "You consider dropping your cockroach collection into the basement window, but you are worried there won't be enough to cause much of a problem"));
            }
            else if(CheckInventory(16)) // check for lots of roaches
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "You pour the giant box of roaches into the basement window. You can see them scattering as soon as they hit the ground inside."));
                RemoveFromInventory(16); // remove smoke
                Gamestate.closedRoaches = true;
                Gamestate.disaster = 4;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "You see a small basement window. It's two small to climb through but maybe something could be poured down through the opening"));
            }
        }

        if (planNumber == 5) // hit keys with slingshot
        {
            if (Gamestate.closedKeys == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(1) == true) // check for slingshot
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you shoot your slingshot through the office mail slot. You manage to hit the master key box. Keys are knocked everywhere!"));
                RemoveFromInventory(1); // remove slingshot
                Gamestate.closedKeys = true;
                Gamestate.disaster = 5;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "You peer through a mail slot and into the main office. You see a master key box. If you could reach it you could make a real mess"));
            }
        }

        if (planNumber == 6) // Fan case
        {
            if (Gamestate.scienceFanCase == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(2) == true) // check for fan case
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you attach the fan case to the experiment"));
                RemoveFromInventory(2); // remove case
                Gamestate.scienceFanCase = true;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is a science experiment here involving a fan. You think the fan could be secured a bit better."));
            }

        }

        if (planNumber == 7) // Fan
        {
            if (Gamestate.scienceFan == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(3) == true) // check for fan 
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you attach an additional fan to the experiment"));
                RemoveFromInventory(3); // remove case
                Gamestate.scienceFan = true;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is a science experiment here involving a fan. You think the fan could be moving a bit more air."));
            }

        }

        if (planNumber == 8) // Fan
        {
            if (Gamestate.scienceRefrigerant == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(4) == true) // check for refridge
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you connect the canister of refridgerant to the experiment."));
                RemoveFromInventory(4); // remove fridge
                Gamestate.scienceRefrigerant = true;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is a science experiment here. Is there a way to make it more cool?"));
            }

        }

        if (planNumber == 9) // Water
        {
            if (Gamestate.scienceWater == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(5) == true) // check for water
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you connect the huge water bottle to the experiment."));
                RemoveFromInventory(5); // remove water
                Gamestate.scienceWater = true;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is a science experiment here. It seems a bit dry right now."));
            }

        }

        if (planNumber == 10) // Chemical
        {
            if (Gamestate.scienceChemical == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(6) == true) // check for industrial chemical
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you mix the industrial chemical mix into the experiment."));
                RemoveFromInventory(6); // remove chemical
                Gamestate.scienceChemical = true;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is a science experiment here. It seems like it's missing something industrial."));
            }

        }

        if (planNumber == 11) // Baking soda
        {
            if (Gamestate.scienceBakingSoda == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is nothing more to do here."));
                return;
            }

            if (CheckInventory(7) == true) // check for baking soda
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "You pour the whole box of baking soda into the experiment. Things are starting to bubble."));
                RemoveFromInventory(7); // remove bakin soda
                Gamestate.scienceBakingSoda = true;
            }
            else
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There is a science experiment here involving vinegar. It's not doing much right now."));
            }

        }

    }

    void PlaceInInventory(int newItem) //This is the function that actually puts a new item into the array.
    {
        for (int i = 0; i<15; i++)
        {
            if (Gamestate.inventory[i] == 0)
            {
                Gamestate.inventory[i] = newItem;
                break;
            }
        }
    }



    void RemoveFromInventory(int item) //This function is used to delete objects from inventory
    {
        int removeIndex = -1;

        for (int i = 0; i < 15; i++) // Locate item to remove
        {
            if (Gamestate.inventory[i] == item)
            {
                removeIndex = i;
                break;
            }
        }

        if (removeIndex >= 0) //If the item was located and identified
        {
            for (int i = removeIndex; i < 14; i++) // Shift items over
            {
                Gamestate.inventory[i] = Gamestate.inventory[i + 1];
            }

            Gamestate.inventory[14] = 0;//clear the last inventory slot;
        }
    }




    public bool CheckInventory(int item)
    {
        for(int i = 0; i < 15; i++)
        {
            if (Gamestate.inventory[i] == item) return true;
        }

        return false;
    }

    public void EndDay(int activity)
    {
        if (GamePause == true) return; //no function while paused
        if (activity == 0)
        {
            Gamestate.accomplishment = 0;
            StartCoroutine(FadeOut());
        }
        if (activity == 1)
        {
            if (Gamestate.beach == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "the beach is closed today."));
                return;
            }
            Gamestate.accomplishment = 1;
            Gamestate.beach = true;
            StartCoroutine(FadeOut());
        }
        if (activity == 2)
        {
            if (Gamestate.iceCream == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you're not in the mood for ice cream today."));
                return;
            }
            Gamestate.accomplishment = 2;
            Gamestate.iceCream = true;
            StartCoroutine(FadeOut());
        }
        if (activity == 3)
        {
            if (Gamestate.movie == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you have already seen everything that's playing."));
                return;
            }
            Gamestate.accomplishment = 3;
            Gamestate.movie = true;
            StartCoroutine(FadeOut());
        }
        if (activity == 4)
        {
            if (Gamestate.baseball == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "There are no games planned for today."));
                return;
            }
            Gamestate.accomplishment = 4;
            Gamestate.baseball = true;
            StartCoroutine(FadeOut());
        }
        if (activity == 5)
        {
            if (Gamestate.videoGames == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you're not in the mood to play video games today."));
                return;
            }
            Gamestate.accomplishment = 5;
            Gamestate.videoGames = true;
            StartCoroutine(FadeOut());
        }
        if (activity == 6)
        {
            if (Gamestate.bicycle == true)
            {
                if (GamePause == false) StartCoroutine(NewMessage(18, "you're not in the mood to ride your bike today."));
                return;
            }
            Gamestate.accomplishment = 6;
            Gamestate.bicycle = true;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator NewMessage(int icon, string message)
    {
        messageObject.SetActive(true); // Turn message box on
        GamePause = true;

        messageObject.GetComponent<MessageBoxIcon>().icon = icon; //send text to message box
        messageObject.GetComponent<MessageBoxIcon>().messageTextMesh.text = message; //send text to message box

        while (!skip) //wait for key press
            yield return null;
        skip = false; //wait for key press from update()
        messageObject.SetActive(false); // Turn message box off after key press
        GamePause = false;
    }

    IEnumerator FadeIn()
    {
        whiteSprite.enabled = true;

        for (float alpha = 255f; alpha > 0; alpha -= Time.deltaTime * 200f)
        {
            if (alpha < 0) { alpha = 0; }

            whiteSprite.color = new Color(1, 1, 1, (alpha / 255));

            yield return null;

        }

        whiteSprite.enabled = false;
    }

    IEnumerator FadeOut()
    {
        whiteSprite.enabled = true;

        for (float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f)
        {
            if (alpha > 255) { alpha = 255; }

            whiteSprite.color = new Color(0, 0, 0, (alpha / 255));

            yield return null;

        }

        SceneManager.LoadScene(3);
    }

}
