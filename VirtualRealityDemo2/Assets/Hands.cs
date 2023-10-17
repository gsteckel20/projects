using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VelUtils.VRInteraction;

public class Hands : MonoBehaviour
{
    [SerializeField]
    VRGrabbableHand hand;
    VRGrabbable obj;
    [SerializeField]
    GameObject openHand;
    [SerializeField]
    GameObject closedHand;
    // Start is called before the first frame update
    void Start()
    {
        hand.OnGrab += (obj) =>
        {
            openHand.GetComponent<Renderer>().enabled = false;
            closedHand.GetComponent<Renderer>().enabled = true;
        };
        hand.OnRelease += (obj) =>
        {
            openHand.GetComponent<Renderer>().enabled = true;
            closedHand.GetComponent<Renderer>().enabled = false;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
