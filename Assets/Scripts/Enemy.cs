using Mono.Cecil.Cil;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Enemy : MonoBehaviour , IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private ExperienceCollectible expPrefab;
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    public void TakeDamage(float Damage)
    {
        if (currentHealth- Damage < 0)
        {
            //die
            Debug.Log("mori");
            Die();
        }
        else
        {
            currentHealth -= Damage;
            Debug.Log("recibi" + Damage);
        }
    }
    public void SetDamageable(float Health)
    {

    }
    private void Die()
    {
        // Soltar experiencia
        ExperienceCollectible exp = Instantiate(expPrefab, transform.position, Quaternion.identity);
        //enabled = false;
        EnemyManager.Instance?.EliminarEnemigo(this.transform);
        Destroy(gameObject);
    }
    private void OnEnable()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.RegistrarEnemigo(transform);
        else
            StartCoroutine(RegisterNextFrame());
    }

    private IEnumerator RegisterNextFrame()
    {
        yield return null; 
        EnemyManager.Instance?.RegistrarEnemigo(transform);
    }

    private void OnDisable()
    {
        
    }
}
