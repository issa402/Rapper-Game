using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform fillTransform;
    public SpriteRenderer backgroundRenderer;
    public SpriteRenderer fillRenderer;
    
    // Called by the RapperController to update the health display
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float healthPercent = Mathf.Clamp01(currentHealth / maxHealth);
        
        // Update the fill amount
        if (fillTransform != null)
        {
            fillTransform.localScale = new Vector3(healthPercent, 1, 1);
        }
        
        // Update the color based on health percentage
        if (fillRenderer != null)
        {
            fillRenderer.color = Color.Lerp(Color.red, Color.green, healthPercent);
        }
    }
    
    // Always face the camera
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0); // Flip to face camera correctly
        }
    }
}