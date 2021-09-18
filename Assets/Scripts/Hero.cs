using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField]
    private Animator animation;
    [SerializeField]
    private SpriteRenderer sprite;
    
    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private List<GameObject> attackAreas;

    private string currentAttack = "";
    private bool canAttack = true;

    void Update()
    {
        Attack();
        Move();
    }

    void Move()
    {
        if (currentAttack.Length != 0) return;
        if (Input.GetKey(KeyCode.D)) {
            body.velocity = new Vector2(5, body.velocity.y);
            animation.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A)) {
            body.velocity = new Vector2(-5, body.velocity.y);
            animation.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0, 180, 0);    
        }
        else {
            body.velocity = new Vector2(0, body.velocity.y);
            animation.SetBool("isRunning", false);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.P) && canAttack) {
            body.velocity = new Vector2(0, body.velocity.y);
            if (currentAttack == "attack_1") {
                currentAttack = "attack_2";
                animation.SetTrigger("attack_2");
                StartCoroutine(Attack2());
            }
            else if(currentAttack == "attack_2"){
                currentAttack = "attack_3";
                animation.SetTrigger("attack_3");
                StartCoroutine(Attack3());
            }
            else{
                currentAttack = "attack_1";
                animation.SetTrigger("attack_1");
                StartCoroutine(Attack1());
            }
        }
    }

    void ApplyDamage(int usedAttack)
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.GetMask("Enemies");
        filter.useLayerMask = true;
        attackAreas[usedAttack].GetComponent<EdgeCollider2D>().OverlapCollider(filter, results);
        foreach(Collider2D item in results) {
            item.gameObject.GetComponent<Enemy>().receiveDamage(3);
        };
    }

    private IEnumerator Attack1()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.25f);
        ApplyDamage(0);
        canAttack = true;
        yield return new WaitForSeconds(0.25f);
        if (currentAttack == "attack_1") {
            currentAttack = "";
        }
    }

    private IEnumerator Attack2()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.25f);
        ApplyDamage(1);
        canAttack = true;
        yield return new WaitForSeconds(0.3f);
        if (currentAttack == "attack_2") {
            currentAttack = "";
        }
    }

    private IEnumerator Attack3()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.8f);
        ApplyDamage(2);
        canAttack = true;
        currentAttack = "";
    }
}
