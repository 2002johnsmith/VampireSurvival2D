using UnityEngine;

public class Espada : WeaponBase
{
    [SerializeField] private float radio = 1.0f; 
    private float anguloActual = 0f;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (player == null) return;

        anguloActual += speed * Time.deltaTime;

        float x = player.position.x + Mathf.Cos(anguloActual) * radio;
        float y = player.position.y + Mathf.Sin(anguloActual) * radio;

        transform.position = new Vector3(x, y, 0);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other); 

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
