using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    int maxHealthPoint = 1000;
    int nowHealthPoint = 1000;
    public Image healthBarFill;

    public void Damage(int enemyAtk)
    {
        nowHealthPoint -= enemyAtk;
        ResetUI();
    }
    public void ResetUI()
    {
        healthBarFill.fillAmount = nowHealthPoint / maxHealthPoint;
    }

}
