using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Benito.ScriptingFoundations.BDebug;

public class LidarVisualizer : MonoBehaviour
{
    [SerializeField] LidarScanner[] scanners;


    List<RaycastHit[]> hitResults = new List<RaycastHit[]>();
    void Start()
    {
        for (int i = 0; i < scanners.Length; i++)
        {
           // scanners[i].SetUpScanner(this);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hitResults.Clear();
            for (int i = 0; i < scanners.Length; i++)
            {
                if (scanners[i].gameObject.activeInHierarchy)
                {
                    hitResults.Add(scanners[i].PerformScan());
                }
            }
        }

        for (int i = 0; i < scanners.Length; i++)
        {
            for (int j = 0; j < hitResults[i].Length; j++)
            {
                if (hitResults[i][j].collider == null)
                    continue;

                //Color color = Color.Lerp(Color.red,Color.blue,hitResults[i].distance/30);
                //Color color = gradient.Evaluate(hitResults[i][j].distance / 30);
                //BDebug.DrawSphere(hitResults[i][j].point, Quaternion.identity, Vector3.one * 0.01f, color);
                //BDebug.DrawLine(scanners[i].transform.position, hitResults[i][j].point, color, 0, 0.025f);
            }

        }

    }


}
