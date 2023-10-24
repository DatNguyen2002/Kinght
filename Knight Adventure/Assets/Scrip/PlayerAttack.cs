using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] public Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    public float attackRange = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("chuot trai duoc nhan xuong");
            Attack();
        }
    }

    void Attack()
    {
        //Player attack animation
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit:"+ enemy.name);

        }
    }
    void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
