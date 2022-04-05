using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    private void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        GameObject[] rootObjs = activeScene.GetRootGameObjects();

        foreach ( GameObject obj in rootObjs )
        {
            if ( obj.name == "Player" )
                GameManager.Instance.Player = obj.GetComponent<Player>();
        }

        GUIManager.Instance.LoadGUI();
    }
}
