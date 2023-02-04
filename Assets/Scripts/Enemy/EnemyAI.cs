using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 10f;
    public int life = 100;
    public int attackPower = 10;

    public HealthBar hpBar = null;

    private int direction = 1;

    private void Start()
    {
        hpBar.SetHP(life);
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

    private void OnTriggerEnter(Collider other)
    {
        TreeManager tree = other.GetComponent<TreeManager>();
        if (tree != null)
        {
            tree.Damage(attackPower);
            Destroy(this); //碰到樹就消失
        }
    }
}
