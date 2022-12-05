using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObject : MonoBehaviour
{
    public float damage;
    public SpriteRenderer rend;
    public GameObject target;
    private float speed = 15f;
    private Rigidbody2D rb2d;
    private Vector2 movement;

    // Start is called before the first frame update
    public void Initialize(float damage, Sprite spr, GameObject target)
    {
        this.damage = damage;
        this.rend.sprite = spr;
        this.target = target;
    }

    void Start()
    {
        StartCoroutine(TimeTick());
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            movement = (target.transform.position - this.gameObject.transform.position).normalized;
        else
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            rb2d.velocity = movement * speed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target != null) { 
            if (collision.CompareTag("Enemy") && collision.gameObject.name == target.name) {
                collision.gameObject.GetComponent<Enemy>().HealthDecrease(damage);
                StopAllCoroutines();
                Destroy(gameObject);
            }
           
        }
        if (collision.CompareTag("Block"))
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator TimeTick() {
        float time = 3f;
        while(time > 0.0f)
        {
            time -= Time.deltaTime;
            yield return null;
        
        }
        Destroy(this.gameObject);
    }
}
