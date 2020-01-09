using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [SerializeField]
    private Transform _oppositePortal = default;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (_oppositePortal.gameObject.name == "LeftPortal")
            {
                print("Colliding with a portal");
                collision.collider.transform.position = new Vector3(
                    _oppositePortal.GetComponent<BoxCollider2D>().transform.position.x + 50,
                    collision.collider.transform.position.y,
                    collision.collider.transform.position.z);
            }
            if (_oppositePortal.gameObject.name == "RightPortal")
            {
                print("Colliding with a portal");
                collision.collider.transform.position = new Vector3(
                    _oppositePortal.GetComponent<BoxCollider2D>().transform.position.x - 50,
                    collision.collider.transform.position.y,
                    collision.collider.transform.position.z);
            }
        }
    }
}
