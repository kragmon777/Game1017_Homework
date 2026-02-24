using UnityEngine;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private float xBuffer = 3f;

    private Transform lastBackground;
    private Renderer lastRenderer;
    private float backgroundWidth;

    // world X where camera-right must reach to spawn again
    private float nextSpawnAtCamRightX;

    private List<GameObject> backgrounds = new List<GameObject>();
    private int objectPoolSize = 3; 

    private void Start()
    {
        if (!cam) cam = Camera.main;

        // Create object pool
        for (int i = 0; i < objectPoolSize; i++)
        {
            GameObject go = Instantiate(backgroundPrefab, transform);
            ReturnToPool(go);
            backgrounds.Add(go);
        }


        lastBackground = GetNextObject().transform;
        lastRenderer = lastBackground.GetComponent<Renderer>();
        backgroundWidth = lastRenderer.bounds.size.x;
        lastRenderer.sortingOrder = 0;

        // Set first trigger based on the first background's right edge
        UpdateNextSpawnTrigger();
    }

    private GameObject GetNextObject()
    {
        foreach (GameObject go in backgrounds)
        {
            if (!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }
        }
        return null;
    }

    private void ReturnToPool(GameObject background)
    {
        background.SetActive(false);
    }

    private void Update()
    {
        float halfCamWidth = cam.orthographicSize * cam.aspect;
        float camRightEdge = cam.transform.position.x + halfCamWidth;

        if (camRightEdge >= nextSpawnAtCamRightX)
        {
            SpawnNextToRight();
            UpdateNextSpawnTrigger();
        }
    }

    private void SpawnNextToRight()
    {
        Vector3 spawnPos = lastBackground.position;
        spawnPos.x += backgroundWidth;

        //lastBackground = Instantiate(backgroundPrefab, spawnPos, Quaternion.identity, transform).transform;
        
        if (GetNextObject() == null)
        {
            float distance;
            float previousDistance = 0;
            GameObject objectToReturn = null;
            foreach (GameObject go in backgrounds)
            {
                distance = Vector3.Distance(go.transform.position, cam.transform.position);

                if ( distance > previousDistance )
                {
                    previousDistance = distance;
                    objectToReturn = go;
                }
            }
            ReturnToPool(objectToReturn);
        }

        lastBackground = GetNextObject().transform;
        lastBackground.position = spawnPos;
        lastRenderer = lastBackground.GetComponent<Renderer>();
        lastRenderer.sortingOrder = 0;
    }

    private void UpdateNextSpawnTrigger()
    {
        // Spawn again only after camera reaches the (new) last background's right edge
        nextSpawnAtCamRightX = lastRenderer.bounds.max.x - xBuffer;
    }

}