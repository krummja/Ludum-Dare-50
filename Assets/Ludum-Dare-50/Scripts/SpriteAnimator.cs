using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum AnimationState
{
    IDLE,
    DASH,
    KICK
}


public class SpriteAnimator : MonoBehaviour
{
    public GameObject IdleObject;
    public GameObject DashObject;

    public GameObject DustPrefab;

    public AnimationState State;

    public void SetIdleState()
    {
        IdleObject.SetActive(true);
        DashObject.SetActive(false);
    }

    public void SetDashState(Vector3 direction)
    {
        IdleObject.SetActive(false);
        DashObject.SetActive(true);

        GameObject dust = Instantiate(DustPrefab, transform);
        dust.GetComponent<Animator>().Play(0);

        if ( direction.x == -1f )
        {
            DashObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            dust.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if ( direction.x == 1f )
        {
            DashObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            dust.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Start()
    {
        State = AnimationState.IDLE;
    }
}
