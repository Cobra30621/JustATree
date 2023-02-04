using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void onStartClick()
    {
        SceneManager.LoadScene(1);
    }

    public void onCreditClick()
    {
        SceneManager.LoadScene(2);
    }

    public void onExitClick()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
