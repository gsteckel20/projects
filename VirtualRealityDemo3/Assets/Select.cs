using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    [SerializeField]
    Camera selectCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            Debug.Log(mousePosition);
            Ray r = selectCamera.ScreenPointToRay(mousePosition);
            Debug.Log(r);
        }
        
    }
}
