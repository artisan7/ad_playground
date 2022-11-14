using System;
using UnityEngine;

public class VolleyBall : MonoBehaviour
{
    public float volleyForce;
    Rigidbody2D _body;

    public static event Action OnClickToPlay;
    public static event Action OnVolley;
    public static event Action OnFallToBottom;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // bounce on hitting edge of screen
        float ballRadius = GetComponent<CircleCollider2D>().radius * 0.1f;
        float leftBorder = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        float rightBorder = Camera.main.ScreenToWorldPoint(Vector2.one * Screen.width).x;
        float bottomBorder = Camera.main.ScreenToWorldPoint(Vector2.zero).y;

        // test left border
        if (transform.position.x - ballRadius <= leftBorder && _body.velocity.x < 0)
            _body.velocity = _body.velocity * Vector2.left;
        // test right border
        else if (transform.position.x + ballRadius >= rightBorder && _body.velocity.x > 0)
            _body.velocity = _body.velocity * Vector2.left;

        // test bottom border
        if (transform.position.y <= bottomBorder && Time.timeScale != 0)
            OnFallToBottom?.Invoke();
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0)
            OnClickToPlay?.Invoke();

        float xdirection = transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        Vector2 direction = new Vector2(xdirection, 0.5f).normalized;

        _body.velocity = Vector2.zero;
        _body.AddForce(direction * volleyForce, ForceMode2D.Impulse);

        OnVolley?.Invoke();
    }
}
