using UnityEngine;
using System.Collections;


public class AnimationSelfDestruct : MonoBehaviour
{
    public float Delay = 0f;

    private void Start()
    {
        transform.parent = null;
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + Delay);
    }
}
