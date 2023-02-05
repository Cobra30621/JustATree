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
    [SerializeField]
    Animator treeAnimator = null;
    /// <summary>������ʵe�W�� </summary>
    public enum TreeAnimationStatus
    {
        Seed = 0,
        Sapling,
        Tree,
        SeedToSapling,
        SaplingToTree
    }
    TreeAnimationStatus nowStatus = TreeAnimationStatus.Seed;

    private void Start()
    {
        ItemManager.Instance.onItemPick += OnItemPick;
        healthBar.SetHP(1000);
    }
    public void TakeDamage(int enemyAtk)
    {
        nowHealthPoint -= enemyAtk;
        healthBar.LossHp(enemyAtk);
        if (nowHealthPoint <= 0)
        {
            //GameOver
            GameManager.Instance.GameOver();
        }
    }

    void OnItemPick(ItemType iType)
    {
        int totalItemCount = ItemManager.Instance.totalItemPicked;
        int getItemTypeCount = ItemManager.Instance.PickedItemCount(iType);

        switch (nowStatus)
        {
            case TreeAnimationStatus.Seed:
                nowStatus = TreeAnimationStatus.Sapling;
                treeAnimator.SetTrigger(TreeAnimationStatus.SeedToSapling.ToString());
                Debug.Log("�𪬺A����: SeedToSapling");
                break;
            case TreeAnimationStatus.Sapling:
                if (totalItemCount >= 3)
                {
                    nowStatus = TreeAnimationStatus.Tree;
                    treeAnimator.SetTrigger(TreeAnimationStatus.SaplingToTree.ToString());
                    Debug.Log("�𪬺A����: SaplingToTree");
                }
                break;
            case TreeAnimationStatus.Tree:
                //�P�_�Ӧ����o�����~�O�_�j��T�� �Ӥ����ʵe���A
                if (getItemTypeCount >= 3)
                {
                    Debug.Log("�𪬺A����: skin " + iType.ToString());
                    if (iType == ItemType.Cola || iType == ItemType.Kirito || iType == ItemType.Trident)
                    {
                        treeAnimator.SetTrigger(iType.ToString());
                    }
                }
                break;
            default:
                break;
        }


    }

}
