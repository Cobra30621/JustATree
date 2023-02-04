using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 10f;
    public int life = 100;
    public int attackPower = 10;

    private int direction = 1;

    void Update()
    {
        // Check direction
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = 1;
        }

        // Move enemy
        transform.position += new Vector3(speed * Time.deltaTime * direction, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        TreeManager tree = other.GetComponent<TreeManager>();
        if (tree != null)
        {
            tree.Damage(attackPower);
        }
    }
}
