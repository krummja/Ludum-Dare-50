using UnityEngine;
using UnityEngine.SceneManagement;


public class GUIManager : BaseManager<GUIManager>
{
    [HideInInspector]
    public GameObject GUI;

    public void LoadGUI()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        GameObject[] rootObjs = currentScene.GetRootGameObjects();

        foreach ( GameObject obj in rootObjs )
        {
            if ( obj.name == "GUI" )
            {
                this.GUI = obj;
                GUIResources res = this.GUI.GetComponent<GUIResources>();
                Clock clock = res.TimerCounter.GetComponent<Clock>();
                GameManager.Instance.Clock = clock;

                GameManager.Instance.MessageObject = res.ModalBox;
            }
        }
    }

    protected override void OnAwake() {}
}
