using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCalculation : MonoBehaviour
{
    public GameObject panel;
    public Text totalMutant;
    public Text totalKill;
    public Text totalDamage;
    public Text totalHitReceived;
    public Text totalDistance;

    public static EndCalculation instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endCalculation()
    {
        panel.SetActive(true);
        totalMutant.text = "";
        totalKill.text = "";
        totalDamage.text = "";
        totalHitReceived.text = "";
        totalDistance.text = FindObjectOfType<RootHeadController>().totalDist.ToString();
    }

    public void onTryAgain()
    {
        SceneManager.LoadScene(0);
    }
}
