using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    //Health property
    public int Health { get; set; }

    //Player movement
    private float _moveSpeed = 3.5f;
    private float _jumpForce = 7.0f;
    private bool _resetJump = false;
    private bool _grounded = false;

    //Collectibles
    public int diamonds;

    //Handles
    private Rigidbody2D _rigid;
    private PlayerAnimation _playerAnimation;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();

        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) 
            return;

        Movement();
        Attack();
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnimation.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _moveSpeed, _rigid.velocity.y);
        Attack();
        _playerAnimation.Move(move);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1<<8);
        //Debug.DrawRay(transform.position, Vector2.down, Color.red);
        if(hitInfo.collider != null)
        {
            if(_resetJump == false)
            {
                _playerAnimation.Jump(false);
                return true;
            }
        }

        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _playerAnimation.Attack();
        }
    }

    public void Damage()
    {
        if(!isDead)
        {
            Health--;
            UIManager.Instance.UpdateLives(Health);

            if (Health < 1)
            {
                isDead = true;
                _playerAnimation.Dead();
            }
        }       
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}
