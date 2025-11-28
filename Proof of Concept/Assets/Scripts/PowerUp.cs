using UnityEngine;
using System.Collections;
public class PowerUp : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    Boundary verticalScreenBoundary;

    [SerializeField]
    Boundary horizontalScreenBoundary;

    [SerializeField]
    float healthTimer = 15;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip audioClip;

    bool IsDying = false;

    GameController gameController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < verticalScreenBoundary.min)
        {
            StartCoroutine(HealthRoutine());
        }
    }

    private void FixedUpdate()
    {
        if (IsDying)
        {
            transform.Rotate(0, 0, 5);
            transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x - 0.05f, 0, 1), Mathf.Clamp(transform.localScale.y - 0.05f, 0, 1), 1);

        }
    }

    IEnumerator HealthRoutine()
    {
        yield return new WaitForSeconds(healthTimer);
        Reset();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DestroyingSequence();
            StartCoroutine(HealthRoutine());

            if (audioSource != null && audioClip != null)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }

    public void DestroyingSequence()
    {
        GetComponent<Collider2D>().enabled = false;
        IsDying = true;

    }
    private void Reset()
    {
        transform.position = new Vector3(Random.Range(horizontalScreenBoundary.min, horizontalScreenBoundary.max),
                                                          verticalScreenBoundary.max, transform.position.z);

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        IsDying = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }
}
