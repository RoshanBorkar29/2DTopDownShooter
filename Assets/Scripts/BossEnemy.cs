using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;

    public int health = 50;
    private float fireTimer = 0f;

    void Start()
    {
        // Optional: Move boss to center top
        transform.position = new Vector2(0, 3.5f); // Adjust Y position as needed
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            FireAtPlayer();
        }
    }

    void FireAtPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) return;

        Vector3 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(10); // Bullet damage value
            Destroy(other.gameObject);
        }
    }
}
