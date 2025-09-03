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
        transform.position = new Vector3(cameraX, cameraY, cameraZ);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cameraX, cameraY, cameraZ);
        if (GameManager.Instance.GetGameState() == GameManager.GameState.Play)
        {
            playerTransform = GameManager.Instance.GetPlayerObject().GetComponent<Transform>();
            if (playerTransform == null)
            {
                return;
            }
            cameraY = playerTransform.position.y;
        }

    }

    void FixedUpdate()
    {

    }
}
