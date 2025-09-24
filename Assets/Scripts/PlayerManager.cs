using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 Movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        ImputSystem.OnMove += Movement2D;
    }
    private void OnDisable()
    {
        ImputSystem.OnMove -= Movement2D;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity= new Vector2 (Movement.x,Movement.y);
    }
    private void Movement2D(Vector2 direction)
    {
        Movement = direction;
    }
}
