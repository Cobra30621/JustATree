using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public int maxHP;
    public int nowHP;

    public void SetHP(int hp)
    {
        maxHP = hp;
        nowHP = hp;
        ResetUI();
    }
    public void LossHp(int dmg)
    {
        nowHP -= dmg;
        ResetUI();
    }
    void ResetUI()
    {
        healthBarImage.fillAmount = (float)nowHP / (float)maxHP;
    }
}
