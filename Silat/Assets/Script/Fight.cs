using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public GameObject LeftArm,RightArm;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public Animator playerAnim;
    public bool isAttacking = false;
    BoxCollider boxCollider;
    GameObject enemy;
    Transform playerTransform;
    public bool LeftArmUnlocked = false;
    public bool RightArmUnlocked = false;
    public bool DefenceUnlocked = false;
    public bool KickUnlocked = false;

    public bool isBlocking = false;

    void Start()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        enemy = GameObject.FindWithTag("Enemy");
        playerTransform = transform;
        LeftArmUnlocked = PlayerPrefs.GetInt("LeftArmUnlocked", 0) == 1;
        RightArmUnlocked = PlayerPrefs.GetInt("RightArmUnlocked", 0) == 1;
        DefenceUnlocked = PlayerPrefs.GetInt("DefenceUnlocked", 0) == 1;
        KickUnlocked = PlayerPrefs.GetInt("KickUnlocked", 0) == 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                LeftArmAttack();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (CanAttack)
            {
                RightArmAttack();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CanAttack)
            {
                BlockOn();
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {

            BlockOff();

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (CanAttack)
            {
                KickAttack();
            }
        }
    }

    public void LeftArmAttack()
    {
        if (LeftArmUnlocked)
        {
            isAttacking = true;
            CanAttack = false;
            playerAnim.SetTrigger("hit");
            StartCoroutine(ResetAttackCooldown());
        }
        
    }

    public void RightArmAttack()
    {
        if (RightArmUnlocked)
        {
            isAttacking = true;
            CanAttack = false;
            playerAnim.SetTrigger("hit1");
            StartCoroutine(ResetAttackCooldown());
        }
    }

    public void KickAttack()
    {
        if (KickUnlocked)
        {
            isAttacking = true;
            CanAttack = false;
            playerAnim.SetTrigger("kick");
            StartCoroutine(ResetAttackCooldown());
        }
    }

    public void BlockOn()
    {
        if (DefenceUnlocked)
        {
            boxCollider.enabled = false;
            isBlocking = true;
            isAttacking = true;
            CanAttack = false;
            playerAnim.SetTrigger("block");
        }

    }
    public void BlockOff()
    {
        boxCollider.enabled = true;
        isAttacking = false;
        isBlocking = false;
        CanAttack = true;
        playerAnim.SetTrigger("idle");
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        playerAnim.SetTrigger("idle");
        CanAttack = true;
        isAttacking = false;
        isBlocking = false;
        boxCollider.enabled = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
        isBlocking = false;
        boxCollider.enabled = true;
   
    }

    public void LeftArmUnlockedByNPC()
    {
        LeftArmUnlocked = true;
        PlayerPrefs.SetInt("LeftArmUnlocked", LeftArmUnlocked ? 1 : 0);
    }

    public void RightArmUnlockedByNPC()
    {
        RightArmUnlocked = true;
        PlayerPrefs.SetInt("RightArmUnlocked", LeftArmUnlocked ? 1 : 0);
    }

    public void DefenceUnlockedByNPC()
    {
        DefenceUnlocked = true;
        PlayerPrefs.SetInt("DefenceUnlocked", DefenceUnlocked ? 1 : 0);
    }

    public void KickUnlockedByNPC()
    {
        KickUnlocked = true;
        PlayerPrefs.SetInt("KickUnlocked", KickUnlocked ? 1 : 0);
    }
    
}
