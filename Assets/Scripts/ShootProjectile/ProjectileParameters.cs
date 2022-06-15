using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileParameters", menuName = "ScriptableObjects/ProjectileParameters", order = 1)]
public class ProjectileParameters : ScriptableObject
{
    public float speed;
    public float maxDistance = 200f;
}
