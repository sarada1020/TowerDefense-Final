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

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    protected virtual void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Tiro bulletScript = bulletObj.GetComponent<Tiro>();
        bulletScript.SetTarget(target);
    }

    protected bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }


}
