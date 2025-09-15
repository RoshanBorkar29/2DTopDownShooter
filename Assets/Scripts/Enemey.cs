using UnityEngine;

public class Enemey : MonoBehaviour
{
    public float speed = 5f;
    public Transform player;

    public GameObject deathEffectPrefab;
      void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
           
                  Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Bullet"))
    {
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    else if (other.CompareTag("Player"))
    {
        other.GetComponent<PlayerMovement>().TakeDamage(20);
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}


}
