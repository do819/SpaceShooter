using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    const int ITEM_DROP_CHANGE = 100;

    private GameController gameController;

    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject BulletItem;
    public GameObject FireRateItem;
    public GameObject SpeedItem;
    public GameObject SubWeaponItem;

    public int scoreValue;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Can't find 'GameController' script");
        }
    }
  
    void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Boundary" || other.tag.EndsWith("Item") || other.tag.StartsWith("Enemy") )
            
        {
            
            return ;

        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        if (other.tag == "Bolt")
        {
            int itemDropPercent = Random.Range(1, 101);
            if (itemDropPercent <= ITEM_DROP_CHANGE)
            {
                int chooseItem = Random.Range(1, 5);
                switch (chooseItem)
                {
                    case 1:
                        Instantiate(BulletItem, other.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(FireRateItem, other.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(SpeedItem, other.transform.position, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(SubWeaponItem, other.transform.position, Quaternion.identity);
                        break;
                    default:
                        break;
                }
                
            }
            if(tag == "EnemyBolt")
            {
                Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

        }

        Instantiate(explosion, transform.position, transform.rotation);

        gameController.AddScore(scoreValue);
        
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
