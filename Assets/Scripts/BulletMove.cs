using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public enum BULLET_MODE
    {
        PLAYER = 0,
        ENEMY = 1
    }
    [SerializeField]    
    private Sprite playerBulletSprite = null;
    [SerializeField]
    private Sprite enemyBulletSprite = null;
    
    private SpriteRenderer spriteRenderer = null;
    [Header("이동 속도")]
    protected float speed = 10f;
    [SerializeField]
    protected GameObject coin;
    [SerializeField]
    protected GameObject explosion = null;
    [SerializeField]
    protected GameObject ShotEffect = null;
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        
            CheckLimit();
        

    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
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

    protected virtual void Despawn()
    {
       
        gameObject.SetActive(false);
        transform.SetParent(GameManager.instance.poolManager.transform, false);
    }

    protected virtual void SetBulletMode(BULLET_MODE bulletMode = BULLET_MODE.PLAYER)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (bulletMode == BULLET_MODE.PLAYER)
        {
            spriteRenderer.sprite = playerBulletSprite;
            gameObject.layer = GameManager.instance.PLAYER_LAYER;
        }
        else 
        {
            spriteRenderer.sprite = enemyBulletSprite;
            gameObject.layer = GameManager.instance.ENEMY_LAYER;
        }
        
    }

    protected virtual void CheckLimit()
    {
        
        
            if (transform.localPosition.x < GameManager.instance.MinPosition.x - 2f || transform.localPosition.x > GameManager.instance.MaxPosition.x + 2f)
            {
                Despawn();
            }
            if (transform.localPosition.y < GameManager.instance.MinPosition.y - 5f || transform.localPosition.y > GameManager.instance.MaxPosition.y + 5f)
            {
                Despawn();
            }
        
        
    }


}
