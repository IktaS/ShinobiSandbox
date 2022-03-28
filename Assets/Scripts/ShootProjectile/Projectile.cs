using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public abstract string PoolName();
    public abstract void Shoot(Vector3 start, Vector3 dir, float speed, float distance);
}
