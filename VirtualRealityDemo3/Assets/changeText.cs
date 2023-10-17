using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeText : MonoBehaviour
{
    public string textHolder;
    public TMP_Text input;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StoreName()
    {
        Debug.Log(input.text);
        textHolder = input.text;
        text.text = textHolder;
    }
}
