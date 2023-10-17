using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CylinderChanger : MonoBehaviour
{
    [SerializeField]
    GameObject Cylinder;
    Renderer ren;
    [SerializeField]
    Slider slider;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temp = transform.localScale;
        temp.y = slider.value;
        transform.localScale = temp;
    }

    public void ChangeYellow()
    {
        ren = GetComponent<Renderer>();
        ren.material.color = Color.yellow;
    }

    public void ChangeRed()
    {
        ren = GetComponent<Renderer>();
        ren.material.color = Color.red;
    }

    public void ChangeBlue()
    {
        ren = GetComponent<Renderer>();
        ren.material.color = Color.cyan;
    }

    public void ChangePurple()
    {
        ren = GetComponent<Renderer>();
        ren.material.color = Color.blue;
    }

    public void ChangeBlack()
    {
        ren = GetComponent<Renderer>();
        ren.material.color = Color.black;
    }
    public void ChangeGreen()
    {
        ren = GetComponent<Renderer>();
        ren.material.color = Color.green;
    }
    public void ChangeWhite()
    {
        ren = GetComponent<Renderer>();
        ren.material.color = Color.white;
    }
}
