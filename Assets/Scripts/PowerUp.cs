using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Health, Shield, FireRate }
    public PowerUpType type;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            switch (type)
            {
                case PowerUpType.Health:
                    player.Heal(30);
                    break;
                case PowerUpType.Shield:
                    player.ActivateShield(5f);
                    break;
                case PowerUpType.FireRate:
                    player.BoostFireRate(5f);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
