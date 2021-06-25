using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemybullet : BulletMove
{
    
    public float Enemyspeed = 4;



    void Start()
    {
    }

    void Update()
    {
        CheckLimit();

        transform.Translate(Vector2.left * Enemyspeed * Time.deltaTime);
    }

    protected override void Despawn()
    {
        base.Despawn();
    }
    protected override void SetBulletMode(BULLET_MODE bulletMode = BULLET_MODE.ENEMY)
    {
        base.SetBulletMode(bulletMode);
    }
    protected override void CheckLimit()
    {
        base.CheckLimit();
    }
}
