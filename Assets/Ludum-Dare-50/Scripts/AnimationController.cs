using UnityEngine;


public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private static readonly int Moving = Animator.StringToHash("Moving");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerMoving()
    {
        animator.SetTrigger(Moving);
    }
}
