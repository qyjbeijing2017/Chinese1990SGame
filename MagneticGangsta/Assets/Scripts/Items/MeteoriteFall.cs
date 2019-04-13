using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeteoriteFall : PlayerFunctionBase
{
    public override string Name { get { return "MeteoriteFall"; } }
    public Vector2 TargetPoint;

    public float Speed = 1f;

    public float SpeedAdd = 3f;

    public event UnityAction OnBoom;

    private bool isBoomed = false;
    private Vector2 Pos2Target
    {
        get
        {
            return TargetPoint - (Vector2)transform.position;
        }
    }

    [SerializeField]private Collider2D collider;

    public override void PlayerInit()
    {
        if (collider == null)
        {
            collider = GetComponent<Collider2D>();
        }
        transform.up = Pos2Target;
        collider.isTrigger = true;
    }

    public override void PlayerLoop()
    {
        
        
    }

    private void FixedUpdate()
    {
        if (!isBoomed)
        {
            Speed += SpeedAdd * Time.fixedDeltaTime;
            PlayerRigidbody2D.velocity = (Vector3)Pos2Target.normalized * Speed;
            transform.up = PlayerRigidbody2D.velocity;
        }
        else
        {
            PlayerRigidbody2D.velocity = Vector3.zero;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && collider.IsTouching(collision))
        {
            OnBoom?.Invoke();
            isBoomed = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
