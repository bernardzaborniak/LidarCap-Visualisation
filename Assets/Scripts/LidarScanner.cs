using UnityEngine;
using Benito.ScriptingFoundations.NaughtyAttributes;
using Benito.ScriptingFoundations.Utilities;
using Unity.Mathematics;
using Benito.ScriptingFoundations.BDebug;

public class LidarScanner : MonoBehaviour
{
    [Header("Scan")]

    [SerializeField] bool continousScan;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float horizontalAngle = 120;
    float verticalAngle = 32;

    [SerializeField] int resolutionX = 600;
    [SerializeField] int resolutionY = 160;

    RaycastHit[] hitResults = new RaycastHit[0];
    LidarVisualizer visualizer;

    [Header("Visualization")]
    [SerializeField] Gradient gradient;
    [SerializeField] float lineThickness;
    [SerializeField] float pointRadius;
    float planeDistance;

    //public void SetUpScanner(LidarVisualizer visualizer)

    void OnEnable()
    {
        hitResults = new RaycastHit[0];
    }

    public void Update()
    {
        planeDistance = (40 * 0.5f) / (120 * 0.5f);

        VisualizeHits();

        if (continousScan)
        {
            PerformScan();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PerformScan();
            }
        }

    }

    void VisualizeHits()
    {
        //Debug.Log("hitResults.Length: " + hitResults.Length);
        for (int i = 0; i < hitResults.Length; i++)
        {
            //Debug.Log("hitResults tick a");

            if (hitResults[i].collider == null)
                continue;

            //Debug.Log("hitResults tick b");
            //Color color = Color.Lerp(Color.red,Color.blue,hitResults[i].distance/30);
            Color color = gradient.Evaluate(hitResults[i].distance / 30);
            BDebug.DrawSphere(hitResults[i].point, Quaternion.identity, Vector3.one * pointRadius, color);
            BDebug.DrawLine(transform.position, hitResults[i].point, color, 0, lineThickness);
        }
    }

    // only accept raycasts with humans
    public RaycastHit[] PerformScan()
    {
        int humanHitCounter = 0;
        float humanHitDistance = 0;

        //Debug.Log("perform scan");
        hitResults = new RaycastHit[resolutionX * resolutionY];

        Vector3 rayDirection;
        Vector3 startPos = transform.position;
        RaycastHit hit;
        //float planeDistance = (resolutionX * 0.5f) / (horizontalAngle * 0.5f);
        //planeDistance *= resolutionX/60;
        //float planeDistance = (resolutionX * 0.5f) / Mathf.Tan(horizontalAngle * 0.5f);
        //float planeDistance = (1 * 0.5f) / Mathf.Tan(horizontalAngle * 0.5f);

        float aspectRatio = 1f * resolutionY / resolutionX;

        for (int x = 0; x < resolutionX; x++)
        {
            for (int y = 0; y < resolutionY; y++)
            {
                float normalizedX = ((x + 0.5f) / resolutionX) - 0.5f;
                float normalizedY = (((y + 0.5f) / resolutionY) - 0.5f) * aspectRatio;
                //rayDirection = transform.forward  + transform.right * normalizedX + transform.up * normalizedY;
                rayDirection = transform.forward * planeDistance + transform.right * normalizedX + transform.up * normalizedY;

                //Physics.Raycast(startPos, rayDirection, out hit, 50f, layerMask);
                if (Physics.Raycast(startPos, rayDirection, out hit, 50f, layerMask))
                {
                    //Debug.Log("raycast ht");
                    humanHitCounter++;
                    humanHitDistance = hit.distance;
                }
                hitResults[(x * resolutionY) + y] = hit;
            }
        }

        if (!continousScan)
        {
            Debug.Log("Human Hits: " + humanHitCounter + " at distance " + humanHitDistance);
        }

        return hitResults;
    }



}
