using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStat")]
public class EnemyStat : ScriptableObject {
    public enum Size
    {
       Small,
       Medium,
       Large,
       None
    };

    public Size size;
    public float health;
    public float rotationSpeed;
    public Sprite spr;
    public int money;

}
