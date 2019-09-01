using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toroidal : MonoBehaviour
{
	private BoxCollider2D boxCol;
	private Vector2 baseColSize;
	private float horizontalMin, horizontalMax, verticalMin, verticalMax;

	// Start is called before the first frame update
	void Start()
    {
		boxCol = gameObject.GetComponent<BoxCollider2D>();

		Camera camera = Camera.main;
		float sHeight = camera.orthographicSize;
		float sWidth = camera.aspect * sHeight;

		horizontalMin = -sWidth;
		horizontalMax = sWidth;

		verticalMin = -sHeight;
		verticalMax = sHeight;

		baseColSize = boxCol.size;
	}

    // Update is called once per frame
    void Update()
    {
		// Debug.Log("vertical out");

		if (transform.position.y + baseColSize.y >= verticalMax)
		{
			boxCol.size = new Vector2(boxCol.size.x, baseColSize.y - (transform.position.y + baseColSize.y - verticalMax));
			Debug.Log("vertical out");
		}

		if (transform.position.x > horizontalMax || transform.position.x < horizontalMin)
		{

			Debug.Log("Horiz out");
		}
	}
}
