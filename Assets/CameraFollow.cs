using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    Transform playerTransform;

    float cameraX = 0f;
    float cameraY = 0f;
    float cameraZ = -10f;
    float cameraSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerTransform = player.GetComponent<Transform>();
        transform.position = new Vector3(cameraX, cameraY, cameraZ);
    }

    // Update is called once per frame
    void Update()
    {
        cameraY = playerTransform.position.y;
        transform.position = new Vector3(cameraX, cameraY, cameraZ);
    }

    void FixedUpdate()
    {

    }
}
