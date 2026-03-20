using UnityEngine;
using System.Collections.Generic;

public class SegmentSpawner : MonoBehaviour
{
   [SerializeField] private GameObject[] segmentPrefabs;
   [SerializeField] private float maxDistanceFromPlayer;
   [SerializeField] private int segmentsListSize = 5;

   [Tooltip("Represents the min (x) and max (y) distance that segments can spawn from each other.")]
   [SerializeField] private Vector2 gapRange;

   [Tooltip("Represents the min (x) and max (y) height that segments can spawn from each other.")]
   [SerializeField] private Vector2 heightRange;

    private Renderer lastRenderer, currentRenderer;
    private GameObject lastGameObject, currentGameObject;

    private List<GameObject> segments = new();

    private float gapSize = 0.5f;
    private GameObject player;
    private float ySpawnPosition;
    private int lastIndex;

    public void Initialize()
    {   
        player = GameManager.Instance.Player.gameObject;

        ySpawnPosition = player.transform.position.y;
        // Segment 1
        lastGameObject = Instantiate(segmentPrefabs[0], new Vector3(player.transform.position.x, ySpawnPosition - 1, 0), Quaternion.identity, transform);
        lastRenderer = lastGameObject.GetComponent<Renderer>();
        segments.Add(lastGameObject);

        // Segment 2
        currentGameObject = Instantiate(segmentPrefabs[1],transform);
        currentRenderer = currentGameObject.GetComponent<Renderer>();
        segments.Add(currentGameObject);

        float xSpawnPosition = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;
        currentGameObject.transform.position = new Vector3(xSpawnPosition, ySpawnPosition - 1, 0);
        
        lastGameObject = currentGameObject;
        lastRenderer = currentRenderer;

        lastIndex = 1;
    }

    private void Update()
    {
        if (lastRenderer == null || player == null) return;
        if (lastRenderer.bounds.max.x < player.transform.position.x + maxDistanceFromPlayer)
        {
            float gapSize = Random.Range(gapRange.x,gapRange.y);
            float heightOffset = Random.Range(heightRange.x,heightRange.y);

            List<int> possibleIndices = new();

            if (lastIndex == 2 || lastIndex == 3)
            {
                possibleIndices.Add(0);
                possibleIndices.Add(1);
            }
            else
            {
                for (int i = 0; i < segmentPrefabs.Length; i++)
                {
                    if (lastIndex == i) continue;

                    possibleIndices.Add(i);
                }
            }

            int ind = Random.Range(0, possibleIndices.Count);
            int index = possibleIndices[ind];

            currentGameObject = Instantiate(segmentPrefabs[index],transform);
            currentRenderer = currentGameObject.GetComponent<Renderer>();

            float xSpawnPosition = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;
            currentGameObject.transform.position = new Vector3(xSpawnPosition,ySpawnPosition + heightOffset, 0);
            segments.Add(currentGameObject);

            if (segments.Count > segmentsListSize)
            {
                Destroy(segments[0]);
                segments.RemoveAt(0);
            }

            lastGameObject = currentGameObject;
            lastRenderer = currentRenderer;
            lastIndex = index;

            FindFirstObjectByType<PlayerController>().speed += 1.05f;
        }
    }

    public void Reset()
    {
        lastRenderer = null;
        currentRenderer = null;

        lastGameObject = null;
        currentGameObject = null;
        lastIndex = 0;

        foreach (GameObject gameObject in segments)
        {
            Destroy(gameObject);
        }
        segments.Clear();
    }
}
