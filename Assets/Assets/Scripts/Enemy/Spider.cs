using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        if(isDead) 
            return;

        Health--;
        if(Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");

            GameObject diamond = Instantiate(_diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public void Attack()
    {
        Instantiate(_acidBall, transform.position, Quaternion.identity);
    }
}
