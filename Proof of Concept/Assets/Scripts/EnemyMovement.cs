using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    Boundary verticalScreenBoundary;

    [SerializeField]
    Boundary horizontalScreenBoundary;

    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < verticalScreenBoundary.min)
        {
            Reset();

        }
    }

    private void Reset()
    {
        transform.position = new Vector3(Random.Range(horizontalScreenBoundary.min, horizontalScreenBoundary.max),
                                                          verticalScreenBoundary.max, transform.position.z);

    }
}
