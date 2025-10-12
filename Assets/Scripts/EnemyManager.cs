using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<Transform> enemigos = new List<Transform>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegistrarEnemigo(Transform enemigo)
    {
        if (!enemigos.Contains(enemigo))
            enemigos.Add(enemigo);
    }

    public void EliminarEnemigo(Transform enemigo)
    {
        enemigos.Remove(enemigo);
    }
    public Transform MoreClose(Transform Entity, float Range = 999 )//gpt
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform enemigo in enemigos)
        {
            if (enemigo == null || !enemigo.gameObject.activeInHierarchy)
                continue;

            float dist = Vector2.Distance(Entity.position, enemigo.position);
            if (dist < closestDistance && dist <= Range)
            {
                closestDistance = dist;
                closest = enemigo;
            }
        }
        return closest;
    }

    public Transform GetRandomEnemy()//gpt
    {
        if (enemigos.Count == 0) return null;

        var activeEnemies = enemigos.Where(e => e != null && e.gameObject.activeInHierarchy).ToList();
        if (activeEnemies.Count == 0) return null;

        return activeEnemies[Random.Range(0, activeEnemies.Count)];
    }
}
