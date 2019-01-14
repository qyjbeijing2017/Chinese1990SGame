using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeBack : MonoBehaviour
{

    public int EdgeBackNum;
    public float BackSpeed;
    public int EdgeBackNumNow;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().velocity = -transform.position * BackSpeed;
            EdgeBackNumNow--;
            if (EdgeBackNumNow <=0)
            {
                gameObject.SetActive(false);
            }
            else
            {

                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (float)EdgeBackNumNow/(float)EdgeBackNum);
            }
        }
    }
}
