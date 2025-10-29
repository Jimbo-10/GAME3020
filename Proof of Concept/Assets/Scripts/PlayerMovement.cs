using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActions;

    InputAction moveInput;

    [SerializeField]
    Boundary verticalBoundary;

    [SerializeField]
    Boundary horizontalBoundary;

    [SerializeField]
    float shootingSpeed;

    Vector2 direction;

    public Camera camera;

    [SerializeField]
    float speed;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip audioClip;


    GameObject bulletPrefab;

    GameController gameController;
    BulletManager bulletManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveInput = inputActions.FindAction("move");
        camera = Camera.main;
        gameController = FindObjectOfType<GameController>();
        bulletManager = FindObjectOfType<BulletManager>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

        StartCoroutine(ShootingRoutine());
    }

    // Update is called once per frame
    void Update()
    { 
        Move();
        CheckBoundaries();

        if (gameController.health == 0)
        {
            gameController.ChangeScene(2);
        }

        if (gameController.score == 100)
        {
            gameController.ChangeScene(3);
        }
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

    IEnumerator ShootingRoutine()
    {
        yield return new WaitForSeconds(shootingSpeed);
        Instantiate(bulletPrefab).transform.position = transform.position;
        bulletManager.GetBullets().transform.position = transform.position;
        StartCoroutine(ShootingRoutine());

        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Debug.Log("I got hit");
            gameController.HealthChange(5);

            collision.GetComponent<AsteroidBehaviour>().DestroyingSequence();
        }


        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("I got hit");
            gameController.HealthChange(5);

            collision.GetComponent<EnemyMovement>().DestroyingSequence();
        }

    }
}
