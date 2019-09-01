using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 mousePos = Input.mousePosition;
		Vector2 wantedPos = Camera.main.ScreenToWorldPoint(
			new Vector2(mousePos.x, mousePos.y));
		transform.position = wantedPos;
	}
}
