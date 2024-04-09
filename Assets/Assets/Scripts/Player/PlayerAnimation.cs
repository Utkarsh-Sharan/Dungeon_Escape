using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Move(float inputDir)
    {
        if(inputDir < 0)
        {
            _playerSprite.flipX = true;
        }
        else if(inputDir > 0)
        {
            _playerSprite.flipX = false;
        }

        _anim.SetFloat("Move", Mathf.Abs(inputDir));
    }

    public void Jump(bool jumping)
    {
        if (jumping)
            _anim.SetBool("Jumping", true);
        else
            _anim.SetBool("Jumping", false);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }

    public void Dash()
    {
        _anim.SetTrigger("Dash");
    }

    public void Dead()
    {
        _anim.SetTrigger("Death");
    }
}
