using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceTesting : MonoBehaviour
{
    const int numberOfTests = 5000;
    Transform test;


    void PerformGetComponent1()
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            test = GetComponent<Transform>();
        }
    }

    void PerformGetComponent2()
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            test = (Transform) GetComponent("Transform");
        }
    }

    void PerformGetComponent3()
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            test = (Transform) GetComponent(typeof(Transform));
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformGetComponent1();
            PerformGetComponent2();
            PerformGetComponent3();
            
            Debug.Log("LOG");
        }
    }
}
