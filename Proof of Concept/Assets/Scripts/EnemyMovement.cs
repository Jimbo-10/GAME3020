using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    Boundary speedRange;

    [SerializeField]
    Boundary verticalScreenBoundary;

    [SerializeField]
    Boundary horizontalScreenBoundary;

    GameController gameController;
    BulletManager bulletManager;

    bool IsDying = false;


    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        bulletManager = FindObjectOfType<BulletManager>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (gameController.score >= 50)
        {
            float xPos = Mathf.PingPong(Time.time * speed, horizontalScreenBoundary.max - horizontalScreenBoundary.min) + horizontalScreenBoundary.min;
            transform.position = new Vector3(xPos, transform.position.y - speed * Time.deltaTime);
        }

        if (transform.position.y < verticalScreenBoundary.min)
        {
            Reset();

        }
    }

    private void FixedUpdate()
    {
        if (IsDying)
        {
            transform.Rotate(0, 0, 5);
            transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x - 0.05f, 0, 1), Mathf.Clamp(transform.localScale.y - 0.05f, 0, 1), 1);
            Reset();
        }
    }

    public void DestroyingSequence()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.red;
        IsDying = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            DestroyingSequence();
            bulletManager.ReturnBullets(collision.gameObject);
            gameController.ChangeScore(5);
        }
    }
    private void Reset()
    {
        transform.position = new Vector3(Random.Range(horizontalScreenBoundary.min, horizontalScreenBoundary.max),
                                                          verticalScreenBoundary.max, transform.position.z);

        speed = Random.Range(speedRange.min, speedRange.max);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        IsDying = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }
}
