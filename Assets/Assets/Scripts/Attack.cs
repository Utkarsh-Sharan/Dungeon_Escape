using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canAttack = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);

        IDamageable hit = other.GetComponent<IDamageable>();
        if(hit != null)
        {
            if (_canAttack)
            {
                hit.Damage();
                _canAttack = false;
                StartCoroutine(AttackRoutine());
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
    }
}