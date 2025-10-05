using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Armas (referencias opcionales si no están como hijos)")]
    [SerializeField] private GameObject espadaFallback;
    [SerializeField] private GameObject shurikenFallback;
    [SerializeField] private GameObject bolaDeFuegoFallback;

    [Header("Combate")]
    [SerializeField] private float fireRate = 0.4f;
    [SerializeField] private bool debugMode = true;

    private float fireTimer;
    private WeaponBase currentWeapon;
    private GameObject armaActiva;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        WeaponBase[] weapons = GetComponentsInChildren<WeaponBase>(true);
        if (debugMode)
            Debug.Log($"PlayerManager: encontradas {weapons.Length} armas en hijos.");

        // Desactivar todas las armas primero
        foreach (var w in weapons)
            w.gameObject.SetActive(false);

        // Leer el arma seleccionada desde PlayerPrefs
        int weaponIndex = PlayerPrefs.GetInt("SelectedWeapon", 0);
        if (debugMode)
            Debug.Log($"PlayerManager: SelectedWeapon desde PlayerPrefs = {weaponIndex}");

        // Elegir el arma correcta según el índice
        WeaponBase chosen = null;
        switch (weaponIndex)
        {
            case 0: // Espada
                chosen = BuscarArmaPorNombre(weapons, "espada") ??
                         espadaFallback?.GetComponentInChildren<WeaponBase>(true);
                break;

            case 1: // Shuriken
                chosen = BuscarArmaPorNombre(weapons, "shuriken") ??
                         shurikenFallback?.GetComponentInChildren<WeaponBase>(true);
                break;

            case 2: // Bola de fuego
                chosen = BuscarArmaPorNombre(weapons, "bola") ??
                         BuscarArmaPorNombre(weapons, "fuego") ??
                         bolaDeFuegoFallback?.GetComponentInChildren<WeaponBase>(true);
                break;
        }

        if (chosen == null && weapons.Length > 0)
            chosen = weapons[0];

        if (chosen != null)
        {
            armaActiva = chosen.gameObject;
            armaActiva.SetActive(true);
            currentWeapon = chosen;
            currentWeapon.SetPlayer(transform);

            if (debugMode)
                Debug.Log($"PlayerManager: arma activada -> {armaActiva.name}");
        }
        else
        {
            Debug.LogError("PlayerManager: No se encontró ninguna arma. Revisa tus hijos o nombres.");
        }
    }

    private WeaponBase BuscarArmaPorNombre(WeaponBase[] weapons, string palabra)
    {
        foreach (var w in weapons)
        {
            if (w == null) continue;
            string n = w.gameObject.name.ToLower();
            if (n.Contains(palabra.ToLower()))
                return w;
        }
        return null;
    }

    private void OnEnable() => ImputSystem.OnMove += Movement2D;
    private void OnDisable() => ImputSystem.OnMove -= Movement2D;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate && currentWeapon != null)
        {
            fireTimer = 0f;

            if (currentWeapon is Shuriken s)
                s.BuscarEnemigo();
            else if (currentWeapon is BolaDeFuego b)
                b.BuscarEnemigo();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }
    private void Movement2D(Vector2 dir) => movement = dir;
}
