using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Animator _swordAnimation;
    private SpriteRenderer _swordArcSprite;
    private SpriteRenderer _playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Move(float move)
    {
        if(move < 0)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector2 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if(move > 0)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector2 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }

        _anim.SetFloat("Move", Mathf.Abs(move));
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
        _swordAnimation.SetTrigger("SwordAnimation");
    }
}
