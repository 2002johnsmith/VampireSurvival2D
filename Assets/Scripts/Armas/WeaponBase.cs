using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Atributos del arma")]
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float speed = 5f;

    protected Transform player;

    protected virtual void Start()
    {
        if (player == null && transform.parent != null)
            player = transform.parent;
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    public virtual void UpgradeDamage(float amount) => damage += amount;
    public virtual void UpgradeSpeed(float amount) => speed += amount;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"{gameObject.name} golpeó a {other.name} causando {damage} de daño");
        }
    }
}
