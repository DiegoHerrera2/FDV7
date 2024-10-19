using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private int _lastMoveX;
    private State _state = State.Idle;
    
    private enum State
    {
        Idle,
        Walk
    }

    // Update is called once per frame
    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        
        var velocity = new Vector3(x, y, 0).normalized * (speed * Time.deltaTime);
        transform.Translate(velocity);
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", y);
        switch (_state)
        {
            case State.Idle:
                animator.SetBool("isWalking", false);
                if (x != 0 || y != 0)
                {
                    _state = State.Walk;
                }
                break;    
            case State.Walk:
                animator.SetBool("isWalking", true);
                spriteRenderer.flipX = _lastMoveX > 0;
                _lastMoveX = x > 0 ? 1 : -1;
                if (x == 0 && y == 0)
                {
                    _state = State.Idle;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
            
            
        }
        Debug.Log(_state);
        
    }
    
}
