using UnityEngine;

public class Gun : Weapon
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float bulletSpeed = 20f;
    public float range = 10f;
    
    private float nextFireTime;
    
    public override void Use()
    {
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    private void Fire()
    {
        // Create bullet
        GameObject bulletObj = Instantiate(
            bulletPrefab,
            transform.position + transform.forward,
            transform.rotation
        );
        
        // Set up bullet
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Initialize(owner, damage, range);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }
        
        // Play sound effect
        if (audioManager != null)
        {
            audioManager.PlayGunshot();
        }
    }
}