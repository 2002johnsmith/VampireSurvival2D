using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Search;
using Unity.VisualScripting;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour , IDamageable
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 5f;
    private Vector2 lastMovementDirection = Vector2.right;
    private Rigidbody2D rb;
    private Vector2 movement;
    //last


    //armas
    [SerializeField] private List<WeaponBase> Weapons = new();
    [SerializeField] private int MaxWeapons = 6;
    //stads
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float RangePick = 2f;

    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int currentExp = 0;
    [SerializeField] private int ExpTonexLevel = 100;

  [SerializeField]  private ExperienceManager expManager;
    [SerializeField]private UIManager uiManager;
    private WeaponBase currentWeapon;
    private GameObject armaActiva;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        InitializeWeapons();
    }
    private void InitializeWeapons()
    {
        // Limpiar armas existentes
        Weapons.Clear();

        // Leer arma seleccionada desde PlayerPrefs
        int weaponIndex = PlayerPrefs.GetInt("SelectedWeapon", 0);

        // Crear arma inicial mediante Factory
        WeaponData_So initialWeaponData = GetWeaponDataByIndex(weaponIndex);//facrtory
        if (initialWeaponData != null)
        {
            AddWeapon(initialWeaponData);
        }
    }
    public void AddWeapon(WeaponData_So weaponData)
    {
        if (Weapons.Count >= MaxWeapons)
        {
            Debug.Log("Límite de armas alcanzado");
            return;
        }

        WeaponBase newWeapon = WeaponFactory.CreateWeapon(weaponData, transform);
        if (newWeapon != null)
        {
            Weapons.Add(newWeapon);
            newWeapon.SetPlayer(transform);

        }
    }
    public void UpgradeWeapon(int weaponId, TypeUpgrade upgradeType, float value)
    {
        WeaponBase weapon = Weapons.Find(w => w.IdWeapon == weaponId);
        if (weapon != null)
        {
            // Aplicar mejora según el tipo
            switch (upgradeType)
            {
                case TypeUpgrade.Damage:
                    weapon.Damage += value;
                    break;
                case TypeUpgrade.Cooldown:
                    weapon.Cooldown = Mathf.Max(0.1f, weapon.Cooldown - value);
                    break;
                case TypeUpgrade.ProyectileCount:
                    weapon.count += (int)value;
                    break;
                case TypeUpgrade.ProyectileSize:
                    weapon.size += value;
                    break;
                case TypeUpgrade.ProyectileSpeed:
                    weapon.Speed += value;
                    break;
            }
        }
    }

    public void AddExperience(int expAmount)
    {
        currentExp += expAmount;
        while (currentExp >= ExpTonexLevel)
        {
            LevelUp();
        }

        // CORREGIR: Usar referencia
        if (expManager != null)
            expManager.UpdateExpBar(currentExp, ExpTonexLevel);
    }
    private void LevelUp()
    {
        currentLevel++;
        currentExp -= ExpTonexLevel;
        ExpTonexLevel = CalculateNextLevelExp(); // CORREGIR nombre método

        // CORREGIR: Usar referencia
        if (uiManager != null)
            uiManager.ShowLevelUpOptions(this);
    }
    private int CalculateNextLevelExp()
    {
        return Mathf.RoundToInt(100 * Mathf.Pow(currentLevel, 1.5f));
    }
    public Vector2 GetLastMovementDirection()
    {
        return movement != Vector2.zero ? movement.normalized : lastMovementDirection;
    }

    private void OnEnable()
    {
        ImputSystem.OnMove += Movement2D;
        ExperienceCollectible.OnExpCollected += AddExperience;
    }
    private void OnDisable()
    {
        ImputSystem.OnMove -= Movement2D;
        ExperienceCollectible.OnExpCollected -= AddExperience;
    }

    void Update()
    {
        // Actualizar todas las armas
        foreach (var weapon in Weapons)
        {
            weapon.WeaponShoot();
        }

        // Actualizar última dirección
        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement.normalized;
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }
    private WeaponData_So GetWeaponDataByIndex(int index)
    {
        string[] weaponNames = { "Biblia" };
        if (index < weaponNames.Length)
            return WeaponFactory.GetWeaponDataByName(weaponNames[index]);
        return null;
    }

    public void TakeDamage(float damage) // CORREGIR parámetro
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }

        // CORREGIR: Usar referencia
        if (uiManager != null)
            uiManager.UpdateHealthBar(currentHealth, maxHealth);
    }
    public void SetDamageable(float Health)
    {

    }
    private void Die()
    {
        Debug.Log("Player muerto");
        // Mostrar game over, reiniciar escena, etc.
        //  GameManager.Instance.GameOver();
    }
    private void Movement2D(Vector2 dir) => movement = dir;
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    
    public Vector2 GetPlayerPosition() => transform.position;
    public float GetPickupRange() => RangePick;
}
