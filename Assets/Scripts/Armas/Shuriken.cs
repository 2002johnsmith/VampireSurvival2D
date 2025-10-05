using UnityEngine;

public class Shuriken : WeaponBase, IReusableProjectile
{
    [SerializeField] private float rango = 2f;
    [SerializeField] private float velocidadLanzamiento = 10f;
    [SerializeField] private bool debugMode = false;

    private Transform objetivo;
    private bool enMovimiento = false;

    void Update()
    {
        if (enMovimiento)
            MoverHaciaObjetivo();
    }

    public void BuscarEnemigo()
    {
        if (EnemyManager.Instance == null)
            return;

        if (enMovimiento) return;
        if (player == null) return;

        float menorDistancia = Mathf.Infinity;
        Transform enemigoCercano = null;

        foreach (Transform enemigo in EnemyManager.Instance.enemigos)
        {
            if (enemigo == null) continue;
            if (!enemigo.gameObject.activeInHierarchy) continue;

            float dist = Vector2.Distance(player.position, enemigo.position);
            if (dist < menorDistancia && dist <= rango)
            {
                menorDistancia = dist;
                enemigoCercano = enemigo;
            }
        }

        if (enemigoCercano != null)
        {
            objetivo = enemigoCercano;
            enMovimiento = true;
            transform.position = player.position;
            gameObject.SetActive(true);
        }
    }

    private void MoverHaciaObjetivo()
    {
        if (objetivo == null)
        {
            ResetProjectile();
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            objetivo.position,
            velocidadLanzamiento * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, objetivo.position) < 0.1f)
        {
            if (debugMode) Debug.Log($"{gameObject.name} impactó a {objetivo.name}");

            if (objetivo != null)
                Object.Destroy(objetivo.gameObject);

            ResetProjectile();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag("Enemy"))
        {
            Object.Destroy(other.gameObject);
            ResetProjectile();
        }
    }

    public void ResetProjectile()
    {
        enMovimiento = false;
        objetivo = null;
        gameObject.SetActive(false);
    }
}
