using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public Animator animator;
    public Fight fp;
    private bool canHit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && fp.isAttacking && canHit)
        {
            enemyAIPatrol enemy = other.GetComponent<enemyAIPatrol>();
            enemy.TakeHit(10);
            canHit = false;
            StartCoroutine(ResetCanHit());
        }
    }

    IEnumerator ResetCanHit()
    {
        yield return new WaitForSeconds(1.5f);
        canHit = true;
    }
}
