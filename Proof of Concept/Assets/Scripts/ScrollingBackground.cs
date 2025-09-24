using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    Boundary verticalBoundary;

    [SerializeField]
    float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.down * Time.deltaTime;

        if (transform.position.y < verticalBoundary.min)
        {
            transform.position = new Vector3(transform.position.x, verticalBoundary.max, transform.position.z);
        }
    }
}
