using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpToTransform : MonoBehaviour {

    //Variables to help camera target
    public Transform camTarget;
    public float trackingSpeed;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    //Used instead of update as its better with a rigigbody
    void FixedUpdate()
    {
        //checks if a valid transform component was assigned
        if(camTarget != null)
        {
            //This is called to slow track the target
            var newPos = Vector2.Lerp(transform.position,
                                       camTarget.position,
                                       Time.deltaTime * trackingSpeed);
            var camPosition = new Vector3(newPos.x, newPos.y, -10f);
            var v3 = camPosition;
            var clampX = Mathf.Clamp(v3.x, minX, maxX);
            var clampY = Mathf.Clamp(v3.y, minY, maxY);
            transform.position = new Vector3(clampX, clampY, -10f);
        }
    }
}
