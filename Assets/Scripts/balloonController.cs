using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator= GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        pop();
    }
    private void pop()
    {
        animator.SetBool("isPoped", true);
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length) ;
    }
}
