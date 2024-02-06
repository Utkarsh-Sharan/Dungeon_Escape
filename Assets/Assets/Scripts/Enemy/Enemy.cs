using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int gems;
    [SerializeField] protected int speed;

    [SerializeField] protected Transform pointA, pointB;

    protected void Attack()
    {

    }

    public abstract void Update();
}
