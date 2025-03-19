using UnityEngine;

public class Bullet : MonoBehaviour
{
    private RapperController shooter;
    private float damage;
    private float range;
    private Vector3 startPosition;
    
    private AudioManager audioManager;
    
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Destroy(gameObject, 5f); // Cleanup after 5 seconds if nothing is hit
    }
    
    public void Initialize(RapperController owner, float bulletDamage, float bulletRange)
    {
        shooter = owner;
        damage = bulletDamage;
        range = bulletRange;
        startPosition = transform.position;
    }
    
    void Update()
    {
        // Check if bullet has exceeded its range
        if (Vector3.Distance(startPosition, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        RapperController hitRapper = other.GetComponent<RapperController>();
        
        if (hitRapper != null && hitRapper != shooter)
        {
            // Deal damage
            hitRapper.TakeDamage(damage);
            
            // Play hit sound
            if (audioManager != null)
            {
                audioManager.PlayHit();
            }
            
            // Destroy bullet
            Destroy(gameObject);
        }
        else if (!other.isTrigger) // Hit environment
        {
            Destroy(gameObject);
        }
    }
}