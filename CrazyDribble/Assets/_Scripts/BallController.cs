using System.Runtime.CompilerServices;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxHorizontalForce = 5f;
    public float verticalForce = 7f;
    public float gravityIncrease = 2f;

    private Rigidbody2D rb;
    private float screenWidth;
    private CircleCollider2D ballCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (ballCollider.OverlapPoint(touchPos))
            {
                Vector2 ballPos = transform.position;

                float distanceFromCenter = Mathf.Clamp01(Mathf.Abs(touchPos.x - ballPos.x) / ballCollider.radius);

                float direction = (touchPos.x < ballPos.x) ? 1f : -1f;

                float xForce = direction * maxHorizontalForce * distanceFromCenter;
                float yForce = verticalForce + (1 - distanceFromCenter) * verticalForce * 0.3f;

                rb.linearVelocity = Vector2.zero;
                rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            }
        }
    }
}
