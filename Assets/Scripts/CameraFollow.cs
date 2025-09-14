using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform playerTransform;

    float cameraX = 0f;
    float cameraY = 0f;
    float cameraZ = -10f;
    float cameraMinY = 0f;
    [SerializeField] private float cameraSpeed = 1f;
    Rigidbody2D rb;
    public static CameraFollow Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(cameraX, cameraY, cameraZ);
        TryGetComponent<Rigidbody2D>(out rb);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.GetGameState() == GameManager.GameState.Play)
        {
            MoveCamera();

        }

    }

    //oyun sonunda doodlejump gibi olabilir.
    void FixedUpdate()
    {

    }
    public void MoveCameraToInitialPosition()
    {
        transform.position = new Vector3(0f, 0f, -10f);
    }
    public Vector3 GetCameraPosition()
    {
        return transform.position;
    }
    void MoveCamera()
    {
        playerTransform = GameManager.Instance.GetPlayerObject().GetComponent<Transform>();
        if (playerTransform == null)
        {
            return;
        }
        //move camera to next posit. if player pos > next pos => go to player position
        cameraY = playerTransform.position.y;
        if (cameraY > cameraMinY)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(cameraX, cameraY, cameraZ), 0.2f);
            transform.position = new Vector3(cameraX, cameraY, cameraZ);

        }
        Vector2 v = rb.linearVelocity;
        v.y = cameraSpeed;
        rb.linearVelocity = v;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        cameraMinY = transform.position.y;
    }
}
