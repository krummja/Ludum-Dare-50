using UnityEngine;


public class CameraMover : MonoBehaviour
{
    private Vector3 position;

    public void Move(Vector3 target)
    {
        position = new Vector3(target.x, target.y, -10f);
    }

    private void Update()
    {
        transform.position = position;
    }
}
