using UnityEngine;

public class NormalMelee : MonoBehaviour
{
    //Normal att. variables
    protected Player player;
    private Vector2 meleeRange;

    protected void Start()
    {
        player = GetComponent<Player>();
        meleeRange = new Vector2(20.0f, 12.0f);
    }

    public void FindTargets()
    {
        Debug.Log("Trying to find enemies...");
        Collider2D[] inRange = Physics2D.OverlapBoxAll(
            transform.position, meleeRange, 0.0f, LayerMask.GetMask("Enemy"));
        Debug.Log(inRange);

        DealDamage(inRange);
    }

    protected void DealDamage(Collider2D[] enemies)
    {
        foreach (Collider2D enemyColl in enemies)
        {
            Debug.Log("Found enemy and dealing damage");

            enemyColl.GetComponent<Entity>().Hit(player.actualDmg);

            Vector3 hitDirection =
                (enemyColl.transform.position -
                transform.position).normalized;

            hitDirection.y = 1.25f;
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawCube(transform.position, meleeRange);
    }
}
