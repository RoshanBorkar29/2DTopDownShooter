using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    public Animator anim;
    public bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
               anim = GetComponent<Animator>();

    }
    void Update()
    {

    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        // if (anim != null)
        // {
        //     anim.SetTrigger("die");
        // }
         Destroy(gameObject, 1.0f);
    }
}