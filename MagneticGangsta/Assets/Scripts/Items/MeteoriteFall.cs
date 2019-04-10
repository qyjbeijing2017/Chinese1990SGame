using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteFall : PlayerFunctionBase
{
    public Vector2 TargetPoint;

    public float Speed = 1f;

    public float SpeedAdd = 3f;

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
        transform.forward = Pos2Target;
        collider.isTrigger = true;
    }

    public override void PlayerLoop()
    {
        Speed += SpeedAdd * Time.deltaTime;
        transform.position += (Vector3)Pos2Target.normalized * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }
}
