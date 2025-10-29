using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    Boundary verticalBoundary;

    Vector3 direction;
    BulletManager bulletManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = Vector3.up;
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y > verticalBoundary.max)
        {
            //Destroy(gameObject);
            bulletManager.ReturnBullets(gameObject);
        }
    }
}
