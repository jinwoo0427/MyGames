using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyBullet;
    public int coin = 0;
    
    protected float speed = 4f;
    public int hp = 3;
   
    [SerializeField]
    private float maxShotTime;
    [SerializeField]
    private float shotSpeed;
    [SerializeField]
    private float time;
    protected GameManager gameManager = null;

    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

       
    }

    protected virtual void Update()
    {
        time += Time.deltaTime;
        if (time > maxShotTime)
        {
            GameObject enemy = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            Enemybullet bulletmove = enemy.GetComponent<Enemybullet>();
            bulletmove.Enemyspeed = shotSpeed;
            time = 0;
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        MoveLimit(); 
       
    }
    protected virtual void MoveLimit()
    {

        if (transform.localPosition.x < GameManager.instance.MinPosition.x - 2f || transform.localPosition.x > GameManager.instance.MaxPosition.x + 2f)
        {
            Destroy(gameObject);
        }
        if (transform.localPosition.y < GameManager.instance.MinPosition.y - 5f || transform.localPosition.y > GameManager.instance.MaxPosition.y + 5f)
        {
            Destroy(gameObject);
        }
    }
    

    }
