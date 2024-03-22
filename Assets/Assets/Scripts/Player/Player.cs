using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    //Health property
    public int Health { get; set; }

    //Player movement
    private float _moveSpeed = 3.5f;
    private float _jumpForce = 7.0f;
    private bool _resetJump = false;
    private bool _grounded = false;

    //Player dash
    private bool _canDash = true;
    private bool _isDashing;
    private float _dashPower = 24f;
    private float _dashingTime = 0.1f;
    private float _dashingCooldown = 1f;

    //Collectibles
    public int diamonds;

    //Handles
    private PlayerInputActions _playerInputActions;
    private Rigidbody2D _rigid;
    private PlayerAnimation _playerAnimation;

    public bool isDead;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Jump.performed += Jump;
        _playerInputActions.Player.Attack.performed += Attack;
        _playerInputActions.Player.Dash.performed += Dash;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();

        Health = 4;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(isDead || _isDashing)
            return;

        Vector2 inputDirection = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        _grounded = IsGrounded();

        _rigid.velocity = new Vector2(inputDirection.x * _moveSpeed, _rigid.velocity.y);
        _playerAnimation.Move(inputDirection.x);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(_isDashing) 
            return;

        if (!isDead && context.performed && IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnimation.Jump(true);
        }
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

    private void Attack(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            _playerAnimation.Attack();
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && _canDash)
        {
            _canDash = false;
            _isDashing = true;
            float originalGravity = _rigid.gravityScale;
            _rigid.gravityScale = 0;

            Vector2 inputDirection = _playerInputActions.Player.Movement.ReadValue<Vector2>();
            _rigid.velocity = new Vector2(inputDirection.x * _dashPower, 0f);

            StartCoroutine(DashExecution(originalGravity));
        }      
    }

    private IEnumerator DashExecution(float originalGravity)
    {
        yield return new WaitForSeconds(_dashingTime);
        _rigid.gravityScale = originalGravity;
        _isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
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
