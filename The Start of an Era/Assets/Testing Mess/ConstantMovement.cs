using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = false;

        StartCoroutine(CDelayBeforeMoving());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            rb.AddForce(new Vector2(-150, 0));
            canMove = false;
        }

        if (transform.position.x >= GameProperties.RightLimit)
            transform.position = new Vector3(
                GameProperties.LeftLimit, 
                transform.position.y, 
                transform.position.z);

        else if (transform.position.x <= GameProperties.LeftLimit)
            transform.position = new Vector3(
                GameProperties.RightLimit, 
                transform.position.y, 
                transform.position.z);
    }

    private IEnumerator CDelayBeforeMoving()
    {
        yield return new WaitForSeconds(1);
        canMove = true;
    }
}
