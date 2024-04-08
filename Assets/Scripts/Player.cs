using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float clickRange;

    public Rigidbody2D rb;

    public GameObject barrel;
    private Vector3 aimDirection;

    public Transform firePoint;
    public GameObject bullet;

    public float moveSpeed;
    public float turnSpeed;
    public float bulletSpeed;
    public float fireRate;

    private float timeTillFire;

    //upgrades
    public GameObject moveSpeedBox;
    public int moveSpeedLevel;
    public TMP_Text moveSpeedText;
    public int moveSpeedPrice;
    public TMP_Text moveSpeedPriceText;

    public GameObject turnSpeedBox;
    public int turnSpeedLevel;
    public TMP_Text turnSpeedText;
    public int turnSpeedPrice;
    public TMP_Text turnSpeedPriceText;

    public GameObject fireRateBox;
    public int fireRateLevel;
    public TMP_Text fireRateText;
    public int fireRatePrice;
    public TMP_Text fireRatePriceText;

    public GameObject bulletSpeedBox;
    public int bulletSpeedLevel;
    public TMP_Text bulletSpeedText;
    public int bulletSpeedPrice;
    public TMP_Text bulletSpeedPriceText;

    public ParticleSystem leftMotor;
    public ParticleSystem rightMotor;
    private bool isMoving;

    public float health;
    public float maxHealth;

    public Image healthBar;
    public Image cooldownBar;




    void Start()
    {
        maxHealth = 10f;
        health = maxHealth;

        clickRange = .5f;

        moveSpeed = .1f;
        turnSpeed = 50f;
        bulletSpeed = 200f;
        fireRate = 2;

        timeTillFire = 0; 

        moveSpeedPrice = 25;
        moveSpeedLevel = 1;

        turnSpeedPrice = 25;
        turnSpeedLevel = 1;

        fireRatePrice = 25;
        fireRateLevel = 1;

        bulletSpeedPrice = 25;
        bulletSpeedLevel = 1;

    }

    void Update()
    {
        healthBar.fillAmount = health/maxHealth;
        cooldownBar.fillAmount = timeTillFire / fireRate;


        if(Input.GetKey(KeyCode.W))
        {
            Vector2 moveDirection = transform.up * moveSpeed;
            rb.AddForce(moveDirection);
            isMoving = true;
            if(!rightMotor.isPlaying)
            {
                rightMotor.Play();
            }
            if(!leftMotor.isPlaying)
            {
                leftMotor.Play();
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            Vector2 moveDirection = -transform.up * moveSpeed;
            rb.AddForce(moveDirection);
            isMoving = true;
            if(!rightMotor.isPlaying)
            {
                rightMotor.Play();
            }
            if(!leftMotor.isPlaying)
            {
                leftMotor.Play();
            }
        } 
        else 
        {
            isMoving = false;
            if(rightMotor.isPlaying)
            {
                rightMotor.Stop();
            }
            if(leftMotor.isPlaying)
            {
                leftMotor.Stop();
            }
        }
        if (isMoving)
        {
            if(!rightMotor.isPlaying)
            {
                rightMotor.Play();
            }
            if(!leftMotor.isPlaying)
            {
                leftMotor.Play();
            }            
            if (Input.GetKey(KeyCode.A))
            {
                float rotationAmount = turnSpeed * Time.deltaTime;
                rb.rotation += rotationAmount;
                if(leftMotor.isPlaying)
                {
                    leftMotor.Stop();
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                float rotationAmount = turnSpeed * Time.deltaTime;
                rb.rotation -= rotationAmount;
                if(rightMotor.isPlaying)
                {
                    rightMotor.Stop();
                }
            }
        }
        else
        {
            if(rightMotor.isPlaying)
            {
                rightMotor.Stop();
            }
            if(leftMotor.isPlaying)
            {
                leftMotor.Stop();
            }
        }        
            
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
        Vector3 eulerAngles = targetRotation.eulerAngles;
        barrel.transform.rotation = Quaternion.Euler(0f, 0f, eulerAngles.z);

        if(timeTillFire <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameObject currentBullet = Instantiate(bullet, firePoint.position, barrel.transform.rotation);
                Rigidbody2D bulletRb = currentBullet.GetComponent<Rigidbody2D>();
                bulletRb.AddForce(barrel.transform.up * bulletSpeed);
                timeTillFire = fireRate;
            }
        }
        timeTillFire -= Time.deltaTime;
        


        if (Input.GetMouseButtonDown(0))
        {
            GameController gc = FindObjectOfType<GameController>();
            float clickDistance = Vector2.Distance(mousePosition, moveSpeedBox.transform.position);
            if (clickDistance <= clickRange)
            {
                int points = gc.points;
                if (points >= moveSpeedPrice)
                {
                    gc.SpendPoints(moveSpeedPrice);
                    moveSpeedPrice *= 2;
                    moveSpeedPriceText.text = "Price: " + moveSpeedPrice;

                    moveSpeedLevel++;
                    moveSpeed *= 1.5f;
                    moveSpeedText.text = "" + moveSpeedLevel;

                }
            }
            float clickDistance2 = Vector2.Distance(mousePosition, turnSpeedBox.transform.position);
            if (clickDistance2 <= clickRange)
            {
                int points = gc.points;
                if (points >= turnSpeedPrice)
                {
                    gc.SpendPoints(turnSpeedPrice);
                    turnSpeedPrice *= 2;
                    turnSpeedPriceText.text = "Price: " + turnSpeedPrice;

                    turnSpeedLevel++;
                    turnSpeed *= 1.25f;
                    turnSpeedText.text = "" + turnSpeedLevel;
                }
            }
            float clickDistance3 = Vector2.Distance(mousePosition, fireRateBox.transform.position);
            if (clickDistance3 <= clickRange)
            {
                int points = gc.points;
                if (points >= fireRatePrice)
                {
                    gc.SpendPoints(fireRatePrice);
                    fireRatePrice *= 2;
                    fireRatePriceText.text = "Price: " + fireRatePrice;

                    fireRateLevel++;
                    fireRate *= .75f;
                    fireRateText.text = "" + fireRateLevel;
                }
            }
            float clickDistance4 = Vector2.Distance(mousePosition, bulletSpeedBox.transform.position);
            if (clickDistance4 <= clickRange)
            {
                int points = gc.points;
                if (points >= bulletSpeedPrice)
                {
                    gc.SpendPoints(bulletSpeedPrice);
                    bulletSpeedPrice *= 2;
                    bulletSpeedPriceText.text = "Price: " + bulletSpeedPrice;

                    bulletSpeedLevel++;
                    bulletSpeed *= 1.5f;
                    bulletSpeedText.text = "" + bulletSpeedLevel;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemyBullet")
        {
            Destroy(collision.gameObject);
            health -= 1f;
            if (health <= 0)
            {
                health = 0;
            }
        }
    }
}
