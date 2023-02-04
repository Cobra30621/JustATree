using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootHeadController : MonoBehaviour
{
    [Tooltip("Grow Speed in meter/s")]
    public float growSpeed = 5;
    public float refDist = 10;
    public float diameter = 1;

    public List<GameObject> rootList;
    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            for (int i = rootList.Count - 1; i > 0; i--)
            {
                rootList[i].transform.position = rootList[i - 1].transform.position + (rootList[i].transform.position - rootList[i - 1].transform.position).normalized * diameter;
                rootList[i].transform.rotation = rootList[i - 1].transform.rotation;
            }

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += (mousePosition - transform.position).normalized
                * Mathf.Clamp01((mousePosition - transform.position).magnitude / refDist)
                * growSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }

    }
}
