using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorders : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        GameProperties.SetCameraSize(cam.ViewportToWorldPoint
            (new Vector3(1, 1, cam.nearClipPlane)));
    }

    //void OnDrawGizmosSelected()
    //{
    //    Vector3 p = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(p, 0.1F);
    //}
}
