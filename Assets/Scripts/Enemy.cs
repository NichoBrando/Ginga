using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 10;
    [SerializeField]
    private Animator animation;
    
    public void receiveDamage(int damage)
    {
        if(health <= 0){
            return;
        }
        health -= damage;
        if(health <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            animation.Play("die");
            Destroy(gameObject, 1f);
        }
    }
}
