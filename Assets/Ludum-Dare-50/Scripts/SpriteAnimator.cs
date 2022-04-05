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
    public GameObject KickObject;

    public GameObject DustPrefab;

    public AnimationState State;

    public void SetIdleState()
    {
        State = AnimationState.IDLE;
    }

    public void SetDashState(Vector3 direction)
    {
        State = AnimationState.DASH;

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

    public void SetKickState(Vector3 direction)
    {
        State = AnimationState.KICK;

        if ( direction.x == -1f )
        {
            KickObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if ( direction.x == 1f )
        {
            KickObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Update()
    {
        switch ( State )
        {
            case AnimationState.IDLE:
                IdleObject.SetActive(true);
                DashObject.SetActive(false);
                KickObject.SetActive(false);
                break;
            case AnimationState.DASH:
                IdleObject.SetActive(false);
                DashObject.SetActive(true);
                KickObject.SetActive(false);
                break;
            case AnimationState.KICK:
                IdleObject.SetActive(false);
                DashObject.SetActive(false);
                KickObject.SetActive(true);
                break;
        }
    }
}
