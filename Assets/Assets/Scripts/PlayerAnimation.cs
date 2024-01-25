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

    public void Move(float move)
    {
        if(move < 0)        
            _playerSprite.flipX = true;        
        else if(move > 0)
            _playerSprite.flipX = false;

        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        if (jumping)
            _anim.SetBool("Jumping", true);
        else
            _anim.SetBool("Jumping", false);
    }
}
