using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public Transform MovePoint;
    public LayerMask StopMovementMask;
    public LayerMask MovableMask;
    public LayerMask ButtonMask;

    public float MoveSpeed = 1f;

    private Vector2 moveInput;

    private void Awake()
    {
        MovePoint.parent = null;
        moveInput = InputManager.Instance.Inputs.MoveInput;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            MovePoint.position,
            MoveSpeed * Time.fixedDeltaTime
        );

        if ( Vector2.Distance(transform.position, MovePoint.position) <= 0.001f )
        {
            // TODO Play around with tweening and snappiness calibration.
            moveInput = InputManager.Instance.Inputs.MoveInput;
            if ( Mathf.Abs(moveInput.x) == 1f )
            {
                if ( !Physics2D.OverlapCircle(MovePoint.position + new Vector3(moveInput.x, 0f, 0f), 0.2f, StopMovementMask) )
                    MovePoint.position += new Vector3(moveInput.x, 0f, 0f);
            }
            else if ( Mathf.Abs(moveInput.y) == 1f )
            {
                if ( !Physics2D.OverlapCircle(MovePoint.position + new Vector3(0f, moveInput.y, 0f), 0.2f, StopMovementMask) )
                    MovePoint.position += new Vector3(0f, moveInput.y, 0f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Color _color = Gizmos.color;
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(MovePoint.position + new Vector3(0f, 0f, 0f), 0.5f);
        Gizmos.color = _color;
    }
}
