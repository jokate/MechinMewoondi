using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShootStat")]

public class ShooterStat : ScriptableObject
{
    public enum Size
    {
        Small,
        Medium,
        Large
    };

    public Size size;

    public float damage;
    public float attatckSec;
    public Sprite spr, shootingspr;


}

