using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomingController : MonoBehaviour
{
    public float zoomingScale = 1;
    public float maxZoomingScale = 5;
    // init scale is camera size
    private float initScale = 5;
    private Vector3 initPos;
    
    public float groundDepth;
    public float groundWidth;
    public Transform playerTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        initScale = GetComponent<Camera>().orthographicSize;
        initPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        float y =  - playerTransform.position.y;
        float scaleY = ((y + 10f)  / groundDepth) * maxZoomingScale;
        float x = Mathf.Abs(playerTransform.position.x);
        float scaleX =  (2 * x  / groundWidth) * maxZoomingScale;
        float tempScale =  Mathf.Max(scaleY, scaleX);
        
        if (tempScale > zoomingScale)
        {
            zoomingScale = tempScale;
        }
        setCameraZoomingScale(zoomingScale);
    }

    public void setCameraZoomingScale(float scale) {
        GetComponent<Camera>().orthographicSize = initScale*Mathf.Clamp(scale,1,maxZoomingScale);
        transform.position = new Vector3(initPos.x,(initPos.y - initScale * (Mathf.Clamp(scale, 1, maxZoomingScale) - 1)),-10);
    }
}
