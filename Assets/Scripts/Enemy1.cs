using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private float randomSpeedChange;

    public Transform barrel;
    public Transform barrel2;
    public Transform barrel3;


    private GameObject player;

    public GameObject bullet;
    public Transform firePoint;    
    public Transform firePoint2;
    public Transform firePoint3;

    public float bulletSpeed;
    public float fireRate;
    private float timeTillFire;

    void Start()
    {
        randomSpeedChange = Random.Range(-0.15f, 0.15f);
        speed += randomSpeedChange;

        player = GameObject.FindGameObjectWithTag("player");

        timeTillFire = fireRate * 2f;
    }

    void Update()
    {
        rb.velocity = transform.up * speed;

        Vector3 aimDirection = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
        Vector3 eulerAngles = targetRotation.eulerAngles;
        barrel.rotation = Quaternion.Euler(0f, 0f, eulerAngles.z);
        if(barrel2 != null)
        {
            Vector3 aim = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, aim);
            Vector3 euler = rotation.eulerAngles;
            barrel2.rotation = Quaternion.Euler(0f, 0f, euler.z);
        }
        if(barrel3 != null)
        {
            Vector3 aim2 = player.transform.position - transform.position;
            Quaternion rotation2 = Quaternion.LookRotation(Vector3.forward, aim2);
            Vector3 euler2 = rotation2.eulerAngles;
            barrel3.rotation = Quaternion.Euler(0f, 0f, euler2.z);
        }

        if(timeTillFire <= 0)
        {
            GameObject currentBullet = Instantiate(bullet, firePoint.position, barrel.transform.rotation);
            Rigidbody2D bulletRb = currentBullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(barrel.transform.up * bulletSpeed);

            if(barrel2 != null)
            {
                GameObject bullet2 = Instantiate(bullet, firePoint2.position, barrel2.transform.rotation);
                Rigidbody2D bullet2Rb = bullet2.GetComponent<Rigidbody2D>();
                bullet2Rb.AddForce(barrel2.transform.up * bulletSpeed);
            }
            if(barrel3 != null)
            {
                GameObject bullet3 = Instantiate(bullet, firePoint3.position, barrel3.transform.rotation);
                Rigidbody2D bullet3Rb = bullet3.GetComponent<Rigidbody2D>();
                bullet3Rb.AddForce(barrel3.transform.up * bulletSpeed);
            }

            timeTillFire = fireRate;
        }
        timeTillFire -= Time.deltaTime;

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "destroy")
        {
            Destroy(gameObject);
        }
        if(collider.gameObject.tag == "playerBullet")
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}
