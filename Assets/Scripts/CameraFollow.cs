using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform playerTransform;

    float cameraX = 0f;
    float cameraY = 0f;
    float cameraZ = -10f;
    float cameraSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance.IsGameOn())
        {
            playerTransform = GameManager.Instance.GetPlayerObject().GetComponent<Transform>();
        }

        transform.position = new Vector3(cameraX, cameraY, cameraZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOn())
        {
            cameraY = playerTransform.position.y;
        }
        transform.position = new Vector3(cameraX, cameraY, cameraZ);
    }

    void FixedUpdate()
    {

    }
}
