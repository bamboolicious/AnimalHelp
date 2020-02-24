using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;
    [SerializeField] private bool facingRight = true;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (playerController == null)
        {
            playerController?.GetComponent<PlayerController>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AnimationControl();
        FlipControl();
    }

    private void FlipControl()
    {

        if (facingRight == true && playerController.rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (facingRight == false && playerController.rb.velocity.x > 0)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void AnimationControl()
    {
        if (Mathf.Abs(playerController.rb.velocity.x) > 2 && playerController.isGrounded)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Jump", false);
        }
        else if (!playerController.isGrounded)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Walk", false);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", false);
            animator.SetBool("Idle", true);
        }
    }
}