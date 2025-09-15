using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 50;
    private int currentHealth;

    private Rigidbody2D rb;
    private Playercontroller playerController;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<Playercontroller>();
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage & knockback!");

        if (rb != null && playerController != null)
        {
            StartCoroutine(ApplyKnockback(knockbackDirection, knockbackForce));
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator ApplyKnockback(Vector2 direction, float force)
    {
        playerController.canMove = false;

        rb.linearVelocity = Vector2.zero; // stop current motion
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.3f); // knockback duration

        playerController.canMove = true;
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // Add respawn or death logic here
    }
}
