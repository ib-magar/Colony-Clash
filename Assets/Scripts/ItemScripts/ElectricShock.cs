
using UnityEngine;

public class ElectricShock : MonoBehaviour
{
    public LayerMask targetLayer;
    public float searchRange = 10f;
    public float damageAmount;
    private void Start()
    {
        // Get the current position on the x-axis
        float currentPosition = transform.position.x;

        // Define the search boundaries
        float minX = currentPosition - searchRange;
        float maxX = currentPosition + searchRange;

        // Find objects within the specified range on the x-axis
        Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(minX, transform.position.y),
                                                          new Vector2(maxX, transform.position.y),
                                                          targetLayer);

        // Iterate through the detected colliders
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider has the LivingEntity component
            LivingEntity livingEntity = collider.GetComponent<LivingEntity>();
            if (livingEntity != null)
            {
                // Call the Damage function
                livingEntity.takeDamage(damageAmount);
            }
        }

        Destroy(gameObject, .3f);
    }
}
