using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float xSpeed;
    [SerializeField] private float moveMultiplier;
    [SerializeField] private float jumpSpeed;
    Transform myTransform;
    Rigidbody2D myRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveHorizontal(-1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveHorizontal(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }
    void MoveHorizontal(int direction)
    {
        Vector2 moveVector = new Vector2(xSpeed, 0f);
        moveVector *= moveMultiplier;

        myTransform.Translate(moveVector * direction * Time.deltaTime);
    }

    void Jump()
    {
        myRigidbody.AddForceY(jumpSpeed, ForceMode2D.Impulse);
    }
}
