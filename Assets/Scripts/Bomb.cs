using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : EnemyMove
{
    [SerializeField]
    private float Rotspeed = 10f;
    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    protected override void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * Rotspeed));
        if (transform.localPosition.x < gameManager.MinPosition.x - 2f)
        {
            Destroy(gameObject);
        }
    }
    


}
