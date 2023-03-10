using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootGrowController : MonoBehaviour
{
    public float circleRadius = 0.5f;
    public RootHeadController rootHeadController;
    [SerializeField]
    GameObject newRootSegment;
    [SerializeField]
    Transform spawnParents;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((rootHeadController.rootList[rootHeadController.rootList.Count - 1].transform.position - transform.position).magnitude > 0.5)
        {
            GameObject newBody = Instantiate(newRootSegment);
            if (spawnParents != null)
                newBody.transform.parent = spawnParents;
            newBody.transform.position = transform.position;
            rootHeadController.rootList.Add(newBody);
        }
    }
}
