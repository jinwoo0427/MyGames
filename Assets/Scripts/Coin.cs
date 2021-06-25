using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{


    public int coinSize = 1;
    [SerializeField]
    private float speed = 10f;
    
    private void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            GameManager.instance.coin += coinSize;
            GameManager.instance.UpdateScore();
            Destroy(gameObject, 0.1f);

        }
    }
        private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
