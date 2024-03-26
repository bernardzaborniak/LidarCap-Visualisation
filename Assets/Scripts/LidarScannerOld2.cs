using UnityEngine;
using Benito.ScriptingFoundations.NaughtyAttributes;
using Benito.ScriptingFoundations.Utilities;
using Unity.Mathematics;

public class LidarScannerOld2 : MonoBehaviour
{
    float horizontalAngle = 120;
    float verticalAngle = 32;

    [SerializeField] int horizontalResolution = 600;
    [SerializeField] int verticalResolution = 160;

    [ReadOnly]
    [SerializeField] float horizontalScanningInterval = 0.2f;
    [ReadOnly]
    [SerializeField] float verticalScanningInterval = 0.2f;

    [SerializeField] float frequency = 10;

    float planeDistance;
    RaycastHit[] hitResults;


    LidarVisualizer visualizer;

    public void SetUpScanner(LidarVisualizer visualizer)
    {
        this.visualizer = visualizer;


    }

    public RaycastHit[] PerformScan()
    {
        planeDistance = (horizontalResolution * 0.5f) / (horizontalAngle * 0.5f);
        hitResults = new RaycastHit[horizontalResolution*verticalResolution];



        float currentX;
        float currentY;
        Vector3 direction;
        Vector3 startPos = transform.position;
        Ray ray;
        RaycastHit hit;

        int counter = 0;

        for (int i = -horizontalResolution / 2; i < horizontalResolution / 2; i++)
        {
            currentX = i;

            for (int j = -verticalResolution / 2; j < verticalResolution / 2; j++)
            {
                Matrix4x4 matrix = Matrix4x4.TRS(transform.forward * planeDistance, Quaternion.LookRotation(transform.forward), Vector3.one);
                direction =  matrix.MultiplyVector(new Vector3(j, i, 1f)).normalized;

                ray = new Ray(startPos, direction);
                Physics.Raycast(ray, out hit);

               
                Debug.Log("direction: " + direction);
                hitResults[counter] = hit;
                counter++;
            }
        }

        //transform.rotation = previousRotation;

        return hitResults;
    }



}
