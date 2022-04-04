using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelHandler : BaseManager<LevelHandler>
{
    public GameObject BaseballField;
    public GameObject OutsideSchool;
    public GameObject Downtown;
    public GameObject Beach;
    public GameObject UtilityRoom;
    public GameObject ScienceRoom;

    public GameObject Player;
    public GameObject GUI;

    public void NextLevel()
    {
        GUI.SetActive(false);
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(3);
    }

    public void DayOne()
    {
        GameObject level = new GameObject("Level");
        GameObject school = Instantiate(BaseballField);
        school.transform.parent = level.transform;

        GameObject gui = Instantiate(GUI);
        gui.SetActive(true);

        GameObject player = Instantiate(Player);
        player.transform.position = new Vector3(-4.5f, 4.5f, 0f);

        StartCoroutine(GameManager.Instance.FadeIn());
    }

    public void DayTwo()
    {
        GameObject level = new GameObject("Level");
        GameObject school = Instantiate(OutsideSchool);
        school.transform.parent = level.transform;
        GameObject gui = Instantiate(GUI);
        gui.SetActive(true);

        GameObject player = Instantiate(Player);
        GameManager.Instance.Player = player.GetComponent<Player>();
        player.transform.position = new Vector3(-4.5f, 4.5f, 0f);

        StartCoroutine(GameManager.Instance.FadeIn());
    }

    public void DayThree()
    {}

    public void DayFour() {}

    public void DayFive() {}

    public void DaySix() {}

    protected override void OnAwake() { }
}
