using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    //use this for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        if (isDead)
            return;

        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if(Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");

            GameObject diamond = Instantiate(_diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
}
