using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public enum ScenesEnum
{
    BOOT_SCENE,
    RADIO_SCENE,
    END_OF_DAY_SCENE,
    BASEBALL_SCENE,
    OUTSIDE_SCHOOL_SCENE,
    DOWNTOWN_SCENE,
    BEACH_SCENE,
    UTILITY_ROOM_SCENE,
    SCIENCE_LAB_SCENE,
}


public class LevelHandler : BaseManager<LevelHandler>
{
    public GameObject BaseballField;
    public GameObject OutsideSchool;
    public GameObject Downtown;
    public GameObject Beach;
    public GameObject UtilityRoom;
    public GameObject ScienceRoom;

    public GameObject WhiteSprite;
    private SpriteRenderer fadeSprite;
    private ScenesEnum Current;

    public bool goalComplete { get; private set; }

    public void NextDay()
    {
        if ( !goalComplete ) return;
        GameManager.Instance.Day++;
        goalComplete = false;

        SceneManager.LoadScene((int) ScenesEnum.END_OF_DAY_SCENE);
    }

    public void ReloadScene()
    {
        StartCoroutine(FadeIn());
        SceneManager.LoadScene((int) Current);
    }

    public void Complete()
    {
        goalComplete = true;
    }

    public void LoadRadioScene()
    {
        StartCoroutine(FadeIn());
        SceneManager.LoadScene((int) ScenesEnum.RADIO_SCENE);
    }

    public void LoadBaseballScene()
    {
        StartCoroutine(FadeIn());
        Current = ScenesEnum.BASEBALL_SCENE;
        SceneManager.LoadScene((int) ScenesEnum.BASEBALL_SCENE);
    }

    public void LoadOutsideScene()
    {
        StartCoroutine(FadeIn());
        Current = ScenesEnum.OUTSIDE_SCHOOL_SCENE;
        SceneManager.LoadScene((int) ScenesEnum.OUTSIDE_SCHOOL_SCENE);
    }

    public void LoadDowntownScene()
    {
        StartCoroutine(FadeIn());
        Current = ScenesEnum.DOWNTOWN_SCENE;
        SceneManager.LoadScene((int) ScenesEnum.DOWNTOWN_SCENE);
    }

    public void LoadBeachScene()
    {
        StartCoroutine(FadeIn());
        Current = ScenesEnum.BEACH_SCENE;
        SceneManager.LoadScene((int) ScenesEnum.BEACH_SCENE);
    }

    public void LoadUtilityRoomScene()
    {
        StartCoroutine(FadeIn());
        Current = ScenesEnum.UTILITY_ROOM_SCENE;
        SceneManager.LoadScene((int) ScenesEnum.UTILITY_ROOM_SCENE);
    }

    public void LoadScienceLabScene()
    {
        StartCoroutine(FadeIn());
        Current = ScenesEnum.SCIENCE_LAB_SCENE;
        SceneManager.LoadScene((int) ScenesEnum.SCIENCE_LAB_SCENE);
    }

    public IEnumerator FadeIn()
    {
        fadeSprite.enabled = true;

        for ( float alpha = 255f; alpha > 0; alpha -= Time.deltaTime * 200f )
        {
            alpha = Mathf.Clamp(alpha, 0, 255);
            fadeSprite.color = new Color(1, 1, 1, alpha / 255);
            yield return null;
        }

        fadeSprite.enabled = false;
    }

    public IEnumerator FadeOut()
    {
        fadeSprite.enabled = true;

        for ( float alpha = 0f; alpha < 255; alpha += Time.deltaTime * 200f )
        {
            alpha = Mathf.Clamp(alpha, 0, 255);
            fadeSprite.color = new Color(0, 0, 0, alpha / 255);
            yield return null;
        }

        SceneManager.LoadScene(3);
    }

    protected override void OnAwake()
    {
        fadeSprite = WhiteSprite.GetComponent<SpriteRenderer>();
    }

    private void GetPlayer()
    {
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] gameObjects = scene.GetRootGameObjects();
        GameObject playerObj;

        for ( int i = 0; i < gameObjects.Length; i++ )
        {
            Debug.Log(gameObjects[i].name);
            if ( gameObjects[i].name == "Player" )
            {
                playerObj = gameObjects[i];
                GameManager.Instance.Player = playerObj.GetComponent<Player>();
            }
        }
    }
}
