using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 10f;
    public int hp = 100;
    public int attackPower = 10;

    public HealthBar hpBar = null;

    private int direction = 1;

    private void Start()
    {
        hpBar.SetHP(hp);
    }
    void Update()
    {
        // 在右邊時往左走 在左邊時往右走
        if (transform.position.x > 0)
            direction = -1;
        else
            direction = 1;

        // Move enemy
        transform.position += new Vector3(speed * Time.deltaTime * direction, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy觸發 OnTriggerEnter!");
        TreeManager tree = other.GetComponent<TreeManager>();
        if (tree != null)
        {
            AudioManager.Instance.StartPlayOnce(4);
            Debug.Log("Enemy觸發 撞到樹! 造成傷害:" + attackPower);
            tree.TakeDamage(attackPower);
            DestroySelf();
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        GameManager.Instance.enemyList.Remove(this);
        Destroy(this.gameObject); //碰到樹就消失
    }
}
