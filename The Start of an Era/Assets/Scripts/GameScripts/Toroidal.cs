using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toroidal : MonoBehaviour
{
	[SerializeField]
	private BoxCollider2D limit;

	private float horizontalMin, horizontalMax, verticalMin, verticalMax;

	// Start is called before the first frame update
	void Start()
    {
		Camera camera = Camera.main;
		float sHeight = camera.orthographicSize;
		float sWidth = camera.aspect * sHeight;

		horizontalMin = -sWidth;
		horizontalMax = sWidth;

		verticalMin = -sHeight;
		verticalMax = sHeight;

		limit.size = new Vector2(horizontalMax*2, verticalMax*2);
	}

    // Update is called once per frame
    void Update()
    {

	}

	private void OnTriggerExit2D(Collider2D col)
	{
		col.transform.position = 
			new Vector3(
				-col.transform.position.x + 
				(col.gameObject.GetComponent<BoxCollider2D>().size.x * 
				Mathf.Sign(col.transform.position.x) * 103), 
				col.transform.position.y, 
				col.transform.position.z);
	}
}
