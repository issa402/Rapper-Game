using UnityEngine;

public class Knife : Weapon
{
    public float attackRange = 1.5f;
    
    public override void Use()
    {
        // Check for targets in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider col in hitColliders)
        {
            RapperController target = col.GetComponent<RapperController>();
            if (target != null && target != owner)
            {
                target.TakeDamage(damage);
                if (audioManager != null)
                {
                    audioManager.PlayHit();
                }
                break;
            }
        }
    }
}