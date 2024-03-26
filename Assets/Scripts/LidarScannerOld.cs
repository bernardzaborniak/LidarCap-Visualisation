using UnityEngine;
using Benito.ScriptingFoundations.NaughtyAttributes;
using Benito.ScriptingFoundations.Utilities;
using Unity.Mathematics;

public class LidarScannerOld : MonoBehaviour
{
    [Tooltip("interval in degrees")]
    [SerializeField] float horizontalScanningInterval = 0.2f;
    [Tooltip("interval in degrees")]

    [SerializeField] float verticalScanningInterval = 0.2f;

    [SerializeField] float frequency = 10;

    float horizontalAngle = 120;
    float verticalAngle = 32;
    [ReadOnly]
    [SerializeField] int horizontalResolution;
    [ReadOnly]
    [SerializeField] int verticalResolution;

    LidarVisualizer visualizer;

    public void SetUpScanner(LidarVisualizer visualizer)
    {
        this.visualizer = visualizer;
    }

    public RaycastHit[] PerformScan()
    {
        horizontalResolution = (int)((horizontalAngle / horizontalScanningInterval) + 0.5f);
        verticalResolution = (int)((verticalAngle / verticalScanningInterval) + 0.5f);

        RaycastHit hit;
        Ray ray;
        //Debug.Log($"perform scan started with params {horizontalResolution} and {verticalResolution}");

        Quaternion previousRotation = transform.rotation;

        Quaternion verticalRotation;
        Quaternion horizontalRotation;
        Vector3 startPos = transform.position;
        Vector3 forward = transform.forward;
        Vector3 up = transform.up;
        Vector3 right = transform.right;

        Vector3 currentDirection = transform.forward;
        //currentDirection = VectorUtilities.RotateAlongAxisNormalized(currentDirection,up,-horizontalResolution*0.5f);
        //currentDirection = VectorUtilities.RotateAlongAxisNormalized(currentDirection,right,-horizontalResolution*0.5f);

        float rotX, rotY;

        RaycastHit[] hitResults = new RaycastHit[horizontalResolution * verticalResolution];

        //transform.rotation = QuaternionUtilities.RotateAlongAxis(transform.rotation, transform.up, - (horizontalAngle * 0.5f));
        //transform.rotation = QuaternionUtilities.RotateAlongAxis(transform.rotation, transform.right, - (verticalAngle * 0.5f));

        for (int i = 0; i < horizontalResolution; i++)
        {
            rotY = i * horizontalScanningInterval - (horizontalResolution * 0.5f);

            //transform.rotation = QuaternionUtilities.RotateAlongAxis(transform.rotation, transform.up, horizontalScanningInterval);

            rotY = i * horizontalScanningInterval - (horizontalAngle*0.5f);
            horizontalRotation = QuaternionUtilities.RotateAlongAxis(transform.rotation,up,rotY);

            //currentDirection = horizontalRotation * forward;

            for (int j = 0; j < verticalResolution; j++)
            {
                rotX = j * verticalScanningInterval - (verticalAngle * 0.5f);
                //rotXDelta = verticalScanningInterval
                //rotY = i * horizontalScanningInterval - (horizontalResolution*0.5f);

                /*rotation = 
                QuaternionUtilities.RotateAlongAxis(transform.rotation,up,rotY) 
                * QuaternionUtilities.RotateAlongAxis(transform.rotation,right,rotX);
                */

                //transform.rotation = QuaternionUtilities.RotateAlongAxis(transform.rotation, transform.right, verticalScanningInterval);


                verticalRotation = QuaternionUtilities.RotateAlongAxis(transform.rotation,right,rotX);


                //rotation = Quaternion.Euler(rotX, rotY, 0f);
                Vector3 direction = verticalRotation * transform.forward;
                //direction = horizontalRotation * direction;

                //ray = new Ray(startPos, direction);
                //Debug.Log($"transform rotations x {transform.rotation.eulerAngles.x} y {transform.rotation.eulerAngles.y}");
                ray = new Ray(startPos, transform.forward);
                Physics.Raycast(ray, out hit);
                hitResults[(i * verticalResolution) + j] = hit;
            }
        }

        //transform.rotation = previousRotation;

        return hitResults;
    }



}
