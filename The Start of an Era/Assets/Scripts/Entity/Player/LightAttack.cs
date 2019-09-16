﻿using UnityEngine;

public class LightAttack : MonoBehaviour
{
    //Normal att. variables
    [SerializeField] private float heavyKnockback;
    protected Player playerScript;
    private Vector2 meleeRange;

    protected void Start()
    {
        playerScript = transform.parent.GetComponent<Player>();
        meleeRange = new Vector2(20.0f, 15.0f);
    }

    public void FindTargets()
    {
        Collider2D[] inRange = Physics2D.OverlapBoxAll(
            transform.position, meleeRange, 0.0f, LayerMask.GetMask("Enemy"));

        DealDamage(inRange);
    }

    protected void DealDamage(Collider2D[] enemies)
    {
        foreach (Collider2D enemyColl in enemies)
        {
            Enemy enemyScript = enemyColl.GetComponent<Enemy>();

            Debug.Log(
                $"Found enemy and dealing {playerScript.ActualDamage} damage");


            Vector3 hitDirection =
                (enemyScript.transform.position -
                transform.position).normalized;

            hitDirection.y = 100f;

            enemyScript.Hit(
                playerScript.ActualDamage, hitDirection, heavyKnockback);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawCube(transform.position, meleeRange);
    }
}
