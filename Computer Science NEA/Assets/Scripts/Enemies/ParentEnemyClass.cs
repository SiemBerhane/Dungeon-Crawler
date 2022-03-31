using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentEnemyClass : CharacterParent
{
    // FIX SHOOTING !!!!!


    protected PlayerBehaviour player;

    [Header("Enemy Parent Specific - Movement")]
    [SerializeField] protected float maxDistFromPlayer;
    [SerializeField] protected float maxAttackRange;
    [SerializeField] protected float playerCheckRadius;
    [SerializeField] protected LayerMask playerLayer;
    protected Vector3 direction;


    [Header("Enemy Parent Specific - Attack")]
    [SerializeField] protected float timeBtwShot;
    [SerializeField] protected bool canShoot = true;
    protected float gunTipY;

    protected enum States {
        Idle,
        Alert,
        Attack
    }

    protected States enemyState;


    protected override void Start()
    {
        base.Start();
        facingRight = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    protected virtual void Update() {
        float moveX;
        float moveY;

        float dirX;
        float dirY;

        StateMachine(out moveX, out moveY);
        LocalAnimatonManager(moveX, moveY, out dirX, out dirY);
        SetScalerValue(dirX, dirY);

        // Doesn't make any sense but it works        
        if (direction.x > 0 && !facingRight) {
            Flip();
        }
        else if (direction.x < 0 && facingRight) {
            Flip();
        }
    }


    // <-------- State Machine -------->
    protected virtual void StateMachine(out float moveX, out float moveY) {
        moveX = 0;
        moveY = 0;

        switch (enemyState) {

            case States.Idle:
                Idle();
                break;
            
            case States.Alert:
                MoveTowardsPlayer(out moveX, out moveY);
                break;

            case States.Attack:
                Attack();
                break;
        }
    }


    // <-------- Movement -------->
    protected virtual void Idle() {
        if (Physics2D.OverlapCircle(transform.position, playerCheckRadius, playerLayer)) {
            print("Player entered enemy zone");
            enemyState = States.Alert;
        }
    }

    protected virtual void MoveTowardsPlayer(out float moveX, out float moveY) {        
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        moveX = rb.position.x;
        moveY = rb.position.y;


        if (Vector2.Distance(transform.position, player.transform.position) <= maxAttackRange) {
            enemyState = States.Attack;
            gunTipY = gunTip.localScale.y;
            print("Attacking... ");
        }
        else if (Vector2.Distance(transform.position, player.transform.position) >= maxDistFromPlayer) {
            enemyState = States.Idle;
            print("Player too far away");
        }

    }


    // <-------- Attacking -------->
    protected virtual IEnumerator AttackDelay() {
        yield return new WaitForSeconds(timeBtwShot);        
        canShoot = true;
    }

    protected override void Attack()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (direction.x > 0 && !facingRight) {
            Flip();
        }
        else if (direction.x < 0 && facingRight) {
            Flip();
        }

        if (Vector2.Distance(transform.position, player.transform.position) >= maxAttackRange) {
            enemyState = States.Alert;
            print("Player too far away to be hit");
            return;
        }

        if (canShoot) {            
            SpawnBullet(direction);
            canShoot = false;
            StartCoroutine(AttackDelay());
        }            
        else {
            return;
        }
    }

    protected virtual void SpawnBullet(Vector3 dir) {
        bulletScript.scaler = dir;
        Instantiate(bullet, gunTip.position, Quaternion.identity);
    }


    // <-------- Health -------->
    public void CallTakeDamage(int damage) {
        TakeDamage(damage);
    }

    protected override void TakeDamage(int damage) {
        base.TakeDamage(damage);
    }

    public void CallIncreaseHealth(int health) {
        IncreaseHealth(health);
    }

    protected override void IncreaseHealth(int health)
    {
        base.IncreaseHealth(health);
    }


    // <-------- Animations -------->
    protected override void LocalAnimatonManager(float moveX, float moveY, out float DirX, out float DirY)
    {
        base.LocalAnimatonManager(moveX, moveY, out DirX, out DirY);
    }

    protected override void Flip()
    {
        base.Flip();
    }

    protected override void SetScalerValue(float dirX, float dirY)
    {
        base.SetScalerValue(dirX, dirY);
    }


    // <-------- Debug -------->
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxAttackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxDistFromPlayer);
    }
}
