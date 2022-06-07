using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject SubEnemy;
    public Transform SubEnmemyLeft;
    public Transform SubEnmemyRight;

    public int numberEnemy;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(BossSpawn());

    }

    IEnumerator BossSpawn()
    {
        yield return new WaitForSeconds(startWait);

        


        for ( int i = 0; i < numberEnemy; i++)
        {
            Instantiate(SubEnemy, SubEnmemyLeft.position, SubEnmemyLeft.rotation);



            Instantiate(SubEnemy, SubEnmemyRight.position, SubEnmemyRight.rotation);


            yield return new WaitForSeconds(spawnWait);

            yield return new WaitForSeconds(spawnWait);


            yield return new WaitForSeconds(spawnWait);
            yield return new WaitForSeconds(spawnWait); yield return new WaitForSeconds(spawnWait);
            yield return new WaitForSeconds(spawnWait);
            yield return new WaitForSeconds(spawnWait);
            yield return new WaitForSeconds(spawnWait);
        }

        yield return new WaitForSeconds(waveWait);


    }


}
