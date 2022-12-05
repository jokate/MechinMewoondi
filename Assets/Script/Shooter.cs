using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public bool selected = false;
    public float floorOffset = 1;
    
    GameObject target;
    public ShooterStat shooterStat;
    public GameObject shootingObj;
    private float speed = 10f, stopDistanceOffset = 1;
    float neardist = 0, currentdist = 0;
    private Rigidbody2D rb2d;
    private Vector3 moveToDest = Vector3.zero;
    private bool assignedFloorOffset = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = shooterStat.spr;
        StartCoroutine(targetShoot());
    }

    private void Update()
    {
        if(GetComponent<Renderer>().isVisible && Input.GetMouseButtonUp(0))
        {
            Vector3 campos = Camera.main.WorldToScreenPoint(transform.position);
            campos.y = Selection.InvertMouseY(campos.y);
            selected = Selection.selection.Contains(campos);
        }

        if (selected)
            GetComponent<Renderer>().material.color = Color.red;
        else
            GetComponent<Renderer>().material.color = Color.white;

        if(selected && Input.GetMouseButtonUp(1)) {
            Vector3 destination = Selection.GetDestination();
            if(destination != Vector3.zero)
            {
                moveToDest = destination;
            }
        }
        UpdateMove();
    }
    private void UpdateMove()
    {
        if(moveToDest != Vector3.zero && transform.position != moveToDest)
        {
            Vector3 direction = (moveToDest - transform.position).normalized;
            rb2d.velocity = direction * speed;
            if(Vector3.Distance(transform.position, moveToDest) < stopDistanceOffset)
            {
                moveToDest = Vector3.zero;
            }

        }
        else
        {
            rb2d.velocity = Vector3.zero;
        }
    }

    IEnumerator targetShoot()
    {
        while(true)
        {
            TargetSet();
            if(target != null)
            {
                Shooting();
            }
            yield return new WaitForSeconds(shooterStat.attatckSec);
        }
    }

    void TargetSet()
    {

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (target == null)
            {
                
                neardist = (gameObject.transform.position - enemy.transform.position).magnitude;
                if(neardist < 6.0f)
                {
                    target = enemy;
                }
                
            }
            else
            {
                currentdist = (gameObject.transform.position - enemy.transform.position).magnitude;
                if (currentdist < neardist)
                {

                    neardist = currentdist;
                    target = enemy;
                }
            }

        }
    }
    
    void Shooting()
    {
        float dmg = shooterStat.damage;
        if ((int)target.GetComponent<Enemy>().enemyStat.size == (int)shooterStat.size)
            dmg *= 1.5f;

        if (shooterStat.size == ShooterStat.Size.Small)
            dmg = dmg * DefenseManager.SmallUpgrade;
        else if (shooterStat.size == ShooterStat.Size.Medium)
            dmg = dmg * DefenseManager.MedUpgrade;
        else
        {
            dmg = dmg * DefenseManager.LargeUpgrade;
        }
        shootingObj.GetComponent<ShootingObject>().Initialize(dmg, shooterStat.shootingspr, target);
        Instantiate(shootingObj, gameObject.transform);
       
        target = null;
    }
}
