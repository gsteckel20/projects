using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VelUtils.VRInteraction;

public class DisappearwhenGrab : MonoBehaviour
{
    [SerializeField]
    VRMoveable staff;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    { 
        staff.Grabbed += () =>
        {
            temp = transform.localScale;
            temp.y = 1;
            transform.localScale = temp;
        };
        staff.Released += () =>
        {
            temp = transform.localScale;
            temp.y = .1f;
            transform.localScale = temp;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
