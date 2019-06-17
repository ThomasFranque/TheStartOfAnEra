using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameProperties
{
    private static Vector3 cameraSize;

    // Warning, this will not be correct if the camera is not on position (0,0,0)
    public static float RightLimit  { get; private set; }
    public static float LeftLimit   { get; private set; }


    static GameProperties()
    {
        
    }

    public static void SetCameraSize(Vector3 camS)
    {
        cameraSize =    camS;
        RightLimit =    camS.x;
        LeftLimit =     -camS.x;
    }

}
