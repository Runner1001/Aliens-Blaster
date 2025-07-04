using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    void Start()
    {
        foreach (var waterFlowAnim in GetComponentsInChildren<WaterFlowAnimation>())
        {
            waterFlowAnim.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<AudioSource>().Play();
    }

    public void SetSpeed(float speed)
    {
        GetComponent<BuoyancyEffector2D>().flowMagnitude = speed;
        foreach (var waterFlowAnim in GetComponentsInChildren<WaterFlowAnimation>())
        {
            waterFlowAnim.enabled = speed != 0;
        }
    }
}
