using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject center;
    float circleR; //반지름
    float deg = 0; //각도
    public float health;
    public EnemyStat enemyStat;


    public void Start()
    {
        center = GameObject.FindGameObjectWithTag("Center");
        circleR = 6.5f;
        GetComponent<SpriteRenderer>().sprite = enemyStat.spr;
        health = enemyStat.health;
    }

    void Update()
    {
        if (health > 0.0f)
        {
            deg += Time.deltaTime * enemyStat.rotationSpeed;
            if (deg < 360)
            {
                var rad = Mathf.Deg2Rad * (deg);
                var x = circleR * Mathf.Sin(rad);
                var y = circleR * Mathf.Cos(rad);
                gameObject.transform.position = center.transform.position + new Vector3(x, y);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, deg * -1); //가운데를 바라보게 각도 조절
            }
            else
            {
                deg = 0;
            }
        } else
        {
            DefenseManager.addMoney(enemyStat.money);
            DefenseManager.UpgradeParts += 0.01f;

            Destroy(this.gameObject);
        }
    }

    public void HealthDecrease(float damage)
    {
        health -= damage;
    }
}
