using UnityEngine;

public class AppleCollectible : MonoBehaviour
{
    public float healthBoost = 25f;
    public float rotationSpeed = 50f;
    public float bobSpeed = 1f;
    public float bobHeight = 0.2f;
    
    private Vector3 startPosition;
    private float timeOffset;
    
    void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI); // Random starting phase
    }
    
    void Update()
    {
        // Rotate the apple
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        // Bob up and down
        float newY = startPosition.y + Mathf.Sin((Time.time + timeOffset) * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        RapperController rapper = other.GetComponent<RapperController>();
        
        if (rapper != null && !rapper.isDead)
        {
            // Heal the rapper
            rapper.Heal(healthBoost);
            
            // Play pickup sound if audio manager exists
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlayApplePickup();
            }
            
            // Visual feedback (optional)
            // You could add a particle effect here
            
            // Destroy the apple
            Destroy(gameObject);
        }
    }
}