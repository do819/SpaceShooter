using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;

    private int speed;
    private float tilt;
    private float fireRate;
    private float nextFire;
    private int numberBullet;
    private bool activeSubWebpon;

    public GameObject shot;
    public Transform shotSpawn;
    public Transform shotSpawnLeft;
    public Transform shotSpawnRight;
    public GameObject SubPlayerLeft;
    public GameObject SubPlayerRight;
    public Boundary boundary;


    private Rigidbody rb;
    private AudioSource auSrc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auSrc = GetComponent<AudioSource>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Can't find 'GameController' script");
        }
        numberBullet = gameController.bullet;
        speed = gameController.speed;
        fireRate = gameController.fireRate;
        activeSubWebpon = gameController.activeSubWebpon;
        SubPlayerLeft.SetActive(false);
        SubPlayerRight.SetActive(false);
    }

    private void Update()
    {
        numberBullet = gameController.bullet;
        activeSubWebpon = gameController.activeSubWebpon;
        if (activeSubWebpon)
        {
            SubPlayerLeft.SetActive(true);
            SubPlayerRight.SetActive(true);
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            int rotationAngle = 180 / numberBullet;

            for (int i = 1; i <= numberBullet; i++)
            {
                shotSpawn.rotation = Quaternion.Euler(0, -90 + (rotationAngle * i) - (rotationAngle / 2), 0);
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            if (activeSubWebpon)
            {
                Instantiate(shot, shotSpawnLeft.position, shotSpawnLeft.rotation);
                Instantiate(shot, shotSpawnRight.position, shotSpawnRight.rotation);
            }

            auSrc.Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * (float)speed;
        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
