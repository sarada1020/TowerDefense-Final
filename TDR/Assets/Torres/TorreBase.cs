using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class TorreBase : MonoBehaviour
{
    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firingPoint;
    [SerializeField] protected float bps = 1f;

    protected Transform target;
    private float timeUntilFire;

}
