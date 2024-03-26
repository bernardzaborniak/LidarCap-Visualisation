using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanarioSwitcher : MonoBehaviour
{
    [SerializeField] GameObject[] scenarioObjects;

    void Start()
    {
        DisableAll();
        scenarioObjects[0].SetActive(true);
    }

    void DisableAll()
    {
        for (int i = 0; i < scenarioObjects.Length; i++)
        {
            scenarioObjects[i].SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisableAll();
            scenarioObjects[0].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisableAll();
            scenarioObjects[1].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DisableAll();
            scenarioObjects[2].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DisableAll();
            scenarioObjects[3].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DisableAll();
            scenarioObjects[4].SetActive(true);
        }

         if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            DisableAll();
            scenarioObjects[5].SetActive(true);
        }

         if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            DisableAll();
            scenarioObjects[6].SetActive(true);
        }

         if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            DisableAll();
            scenarioObjects[7].SetActive(true);
        }
    }
}
