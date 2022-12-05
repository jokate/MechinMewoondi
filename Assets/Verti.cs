using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verti : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        float time = 3.0f;
        while(time >0.0f)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
