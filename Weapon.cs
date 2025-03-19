using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage = 20f;
    protected RapperController owner;
    public bool HasOwner => owner != null;
    
    protected AudioManager audioManager;
    
    protected virtual void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    public abstract void Use();
    
    public void SetOwner(RapperController newOwner)
    {
        owner = newOwner;
        if (audioManager != null)
        {
            audioManager.PlayWeaponPickup();
        }
    }
    
    public void Drop()
    {
        owner = null;
        // Add random rotation when dropped
        float randomRotation = Random.Range(0f, 360f);
        transform.Rotate(0f, randomRotation, 0f);
    }
    
    protected bool IsInRange(Vector3 target, float range)
    {
        return Vector3.Distance(transform.position, target) <= range;
    }
}