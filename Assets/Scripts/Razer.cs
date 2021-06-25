using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razer : BulletMove
{
    void Start()
    {

    }



    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Bomb")
        {
            Bomb bomb = collision.gameObject.GetComponent<Bomb>();
            bomb.hp -= 1;
            Instantiate(ShotEffect, transform.position, Quaternion.identity);
            if (bomb.hp <= 0)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);
                GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                Coin coinScript = coinObj.GetComponent<Coin>();
                coinScript.coinSize = bomb.coin;
                Destroy(collision.gameObject);
            }
            
        }
        else if (collision.tag == "Enemy")
        {
            EnemyMove enemies = collision.gameObject.GetComponent<EnemyMove>();
            enemies.hp -= 1;
            Instantiate(ShotEffect, transform.position, Quaternion.identity);
            if (enemies.hp <= 0)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);
                GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                Coin coinScript = coinObj.GetComponent<Coin>();
                coinScript.coinSize = enemies.coin;
                Destroy(collision.gameObject);
            }
            
        }
        
    }
    
}
