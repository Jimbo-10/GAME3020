using UnityEngine;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    int bulletTotal = 50;

    GameObject bulletPrefab;
    Queue<GameObject> bulletPool = new Queue<GameObject>();
    void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

        for (int i = 0; i < bulletTotal; i++)
        {
            CreateBullet();
        }
    }

    void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bullet.transform.parent = transform;
        bulletPool.Enqueue(bullet);
    }
    public GameObject GetBullets()
    {
        if (bulletPool.Count == 0)
        {
            Debug.LogError("No Bullet left in the queue");
            CreateBullet();
        }
        GameObject bullet = bulletPool.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullets(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
