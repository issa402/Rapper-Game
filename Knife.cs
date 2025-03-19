using UnityEngine;
using System.Collections;

public class Knife : Weapon
{
    public float attackRange = 1.5f;
    public float attackCooldown = 0.5f;
    public float attackDuration = 0.25f;
    
    [Header("Visual Effects")]
    public GameObject slashEffectPrefab;
    public Color knifeColor = new Color(0.7f, 0.7f, 0.7f); // Silver color
    
    private bool isAttacking = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Renderer knifeRenderer;
    
    protected override void Start()
    {
        base.Start();
        
        knifeRenderer = GetComponent<Renderer>();
        if (knifeRenderer != null)
        {
            knifeRenderer.material.color = knifeColor;
        }
        
        // Save original position/rotation for animation purposes
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }
    
    public override void Use()
    {
        if (!isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }
    
    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        
        // Save current position/rotation
        Vector3 startPos = transform.localPosition;
        Quaternion startRot = transform.localRotation;
        
        // Animate the knife swing
        float elapsed = 0f;
        while (elapsed < attackDuration)
        {
            float t = elapsed / attackDuration;
            
            // Move the knife forward and rotate it
            transform.localPosition = startPos + Vector3.forward * t * 0.5f;
            transform.localRotation = startRot * Quaternion.Euler(0, 0, -90f * t);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // Check for targets in range
        if (owner != null)
        {
            Vector3 attackPos = owner.transform.position + owner.transform.forward * attackRange;
            Collider[] hitColliders = Physics.OverlapSphere(attackPos, attackRange / 2f);
            
            bool hitSomething = false;
            
            foreach (Collider col in hitColliders)
            {
                RapperController target = col.GetComponent<RapperController>();
                if (target != null && target != owner)
                {
                    // Deal damage
                    target.TakeDamage(damage);
                    hitSomething = true;
                    
                    // Play hit sound
                    if (audioManager != null)
                    {
                        audioManager.PlayHit();
                    }
                    
                    // Create slash effect
                    if (slashEffectPrefab != null)
                    {
                        Instantiate(slashEffectPrefab, target.transform.position, Quaternion.identity);
                    }
                    
                    break;
                }
            }
            
            // If we didn't hit anything, just play the knife whoosh sound
            if (!hitSomething && audioManager != null)
            {
                // Play swing sound (if implemented in AudioManager)
            }
        }
        
        // Reset position and rotation
        transform.localPosition = startPos;
        transform.localRotation = startRot;
        
        // Cooldown
        yield return new WaitForSeconds(attackCooldown - attackDuration);
        
        isAttacking = false;
    }
    
    // Visualize attack range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, attackRange / 2f);
    }
}