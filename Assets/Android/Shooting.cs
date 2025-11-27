using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 10f;

    private bool isMobile;

    private void Start()
    {
        isMobile = MobileInputManager.Instance.IsMobileDevice();
    }

    void Update()
    {
        if (isMobile && SimpleInput.GetButtonDown("Fire"))
        {
            Shoot();
        }
        else if ( !isMobile && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}