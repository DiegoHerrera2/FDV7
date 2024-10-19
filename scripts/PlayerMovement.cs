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
    private State _state = State.IdleDown;
    
    private enum State
    {
        IdleUp,
        IdleDown,
        IdleSide,
        WalkUp,
        WalkDown,
        WalkSide
    }

    // Update is called once per frame
    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        
        var velocity = new Vector3(x, y, 0).normalized * (speed * Time.deltaTime);
        transform.Translate(velocity);

        switch (_state)
        {
            case State.IdleUp:
            case State.IdleDown:
            case State.IdleSide:
                animator.SetBool("isWalking", false);
                if (x != 0)
                {
                    
                    _state = State.WalkSide;
                }
                else if (y > 0)
                {
                    _state = State.WalkUp;
                }
                else if (y < 0)
                {
                    _state = State.WalkDown;
                }
                break;    
            case State.WalkUp:
                animator.SetBool("isWalking", true);
                animator.SetInteger("moveY", 1);
                if (y == 0)
                {
                    _state = State.IdleUp;
                }
                else if (y < 0)
                {
                    _state = State.WalkDown;
                }
                break;
            case State.WalkDown:
                animator.SetBool("isWalking", true);
                animator.SetInteger("moveY", -1);
                if (y == 0)
                {
                    _state = State.IdleDown;
                }
                else if (y > 0)
                {
                    _state = State.WalkUp;
                }
                break;
            case State.WalkSide:
                spriteRenderer.flipX = _lastMoveX > 0;
                animator.SetBool("isWalking", true);
                animator.SetInteger("moveY", 0);
                if (x == 0)
                {
                    _state = State.IdleSide;
                }
                else if (y != 0)
                {
                    _state = y > 0 ? State.WalkDown : State.WalkUp;
                }
                _lastMoveX = x > 0 ? 1 : -1;
                break;
            default:
                throw new ArgumentOutOfRangeException();
            
            
        }
        Debug.Log(_state);
        
    }
    
}
