using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    int maxHealthPoint = 1000;
    int nowHealthPoint = 1000;
    [SerializeField]
    HealthBar healthBar = null;
    private void Start()
    {
        healthBar.SetHP(1000);
    }
    public void Damage(int enemyAtk)
    {
        nowHealthPoint -= enemyAtk;
        healthBar.LossHp(enemyAtk);
    }

}
