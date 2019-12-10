using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [SerializeField]
    private Transform _oppositePortal;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            print("Colliding with a portal");
            collision.collider.transform.position = new Vector3(
                _oppositePortal.GetComponent<BoxCollider2D>().transform.position.x + 50, 
                collision.collider.transform.position.y, 
                collision.collider.transform.position.z);
        }
    }
}
