using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootHeadController : MonoBehaviour
{
    [Tooltip("Grow Speed in meter/s")]
    public float growSpeed = 5;

    public List<GameObject> rootList;
    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rootList.Count - 1; i++)
        {
            rootList[i+1].transform.position = rootList[i].transform.position;
            rootList[i+1].transform.rotation = rootList[i].transform.rotation;
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position += (mousePosition - transform.position).normalized * growSpeed / Time.deltaTime;
        
    }
}
