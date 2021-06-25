using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermove : MonoBehaviour
{
    
    [Header("이동 속도")]
    [SerializeField]
    private float speed = 10f;
    
    [SerializeField]
    private float shotMax = 0.5f;
    [SerializeField]
    private float shotDelay = 0;
    private Vector2 targetPosition = Vector2.zero;
    private GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    [SerializeField]
    private GameObject bullet = null;
    [SerializeField]
    private Transform bulletPosition = null;
    [SerializeField]
    private GameObject razerPrefab = null;
    [SerializeField]
    private GameObject explosion = null;
    private Animator animator = null;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";


    void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        Move();
        if (GameManager.instance.isskill == false)
        {
            Shot();
        }
        else
        {
            
            Invoke("Shot", 3f);
        }
    }

    private void Shot()
    {


        if (GameManager.instance.isskill == true)
            return;

        shotDelay += Time.deltaTime;
            
            if (shotDelay > shotMax)
            {
                shotDelay = 0;
                Vector2 vec = new Vector2(transform.position.x, transform.position.y);
                Instantiate(bullet, vec, Quaternion.identity);
            }
        
       

    }

    public void Razershot()
    {
        GameObject razer;
        SoundManager.instance.RazerSound();
        razer = Instantiate(razerPrefab, bulletPosition);
        StartCoroutine(SkillDelay());
    }
    IEnumerator SkillDelay()
    {
        yield return new WaitForSeconds(3f);
        gameManager.isskill = false;
    }
   

    private void Move()
    {
        float newX;
        float newY;
        if (Application.platform == RuntimePlatform.Android)
        {
            float x = SimpleInput.GetAxisRaw(horizontalAxis);
            float y = SimpleInput.GetAxisRaw(verticalAxis);
            Vector3 dir = new Vector3(x, y, 0).normalized;
            transform.position = transform.position + dir * Time.deltaTime * speed;
            newX = transform.position.x;
            newY = transform.position.y;
        }
        else
        {
            float x = SimpleInput.GetAxisRaw("Horizontal");
            float y = SimpleInput.GetAxisRaw("Vertical");
            Vector3 dir = new Vector3(x, y, 0).normalized;
            transform.position = transform.position + dir * Time.deltaTime * speed;
            newX = transform.position.x;
            newY = transform.position.y;
        }

        newX = Mathf.Clamp(newX, gameManager.MinPosition.x+0.5f, gameManager.MaxPosition.x+0.5f);
        newY = Mathf.Clamp(newY, gameManager.MinPosition.y+0.5f, gameManager.MaxPosition.y);
        transform.position = new Vector3(newX, newY, transform.position.z);
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Item")
        {
            SoundManager.instance.CoinSound();
            Coin coinScript = collision.gameObject.GetComponent<Coin>();
            GameManager.instance.coin += coinScript.coinSize;
            GameManager.instance.UpdateScore();
            
            Destroy(collision.gameObject);


        }
        else if (collision.gameObject.tag == "Bomb" || collision.gameObject.tag == "Enemy" ||
                collision.gameObject.tag == "EnemyBullet")
        {

            Destroy(collision.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);

            if (GameManager.instance.life > 1)
            {
                GameManager.instance.life--;
                GameManager.instance.UpdateLife();
                StartCoroutine(Damaged());
            }
            else
            {
                StartCoroutine(Dead());
            }


            
        }
       
    }
   
    private IEnumerator Damaged()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    private IEnumerator Dead()
    {
        PlayerPrefs.SetInt("Score", GameManager.instance.coin);

        animator.Play("New Animation");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }

}
