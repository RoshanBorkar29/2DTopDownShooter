using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.25f;
    private float nextFireTime = 0f;

    public int maxHealth = 100;
    private int currentHealth;
void Start()
{
    currentHealth = maxHealth;
    GameManager.instance.UpdateHealthBar(currentHealth, maxHealth);
}

    void Update()
    {
        Move();

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Transform nearestEnemy = FindNearestEnemy();
                if (nearestEnemy != null)
                {
                    Vector2 dir = nearestEnemy.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                }

            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
       

    }
    Transform FindNearestEnemy()
{
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    Transform nearest = null;
    float minDist = Mathf.Infinity;

    foreach (GameObject enemy in enemies)
    {
        float dist = Vector2.Distance(transform.position, enemy.transform.position);
        if (dist < minDist)
        {
            minDist = dist;
            nearest = enemy.transform;
        }
    }

    return nearest;
}
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
        GameManager.instance.UpdateHealthBar(currentHealth, maxHealth);
    }
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        GameManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        
    }
    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldRoutine(duration));
    }
    IEnumerator ShieldRoutine(float duration)
    {
        GameManager.instance.ShowPowerUpTimer("Shield", duration);
            yield return new WaitForSeconds(duration);
    }
    public void BoostFireRate(float duration)
    {
          StartCoroutine(FireRateRoutine(duration));
    }
    IEnumerator FireRateRoutine(float duration)
    {
        float originalFireRate = fireRate;
        fireRate /= 2;

        GameManager.instance.ShowPowerUpTimer("Fire Rate", duration);

        yield return new WaitForSeconds(duration);

        fireRate = originalFireRate;
    }
void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("EnemyBullet"))
    {
        TakeDamage(10); // or any damage
        Destroy(other.gameObject);
    }
}

}
