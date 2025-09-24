using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActions;

    InputAction moveInput;

    [SerializeField]
    Boundary verticalBoundary;

    [SerializeField]
    Boundary horizontalBoundary;

    Vector2 direction;

    public Camera camera;

    [SerializeField]
    float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveInput = inputActions.FindAction("move");
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    { 
        Move();
        CheckBoundaries();
    }

    void Move()
    {
        direction = moveInput.ReadValue<Vector2>();
        Vector2 movementAmount = direction * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + movementAmount.x,
                                            transform.position.y + movementAmount.y, transform.position.z);
    }

    void CheckBoundaries()
    {
        float positionX = Mathf.Clamp(transform.position.x, horizontalBoundary.min, horizontalBoundary.max);
        float positionY = Mathf.Clamp(transform.position.y, verticalBoundary.min, verticalBoundary.max);

        transform.position = new Vector3(positionX, positionY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("I got hit");
        }
    }
}
