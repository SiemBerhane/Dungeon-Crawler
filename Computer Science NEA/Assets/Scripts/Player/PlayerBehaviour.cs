using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class PlayerBehaviour : CharacterParent
{
    [Header("Child Specific - Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private int numOfDashes;
    private float currentDashDuration;
    public float currentDashCoolDown;

    private int currentNumOfDashes;
    private DashStates dashStates;
    private enum DashStates {
        Ready,
        Dashing,
        Cooldown
    }


    protected override void Start ()
    {
        base.Start();

        currentDashDuration = dashDuration;
        currentDashCoolDown = dashCoolDown;
        currentNumOfDashes = numOfDashes;
        dashStates = DashStates.Ready;
    }

    protected void Update()
    {
        float dirX;
        float dirY;

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        CharacterMove(movementX, movementY);
        LocalAnimatonManager(movementX, movementY, out dirX, out dirY);    
        SetScalerValue(dirX, dirY);
        LocalPlayAnimation();
        DashManager();

        if (movementX > 0 && !facingRight) {
            Flip();
        }
        else if (movementX < 0 && facingRight) {
            Flip();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }


    // <-------- Dash -------->
    private void DashManager()
    {
        switch (dashStates) {
            case DashStates.Ready:

                if (Input.GetKeyDown(KeyCode.Space)) {
                    dashStates = DashStates.Dashing;
                }
                break;

            case DashStates.Dashing:
                
                while (currentDashDuration > 0) {
                    Vector2 newDashVector = new Vector2(rotationObject.localScale.x * dashSpeed, rotationObject.localScale.y * dashSpeed);
                    rb.AddForce(newDashVector * Time.deltaTime);
                    currentDashDuration -= Time.deltaTime;
                }

                currentDashDuration = dashDuration;
                dashStates = DashStates.Cooldown;
                break;

            case DashStates.Cooldown:
                StartCoroutine(DashCoolDown());
                break;
        }
    }

    private IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(dashCoolDown);
        dashStates = DashStates.Ready;
    }


    // <-------- Attacking -------->
    protected override void Attack()
    {
        base.Attack();
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


    // <-------- Movement -------->
    protected override void CharacterMove (float moveX, float moveY)
    {               
        base.CharacterMove(moveX, moveY);
    }


    // <-------- Animations -------->
    protected override void LocalAnimatonManager (float moveX, float moveY, out float DirX, out float DirY) {
        base.LocalAnimatonManager(moveX, moveY, out DirX, out DirY);
    }

    protected override void LocalPlayAnimation ()
    {        
        base.LocalPlayAnimation();
    }

    protected override void PlayCoroutineAnimation ()
    {
        base.PlayCoroutineAnimation();
    }

    protected override void Flip ()
    {
        base.Flip();            
    }

    protected override void SetScalerValue (float dirX, float dirY) {
        base.SetScalerValue(dirX,  dirY);
    }

}
