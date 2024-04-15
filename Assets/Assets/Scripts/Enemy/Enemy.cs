using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //enemy properties
    [SerializeField] protected int health;
    [SerializeField] protected int gems;
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject _diamondPrefab;

    //navigation
    [SerializeField] protected Transform pointA, pointB;
    protected Vector3 currentTarget;

    //handles
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected Player player;

    //bool checks
    protected bool isHit;
    protected bool isDead;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
            return;

        if(!isDead)
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

        float distance = Vector3.Distance(transform.position, player.transform.position);     //if distance > 2, enemies start moving again
        if(distance > 2)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

        Vector3 direction = transform.position - player.transform.position;       //always face towards player when in combat
        if (anim.GetBool("InCombat") == true && direction.x > 0.0f)
        {          
            spriteRenderer.flipX = true;
        }
        else if(anim.GetBool("InCombat") == true && direction.x < 0.0f)
        {
            spriteRenderer.flipX = false;
        }
    }

    protected virtual void FlipSprite()
    {
        if (currentTarget == pointA.position)
        {
            spriteRenderer.flipX = true;
        }
        else if (currentTarget == pointB.position)
        {
            spriteRenderer.flipX = false;
        }
    }
}
