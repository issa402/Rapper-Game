using UnityEngine;
using System.Collections;

public class RapperController : MonoBehaviour
{
    [Header("Identity")]
    public string rapperName = "";
    
    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 100f;
    
    [Header("Stats")]
    public float health = 100f;
    public float maxHealth = 100f;
    public bool isDead = false;
    public bool isPlayer = false;
    
    [Header("Visual Settings")]
    public Color rapperColor = Color.white;
    public GameObject healthBarPrefab;
    private Transform healthBar;
    private Transform healthBarFill;
    
    // Weapon handling
    private Weapon currentWeapon;
    private bool hasWeapon = false;
    
    // AI behavior
    private Vector3 targetPosition;
    private float directionChangeInterval = 3f;
    private float nextDirectionChange;
    
    // Components
    private Rigidbody rb;
    private Renderer rapperRenderer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rapperRenderer = GetComponent<Renderer>();
        
        // Apply rapper color
        if (rapperRenderer != null)
        {
            rapperRenderer.material.color = rapperColor;
        }
        
        SetNewRandomDirection();
        nextDirectionChange = Time.time + directionChangeInterval;
        
        // Create health bar if prefab is set
        if (healthBarPrefab != null)
        {
            GameObject healthBarObj = Instantiate(healthBarPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            healthBarObj.transform.SetParent(transform);
            
            healthBar = healthBarObj.transform;
            healthBarFill = healthBar.Find("Fill");
            
            UpdateHealthBar();
        }
    }
    
    void Update()
    {
        if (isDead) return;
        
        if (isPlayer)
        {
            HandlePlayerInput();
        }
        else
        {
            HandleAIMovement();
        }
        
        // Handle weapon actions
        if (hasWeapon)
        {
            if (isPlayer)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentWeapon.Use();
                }
            }
            else
            {
                // AI weapon usage
                TryUseWeapon();
            }
        }
        
        // Update health bar position
        if (healthBar != null)
        {
            // Health bar should always face camera
            healthBar.LookAt(Camera.main.transform);
            healthBar.Rotate(0, 180, 0);
        }
    }
    
    private void HandlePlayerInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movement),
                rotationSpeed * Time.deltaTime
            );
        }
    }
    
    private void HandleAIMovement()
    {
        if (Time.time >= nextDirectionChange)
        {
            SetNewRandomDirection();
            nextDirectionChange = Time.time + directionChangeInterval;
        }
        
        // Move towards target
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            movementSpeed * Time.deltaTime
        );
        
        // Rotate towards movement direction
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
    
    private void SetNewRandomDirection()
    {
        // Get arena size from game manager if available
        float arenaSize = 20f;
        if (GameManager.Instance != null)
        {
            arenaSize = GameManager.Instance.arenaSize;
        }
        
        float randomX = Random.Range(-arenaSize, arenaSize);
        float randomZ = Random.Range(-arenaSize, arenaSize);
        targetPosition = new Vector3(randomX, 0f, randomZ);
    }
    
    private void TryUseWeapon()
    {
        // Simple AI logic for weapon usage
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider col in nearbyObjects)
        {
            RapperController otherRapper = col.GetComponent<RapperController>();
            if (otherRapper != null && otherRapper != this && !otherRapper.isDead)
            {
                // Face the target
                Vector3 directionToTarget = (otherRapper.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(directionToTarget);
                
                // Use weapon
                currentWeapon.Use();
                break;
            }
        }
    }
    
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        
        health -= damage;
        UpdateHealthBar();
        
        if (health <= 0)
        {
            Die();
        }
    }
    
    public void Heal(float amount)
    {
        if (isDead) return;
        
        health = Mathf.Min(health + amount, maxHealth);
        UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            float fillAmount = health / maxHealth;
            healthBarFill.localScale = new Vector3(fillAmount, 1, 1);
            
            // Change color based on health percentage
            Renderer fillRenderer = healthBarFill.GetComponent<Renderer>();
            if (fillRenderer != null)
            {
                Color healthColor = Color.Lerp(Color.red, Color.green, fillAmount);
                fillRenderer.material.color = healthColor;
            }
        }
    }
    
    private void Die()
    {
        isDead = true;
        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        
        // Drop weapon if holding one
        if (hasWeapon)
        {
            DropWeapon();
        }
        
        // Notify GameManager
        GameManager.Instance.RapperDied(this);
        
        // Visual feedback
        transform.Rotate(90f, 0f, 0f);
        
        // Hide health bar
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(false);
        }
    }
    
    public void PickupWeapon(Weapon weapon)
    {
        if (hasWeapon)
        {
            DropWeapon();
        }
        
        currentWeapon = weapon;
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = new Vector3(0.5f, 0f, 0.5f);
        weapon.transform.localRotation = Quaternion.identity;
        weapon.SetOwner(this);
        hasWeapon = true;
    }
    
    public void DropWeapon()
    {
        if (!hasWeapon) return;
        
        currentWeapon.transform.SetParent(null);
        currentWeapon.Drop();
        currentWeapon = null;
        hasWeapon = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (isDead) return;
        
        // Check for weapon pickups
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon != null && !weapon.HasOwner)
        {
            PickupWeapon(weapon);
        }
        
        // Apple collectibles are handled in their own script
    }
}