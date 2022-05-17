using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeedForward,playerSpeedSides;

    public WeaponBaseBehaviour activeWeapon;

    [SerializeField]private CharacterController _characterController;
    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = GetComponentInChildren<WeaponBaseBehaviour>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeWeapon.canPlayerShoot && Input.GetKeyDown(KeyCode.Space))
        {
            activeWeapon.Shoot();
        }

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");


        _characterController.Move(transform.forward * (verticalInput * playerSpeedForward * Time.deltaTime));
        transform.Rotate(Vector3.up, horizontalInput*playerSpeedSides*Time.deltaTime);
    }

    public void WeaponUpgrade()
    {
        
        activeWeapon.bulletSpeed = 5 * Vector3.Distance(activeWeapon.bullet.transform.position, activeWeapon.weaponRangeTransform.position);
        activeWeapon.weaponRangeTransform.Translate(activeWeapon.weaponRangeTransform.forward*0.5f);
    }
}
