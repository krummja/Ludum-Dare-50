using UnityEngine;


[CreateAssetMenu(menuName = "LD50/Level", fileName = "Level")]
public class BaseLevel : ScriptableObject
{
    public Transform North;
    public Transform South;
    public Transform East;
    public Transform West;
}

