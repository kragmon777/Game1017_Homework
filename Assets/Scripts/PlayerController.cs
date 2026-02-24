using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 startPosition;

    public void Initialize()
    {
        startPosition = transform.position;
        // Turn on gravity
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState !=GameState.InGame) return;

        float distancePerFrame = speed * Time.deltaTime;
        transform.Translate(distancePerFrame, 0f, 0f);
    }

    public void Reset()
    {
        transform.position = startPosition;
    }
}
