using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private float fixedY;
    private float fixedZ;
    private float xOffset;

    private void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;

        xOffset = transform.position.x;

        if (player == null)
        {
            var pc = FindFirstObjectByType<PlayerController>();
            if (pc != null)
            {
                player = pc.gameObject;
            }
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        float newXPosition = player.transform.position.x + xOffset;
        transform.position = new Vector3(newXPosition, fixedY, fixedZ);
    }
}
