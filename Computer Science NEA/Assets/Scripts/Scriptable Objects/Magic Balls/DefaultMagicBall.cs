using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicBall", menuName = "ScriptableObjects/Weapons/MagicBalls")]
public class DefaultMagicBall : ScriptableObject
{
    public string magicBallName;

    public float travelSpeed;
    public float lifeSpan;
    public int damage;
    public Sprite magicBallSprite;
}
