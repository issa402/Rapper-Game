using UnityEngine;

public class Gun : Weapon
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float bulletSpeed = 20f;
    public float range = 10f;
    
    [Header("Visual Effects")]
    public Color bulletColor = Color.yellow;
    public GameObject muzzleFlashPrefab;
    public float muzzleFlashDuration = 0.1f;
    public LineRenderer bulletTrailPrefab;
    public float trailDuration = 0.1f;
    
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
        // Create muzzle flash if prefab exists
        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, transform.position + transform.forward * 0.5f, transform.rotation);
            Destroy(flash, muzzleFlashDuration);
        }
        
        // Create bullet trail if prefab exists
        if (bulletTrailPrefab != null)
        {
            LineRenderer trail = Instantiate(bulletTrailPrefab);
            trail.SetPosition(0, transform.position + transform.forward * 0.5f);
            trail.SetPosition(1, transform.position + transform.forward * range);
            
            // Set color
            trail.startColor = bulletColor;
            trail.endColor = new Color(bulletColor.r, bulletColor.g, bulletColor.b, 0f);
            
            Destroy(trail.gameObject, trailDuration);
        }
        
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
            
            // Set bullet color
            Renderer bulletRenderer = bulletObj.GetComponent<Renderer>();
            if (bulletRenderer != null)
            {
                bulletRenderer.material.color = bulletColor;
            }
            
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }
        
        // Play sound effect
        if (audioManager != null)
        {
            audioManager.PlayGunshot();
        }
    }
}