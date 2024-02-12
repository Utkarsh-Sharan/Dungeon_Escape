using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField] private GameObject _acidBall;

    public int Health { get; set; }

    //use this for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        //do nothing, sit still!
    }

    public void Damage()
    {
        Health--;
        if(Health < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void Attack()
    {
        Instantiate(_acidBall, transform.position, Quaternion.identity);
    }
}
