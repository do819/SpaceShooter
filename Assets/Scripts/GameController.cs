using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int score;
    private bool restart;
    private bool nextLevel;
    private bool gameOver;
    private int level;

    public GameObject hazard;
    public GameObject enemy_one;
    public GameObject enemy_two;
    public GameObject enemy_three;
    public GameObject mainCamera;
    public GameObject subCamera;
    public Vector3 spawnValue;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public int bullet;
    public float fireRate;
    public int speed;
    public bool activeSubWebpon;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    // Start is called before the first frame update
    void Start()
    {
        subCamera.SetActive(true);
        mainCamera.SetActive(false);
        restart = false;
        nextLevel = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        bullet = 1;
        fireRate = (float)0.7;
        speed = 5;
        activeSubWebpon = false;
        level = 2;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (nextLevel)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void FixedUpdate()
    {
        if (subCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            subCamera.SetActive(false);
            mainCamera.SetActive(true);
            //StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startWait);


        int amount = 2 + level * 3;

        for(int j = 1; j <= amount; j++)
        {
            if (level <= 1)
            {
                // Spawn Harard
                
                StartCoroutine(HazardSpawn());
            }
            else
            {
                //StartCoroutine(EnemySpawn());

                int choosetype = Random.Range(1, 3);

                switch (choosetype)
                {
                    case 1:
                        //debug.log("spawn");
                        // spawn harard
                        StartCoroutine(HazardSpawn());
                        //StartCoroutine(EnemySpawn());
                        break;
                    case 2:
                        //debug.log("spawn");
                        // spawn ennemy
                        StartCoroutine(EnemySpawn());
                        break;
                    default:
                        break;
                }
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                Restart();
                break;
            }

            if (j == amount)
            {
                NextLevel();
                break;
            }
        }


    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateWeapon(string tag)
    {
        switch (tag)
        {
            case "BulletItem":
                bullet += ((bullet <= 5) ? 1 : 0);
                break;
            case "FireRateItem":
                fireRate -= ((fireRate <= 0.2f) ? 0 : 0.1f);
                Debug.Log("fireRate: " + fireRate);
                break;
            case "SpeedItem":
                speed += ((speed <= 15) ? 1 : 0);
                Debug.Log("speed: " + speed);
                break;
            case "SubWeaponItem":
                activeSubWebpon = true;
                break;
            default:
                break;
        }
    }

    IEnumerator HazardSpawn()
    {
        //Debug.Log("Spawn1");
        for (int i = 0; i < hazardCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
            Quaternion spawnRotation = Quaternion.identity;

            Instantiate(hazard, spawnPosition, spawnRotation);

            yield return new WaitForSeconds(spawnWait);

            if (gameOver)
            {
                break;
            }
        }
    }

    IEnumerator EnemySpawn()
    {
        
        int chooseEnemy = Random.Range(1, 4);

        for (int i = 0; i < hazardCount; i++)
        {
            yield return new WaitForSeconds(spawnWait);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
            Quaternion spawnRotation = new Quaternion(0f, 180f,0f,0f);
            

            switch (chooseEnemy)
            {
                case 1:
                    Instantiate(enemy_one, spawnPosition, spawnRotation);
                    break;
                case 2:
                    Instantiate(enemy_two, spawnPosition, spawnRotation);
                    break;
                case 3:
                    Instantiate(enemy_three, spawnPosition, spawnRotation);
                    break;
                default:
                    break;
            }

            yield return new WaitForSeconds(spawnWait);
          

            if (gameOver)
            {
                break;
            }

            yield return new WaitForSeconds(spawnWait);
        }
    }

    private void Restart()
    {
        restartText.text = "Press 'R' for Restart";
        restart = true;
    }

    public void NextLevel()
    {
        gameOverText.text = "Well done. Press 'N' for NextLevel";
        nextLevel = true;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}
