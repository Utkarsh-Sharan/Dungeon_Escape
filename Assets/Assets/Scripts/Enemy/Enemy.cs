using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int gems;
    [SerializeField] protected float speed;

    [SerializeField] protected Transform pointA, pointB;
    protected Vector3 currentTarget;

    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Transform player;

    protected bool isHit;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
            return;
        Movement();
    }

    public virtual void Movement()
    {
        FlipSprite();

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }

        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.position, player.position);     //if distance > 2, enemies start moving again
        if(distance > 2)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

        Vector3 direction = transform.position - player.position;       //always face towards player when in combat
        if(anim.GetBool("InCombat") == true && direction.x > 0.0f)
        {
            sprite.flipX = true;
        }
        else if(anim.GetBool("InCombat") == true && direction.x < 0.0f)
        {
            sprite.flipX = false;
        }
    }

    protected virtual void FlipSprite()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else if (currentTarget == pointB.position)
        {
            sprite.flipX = false;
        }
    }
}
