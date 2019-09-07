using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCam : MonoBehaviour
{
	[SerializeField]
	private bool leftCam, rightCam;

    // Start is called before the first frame update
    void Start()
    {
		Camera camera = Camera.main;
		float sHeight = camera.orthographicSize;
		float sWidth = camera.aspect * sHeight;

		if (leftCam)
			transform.position = 
				new Vector3(
					sWidth * 2, 
					transform.position.y, 
					transform.position.z);
		else if (rightCam)
			transform.position =
				new Vector3(
					-sWidth * 2,
					transform.position.y,
					transform.position.z);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
