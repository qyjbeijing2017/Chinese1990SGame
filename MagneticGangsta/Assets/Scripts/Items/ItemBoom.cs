using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemBoom : PlayerFunctionBase
{

    public event UnityAction OnCreatItem;

    [SerializeField] private Animator animator;
    
    // Start is called before the first frame update
    public override void PlayerInit()
    {
        if (animator == null)
        {
            animator.GetComponent<Animation>();
        }
    }

    public void BoomStart()
    {

    }

    public void CreatItem()
    {
        OnCreatItem?.Invoke();
    }

    public void BoomEnd()
    {
        Destroy(gameObject);
    }

}
