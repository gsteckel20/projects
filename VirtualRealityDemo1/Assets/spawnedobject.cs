using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class spawnedobject : MonoBehaviour
{
    float lifeTime_s = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime_s -= Time.deltaTime;

        if(lifeTime_s < 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
