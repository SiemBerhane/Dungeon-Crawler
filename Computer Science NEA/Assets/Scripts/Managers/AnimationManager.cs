using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;


public class AnimationManager : MonoBehaviour
{

    public string SetMovementAnimState (float moveX, float moveY)
    {
        string animState = string.Empty;
        
        if (moveY > 0) // Moving away from the camera/North
        {
            animState = "Walk_Backwards";
        }
        else if (moveY < 0) // Moving towards the camera/South
        {
            animState = "Walk_Forward";
        }
        else if (moveX == 0 && moveY == 0) // Stationary
        {
            animState = "Idle";
        }
        else if (moveX != 0) // Moving horizontally
        {
            animState = "Walk_Side";
        }

        return animState;
    }


    public void GetPlayerRotation (string animStates, out float dirX, out float dirY, bool facingRight)
    {
        dirX = 0;
        dirY = 0;

        switch (animStates)
        {
            case "Walk_Forward":
                dirX = 0;
                dirY = -1;                
                break;
            
            case "Walk_Backwards":
                dirX = 0;
                dirY = 1;                
                break;

            case "Walk_Side":

                if (facingRight) {
                    dirX = 1;
                    dirY = 0;
                }           
                else {
                    dirX = -1;
                    dirY = 0;
                } 
                                  
                break;
        }        
    }

    public void PlayAnimation(string states, AnimationClip[] animClips, AnimancerComponent anim)
    {
        string stateName = states.ToString();
        for (int i = 0; i < animClips.Length; i++)
        {            
            if (stateName == animClips[i].name)
            {
                // Dont know what this error is for. Animations seem to work like normal, no error thrown in editor
                anim.Play(animClips[i]);
            }
        }
    }

    public IEnumerator PlayCoroutineAnimation(string states, AnimationClip[] animClips, AnimancerComponent anim)
    {   
        string stateName = states.ToString();
        int animIndent = 0;
        for (int i = 0; i < animClips.Length; i++)
        {
            if (stateName == animClips[i].name)
            {                
                animIndent = i;
            }
        }

        // Dont know what this error is for. Animations seem to work like normal, no error thrown in editor
        AnimancerState state = anim.Play(animClips[animIndent]);
        yield return state;
        Debug.Log(animClips[animIndent].name + " ended");
    }
}
