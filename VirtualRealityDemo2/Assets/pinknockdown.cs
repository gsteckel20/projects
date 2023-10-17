using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pinknockdown : MonoBehaviour
{
    [SerializeField]
    GameObject[] pins;
    [SerializeField]
    GameObject ball;
    int knockedDownCount = 0;
    [SerializeField]
    TMP_Text infotext;
    [SerializeField]
    Transform BallSpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        spawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        int tempCount = 0;
        for(int i = 0; i < pins.Length; i++)
        {
            Vector3 pinUp = pins[i].transform.up;
            float upAngle = Vector3.Angle(pinUp, Vector3.up);
            if(upAngle > 5)
            {
                tempCount++;
            }
        }
        knockedDownCount = tempCount;
        infotext.text = "You knocked down " + knockedDownCount + " pins!";
    }

    public void spawnBall()
    {
        GameObject.Instantiate<GameObject>(ball, BallSpawnPos.position, Quaternion.identity);
    }
}
