using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField] DefaultMagicBall magicBall;

    [Header("Variables")]
    [HideInInspector] public Vector2 scaler;

    private float speed;
    private float activeTime;
    private int damage;
    private SpriteRenderer spriteRenderer;
    private int enemyLayer, playerLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask[] whatCanHit;


    private void Start() {
        AssignValues();

        if ( (scaler.x * -1) == scaler.y || scaler.x == scaler.y) {
            scaler.x = 0;
        }

        rb.velocity = scaler * speed;
        Destroy(gameObject, activeTime);
    }

    private void AssignValues() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyLayer = 8;
        playerLayer = 10;

        speed = magicBall.travelSpeed;
        activeTime = magicBall.lifeSpan;
        damage = magicBall.damage;
        spriteRenderer.sprite = magicBall.magicBallSprite;
    }

    private void OnTriggerEnter2D(Collider2D col) {

        // Don't destroy magic ball if it collides with another or collides with the room collider
        if (col.CompareTag("MagicBall") || col.CompareTag("Room")) 
        {
            return;
        }

        // Loops through all the layers a bullet can damage
        // Checks if the bullet hit a player or an enemy and deals damage accordingly
        for (int i = 0; i < whatCanHit.Length; i++)
        {
            if ((whatCanHit[i].value & (1 << col.transform.gameObject.layer)) > 0) {

                if ((whatCanHit[i].value & (1 << enemyLayer)) > 0) {
                    ParentEnemyClass e = col.GetComponent<ParentEnemyClass>();
                    e.CallTakeDamage(damage);
                }
                else if ((whatCanHit[i].value & (1 << playerLayer)) > 0) {
                    PlayerBehaviour p = col.GetComponent<PlayerBehaviour>();
                    p.CallTakeDamage(damage);
                }

                Destroy(gameObject);
            }
        }


        Destroy(gameObject);
        
    }


}
