using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallWeaponBehaviour : WeaponBaseBehaviour
{
    public bool isShootComeBack;
    private float bulletBackSpeed;
    private void Update()
    {
        if (isShoot)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, currentTargetTransform.position,
                Time.deltaTime * bulletForwardSpeed);
            bulletBackSpeed = bulletSpeed;
        }
        else if (isShootComeBack)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, bulletInitialTransform.position,
                Time.deltaTime * bulletBackSpeed);
            bulletBackSpeed += Time.deltaTime*5;
            if(currentTargetTransform)
            {
                Destroy(currentTargetTransform.gameObject);
                currentTargetTransform = null;
            }
        }
        else
        {
            bulletForwardSpeed = bulletBackSpeed = bulletSpeed;
            bullet.transform.position = bulletInitialTransform.position;
        }

        if ( currentTargetTransform && Vector3.Distance(bullet.transform.position, currentTargetTransform.position) < 0.1f)
        {
            ShootComeBack();
        }

        if (Vector3.Distance(bullet.transform.position, bulletInitialTransform.position) < 0.1f)
        {
            
            //bullet.transform.parent = transform;
            isShootComeBack = false;
            canPlayerShoot = true;
        }
    }

    void ShootComeBack()
    {
        isShoot = false;
        isShootComeBack = true;
    }
}
