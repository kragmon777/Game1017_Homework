using UnityEngine;
using System.Collections.Generic;

public class SegmentSpawner : MonoBehaviour
{
   [SerializeField] private GameObject segmentPrefab, segmentPrefab2;
   [SerializeField] private float maxDistanceFromPlayer;
   [SerializeField] private int segmentsListSize = 5;

    private Renderer lastRenderer, currentRenderer;

    private GameObject lastGameObject, currentGameObject;
    private List<GameObject> segments = new();
    private float gapSize = 0.5f;
    private GameObject player;

    public void Initialize()
    {   
        player = GameManager.Instance.player.gameObject;

        // Segment 1
        lastGameObject = Instantiate(segmentPrefab, new Vector3(player.transform.position.x, player.transform.position.y - 1, 0), Quaternion.identity, transform);
        lastRenderer = lastGameObject.GetComponent<Renderer>();
        segments.Add(lastGameObject);

        // Segment 2
        currentGameObject = Instantiate(segmentPrefab,transform);
        currentRenderer = currentGameObject.GetComponent<Renderer>();
        segments.Add(currentGameObject);

        float xSpawnPosition = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;
        currentGameObject.transform.position = new Vector3(xSpawnPosition, player.transform.position.y - 1, 0);
        
        lastGameObject = currentGameObject;
        lastRenderer = currentRenderer;
        
    }

    private void Update()
    {
        if (lastRenderer == null || player == null) return;
        if (lastRenderer.bounds.max.x < player.transform.position.x + maxDistanceFromPlayer)
        {
            gapSize = Random.Range(0.5f, 1.5f);
            float heightOffset = Random.Range(-1.5f, 1.5f);

            currentGameObject = Instantiate(segmentPrefab2,transform);
            currentRenderer = currentGameObject.GetComponent<Renderer>();

            float xSpawnPosition = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;
            currentGameObject.transform.position = new Vector3(xSpawnPosition,lastGameObject.transform.position.y + heightOffset, 0);
            segments.Add(currentGameObject);

            if (segments.Count > segmentsListSize)
            {
                Destroy(segments[0]);
                segments.RemoveAt(0);
            }

            lastGameObject = currentGameObject;
            lastRenderer = currentRenderer;
        }
    }

    public void Reset()
    {
        lastRenderer = null;
        currentRenderer = null;

        lastGameObject = null;
        currentGameObject = null;

        foreach (GameObject gameObject in segments)
        {
            Destroy(gameObject);
        }
        segments.Clear();
    }
}
