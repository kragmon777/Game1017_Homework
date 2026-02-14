using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState !=GameState.InGame) return;

        float distancePerFrame = speed * Time.deltaTime;
        transform.Translate(distancePerFrame, 0f, 0f);
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
    }
}
