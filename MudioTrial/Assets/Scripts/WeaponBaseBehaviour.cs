using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBaseBehaviour : MonoBehaviour
{
    public Transform bulletInitialTransform;
    public Transform weaponRangeTransform;
    public Transform currentTargetTransform;
    public float bulletSpeed;
    public float bulletForwardSpeed;
    public GameObject bullet;
    public bool isShoot ;
    public bool canPlayerShoot;
    public Vector3 shootPos;

    private void Start()
    {
        canPlayerShoot = true;
        bulletSpeed = Vector3.Distance(bullet.transform.position, weaponRangeTransform.position)*5;
    }

    public void Shoot()
    {
        currentTargetTransform = Instantiate(weaponRangeTransform, weaponRangeTransform.position, Quaternion.identity);
        canPlayerShoot = false;
        bullet.transform.parent = null;
        shootPos = transform.root.position;
        
        isShoot = true;
    }
    
    
}
