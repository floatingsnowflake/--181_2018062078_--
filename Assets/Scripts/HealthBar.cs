using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Vector3 offset;
    private Transform PlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.Find("hero").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerTransform.position + offset;
    }
}
