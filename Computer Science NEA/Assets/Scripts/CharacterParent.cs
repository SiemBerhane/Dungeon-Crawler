using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class CharacterParent : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Rigidbody2D rb; 
    [SerializeField] protected Transform rotationObject;


    [Header("Attack")]
    [SerializeField] protected Transform gunTip;
    [SerializeField] protected GameObject bullet;
    protected DefaultBullet bulletScript;
    

    [Header("Health")]
    [SerializeField] protected int maxHealth;
    protected int currentHealth;

    
    [Header("Animation")]
    [SerializeField] protected AnimationManager animManager;
    [SerializeField] protected AnimationClip[] animClips;
    [SerializeField] protected AnimancerComponent anim;
    protected bool facingRight = true;

    protected string animState;


    protected virtual void Start()
    {
        currentHealth = maxHealth;
        bulletScript = bullet.GetComponent<DefaultBullet>();
    }


    // <-------- Attacking -------->
    protected virtual void Attack()
    {
        bulletScript.scaler.x = transform.localScale.x;
        bulletScript.scaler.y = gunTip.localScale.y;
        Instantiate(bullet, gunTip.position, Quaternion.identity);
    }


    // <-------- Health -------->
    protected virtual void TakeDamage(int damage) {
        currentHealth -= damage;
        // print($"{gameObject.name}: has taken {damage} damage!");
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    protected virtual void IncreaseHealth(int health) {
        currentHealth += health;
        // print($"{gameObject.name}: has increased their health from: {currentHealth - health} to: {currentHealth}!");
    }


    // <-------- Movement -------->
    protected virtual void CharacterMove (float moveX, float moveY)
    {               
        Vector2 move = new Vector2(moveX, moveY).normalized;
        rb.velocity = move * moveSpeed * Time.fixedDeltaTime;
    }


    // <-------- Animations -------->
    protected virtual void LocalAnimatonManager (float moveX, float moveY, out float DirX, out float DirY) {
        animState = animManager.SetMovementAnimState(moveX, moveY);
        animManager.GetPlayerRotation(animState, out DirX, out DirY, facingRight);
    }

    protected virtual void LocalPlayAnimation ()
    {
        animManager.PlayAnimation(animState, animClips, anim);
    }

    protected virtual void PlayCoroutineAnimation ()
    {
        animManager.PlayCoroutineAnimation(animState, animClips, anim);
    }

    protected virtual void Flip ()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;               
    }

    protected virtual void SetScalerValue (float dirX, float dirY) {
        Vector3 Scaler = rotationObject.localScale;        
        Scaler.x = dirX;
        Scaler.y = dirY;
        rotationObject.localScale = Scaler;
        gunTip.localScale = Scaler;
    }

}

