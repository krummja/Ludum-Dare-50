using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public Transform MovePoint;
    public LayerMask StopMovementMask;
    public LayerMask MovableMask;
    public LayerMask ButtonMask;
    public SpriteAnimator Animator;

    public float MoveSpeed = 1f;

    public void TryMove(Vector2 target)
    {
        if ( Vector2.Distance(transform.position, MovePoint.position) <= 0.001f )
        {
            if ( Mathf.Abs(target.x) == 1f || Mathf.Abs(target.y) == 1f )
            {
                Vector3 pos = MovePoint.position + new Vector3(target.x, target.y, 0f);
                if ( Physics2D.OverlapCircle(pos, 0.2f, MovableMask) )
                {
                    Collider2D collider = Physics2D.OverlapCircle(pos, 0.2f, MovableMask);
                    Movable movable = collider.gameObject.GetComponent<Movable>();

                    movable.TryMove(target);
                    Animator.SetKickState(target);
                    AudioManager.Instance.PlaySound("Kick");
                }

                else if ( !Physics2D.OverlapCircle(pos, 0.2f, StopMovementMask) )
                {
                    GameManager.Instance.DecrementMoves();
                    Animator.SetDashState(target);
                    MovePoint.position += new Vector3(target.x, target.y, 0f);
                }

                else
                {
                    return;
                }
            }
        }
    }

    private void Awake()
    {
        MovePoint.parent = null;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            MovePoint.position,
            MoveSpeed * Time.fixedDeltaTime
        );

        if ( Vector2.Distance(transform.position, MovePoint.position) < 0.05f )
            Animator.SetIdleState();
    }

    private void OnDrawGizmos()
    {
        Color _color = Gizmos.color;
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(MovePoint.position + new Vector3(0f, 0f, 0f), 0.5f);
        Gizmos.color = _color;
    }
}
