using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;

    public Transform shootPoint;
    public float bulletSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        var bullet = Instantiate(bullet1, shootPoint);
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right;
    }
}
