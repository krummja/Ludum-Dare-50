using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movable : MonoBehaviour
{
    public Transform MovePoint;
    public LayerMask StopMovementMask;

    public float MoveSpeed = 1f;

    private void Awake()
    {
        MovePoint.parent = null;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            MovePoint.position,
            MoveSpeed * Time.fixedDeltaTime
        );
    }

    private void OnDrawGizmos()
    {
        Color _color = Gizmos.color;
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(MovePoint.position + new Vector3(0f, 0f, 0f), 0.5f);
        Gizmos.color = _color;
    }
}
