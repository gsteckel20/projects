using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inputexample : MonoBehaviour
{
    [SerializeField]
    spawnedobject spawnedObjectPrefab;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    spawnedobject spawnedSphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnedobject spawned = GameObject.Instantiate(spawnedObjectPrefab);
            Rigidbody rb = spawned.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(
                Random.Range(-5.0f, 5.0f),
                Random.Range(-5.0f, 5.0f),
                Random.Range(-5.0f, 5.0f));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            spawnedobject spawnSphere = GameObject.Instantiate(spawnedSphere);
            Rigidbody rbs = spawnedSphere.GetComponent<Rigidbody>();
        }
    }
}
