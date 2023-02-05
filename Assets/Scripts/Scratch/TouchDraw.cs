using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDraw : MonoBehaviour
{
    Coroutine drawing;

    public GameObject linePrefab;

    // public Camera mainCam;
    public static List<LineRenderer> drawnLineRenderers = new List<LineRenderer>();
    public Scratch scratchScript;
    bool isDrawing;

    // void Update(){
    //     if(Input.GetMouseButtonDown(0)){
    //             StartLine();
    //     }
    //     if(Input.GetMouseButtonUp(0)){
    //         FinishLine();
    //     }
    // }
    public void StartLine()
    {
        if (!isDrawing)
        {
            Debug.Log("StartLine");
            isDrawing = true;
            if (drawing != null)
            {
                StopCoroutine(drawing);
            }

            drawing = StartCoroutine(DrawLine());
        }
    }

    public void FinishLine()
    {
        if (isDrawing)
        {
            Debug.Log("FinishLine");
            isDrawing = false;
            if (drawing != null)
                StopCoroutine(drawing);
        }
    }

    IEnumerator DrawLine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.2f);
        GameObject newGameObject = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        LineRenderer line = newGameObject.GetComponent<LineRenderer>();
        drawnLineRenderers.Add(line);
        line.positionCount = 0;
        Vector3 prePosition = GameManager.Instance.headTransform.position;
        prePosition.z = 0;
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, prePosition);
        yield return null;
        while (true)
        {
            Vector3 position = GameManager.Instance.headTransform.position;
            position.z = 0;
            Vector3 direction = position - prePosition;
            prePosition = position;
            position += direction.normalized * 3f;
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, position);
            scratchScript.AssignScreenAsMask();
            yield return waitForSeconds;
        }
    }
}