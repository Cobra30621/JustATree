using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootHeadController : MonoBehaviour
{
    [Tooltip("Grow Speed in meter/s")]
    public float growSpeed = 5;
    [Tooltip("Max Distance Between")]
    public float refDist = 10;
    public float diameter = 1;
    public bool snakeMode = false;
    public List<GameObject> rootList;
    private Vector3 mousePosition;
    public bool isMoving = false;

    public float spawnTimeCooling = 0.05f;
    float spawnTimer = 0;
    // snake mode spawner
    // public float circleRadius = 0.5f;
    private Vector3 initPos;
    [SerializeField]
    GameObject newRootSegment;
    GameObject newBody;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (snakeMode)
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
            else
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (spawnTimer<=0)
                {
                    newBody = Instantiate(newRootSegment);
                    newBody.transform.position = transform.position;
                    newBody.transform.rotation = transform.rotation;
                    rootList.Add(newBody);
                    transform.position += (mousePosition - transform.position).normalized * diameter;
                    //transform.rotation

                    // set z to 0
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                    // reset spawn time
                    spawnTimer = Mathf.Clamp(spawnTimeCooling * 1f / ( 0.00001f  + 
                        Mathf.Clamp01((mousePosition - transform.position).magnitude 
                        / refDist)) , spawnTimeCooling ,10f); 
                }
            }
            if (!isMoving)
            {
                AudioManager.Instance.StartPlayLoop(0);
                print("run");
                isMoving = true;
            }
        }
        else
        {
            // if not moving than stop sound 
            if (isMoving)
            {
                AudioManager.Instance.StopPlayLoop();
                isMoving = false;
            }
        }

        

        // spawn
        if (snakeMode)
        {
            if ((rootList[rootList.Count - 1].transform.position - initPos).magnitude > diameter)
            {
                newBody = Instantiate(newRootSegment);
                newBody.transform.position = initPos;
                newBody.transform.rotation = rootList[rootList.Count - 1].transform.rotation;
                //
                rootList.Add(newBody);
            }
        }
        else
        {

        }

        if (spawnTimer>=0)
        {
            spawnTimer -= Time.deltaTime;
        }

    }
}
