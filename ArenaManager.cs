using UnityEngine;
using System.Collections;

public class ArenaManager : MonoBehaviour
{
    [Header("Arena Settings")]
    public Transform arenaBorder;
    public Color borderColor = new Color(0.5f, 0f, 1f); // Purple color in reference image
    public float initialSize = 20f;
    public float shrinkInterval = 30f;
    public float shrinkAmount = 0.1f;
    public float damageOutsideArena = 10f;
    public float damageInterval = 1f;
    
    [Header("Effects")]
    public GameObject shrinkWarningEffect;
    public float warningDuration = 3f;
    
    private float currentSize;
    private Vector3 originalScale;
    private bool isActive = true;
    
    void Start()
    {
        if (arenaBorder != null)
        {
            originalScale = arenaBorder.localScale;
            
            // Set border color
            Renderer[] renderers = arenaBorder.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = borderColor;
            }
        }
        
        currentSize = initialSize;
        
        // Start arena shrinking
        StartCoroutine(ShrinkArenaRoutine());
        
        // Start damage outside arena check
        StartCoroutine(DamageOutsideArenaRoutine());
    }
    
    private IEnumerator ShrinkArenaRoutine()
    {
        while (isActive && arenaBorder != null)
        {
            yield return new WaitForSeconds(shrinkInterval);
            
            // Show warning
            if (shrinkWarningEffect != null)
            {
                shrinkWarningEffect.SetActive(true);
                yield return new WaitForSeconds(warningDuration);
                shrinkWarningEffect.SetActive(false);
            }
            
            // Shrink arena
            float newSize = currentSize * (1f - shrinkAmount);
            
            // Animate the shrinking
            float elapsed = 0f;
            float duration = 2f;
            
            Vector3 startScale = arenaBorder.localScale;
            Vector3 targetScale = originalScale * (newSize / initialSize);
            
            while (elapsed < duration)
            {
                arenaBorder.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            // Ensure we reach the exact target
            arenaBorder.localScale = targetScale;
            currentSize = newSize;
            
            // Play sound effect
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlayArenaShrink();
            }
        }
    }
    
    private IEnumerator DamageOutsideArenaRoutine()
    {
        while (isActive)
        {
            // Find all rappers
            RapperController[] rappers = FindObjectsOfType<RapperController>();
            
            foreach (RapperController rapper in rappers)
            {
                if (!rapper.isDead && !IsInArena(rapper.transform.position))
                {
                    rapper.TakeDamage(damageOutsideArena);
                }
            }
            
            yield return new WaitForSeconds(damageInterval);
        }
    }
    
    public bool IsInArena(Vector3 position)
    {
        // Check if position is within current arena bounds
        return Mathf.Abs(position.x) <= currentSize && Mathf.Abs(position.z) <= currentSize;
    }
    
    public float GetCurrentArenaSize()
    {
        return currentSize;
    }
    
    // Visualize arena in editor
    void OnDrawGizmos()
    {
        Gizmos.color = borderColor;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(initialSize * 2, 1, initialSize * 2));
    }
}