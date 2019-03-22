using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBuffBase : System.Object
{
    public string Name = "buff";

    [HideInInspector]public PlayerBuffManager BuffManager;

    public CDBase MaxTime = new CDBase(5.0f); 

    public PlayerBuffBase() { }
    public PlayerBuffBase(string name, float maxTime) { Name = name; MaxTime.CDTime = maxTime; }

    public void StartBefore(BuffAttributesData buffAttributes)
    {
        BuffStart(buffAttributes);
        MaxTime.OnTimeOut += EndBefore;
        MaxTime.Start();
    }

    private void EndBefore()
    {
        BuffManager.RemoveBuff(Name);
        
    }

    public virtual void BuffStart(BuffAttributesData buffAttributes) { }

    public virtual void BuffEnd(BuffAttributesData buffAttributes) { }




    public virtual void Update() { }

    public virtual PlayerBuffBase Copy()
    {
        PlayerBuffBase copy = new PlayerBuffBase(Name,MaxTime.CDTime);
        copy.BuffManager = BuffManager;
        return copy;
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collision) { }
    public virtual void OnTriggerExit2D(Collider2D collision) { }
    public virtual void OnTriggerStay2D(Collider2D collision) { }
    public virtual void OnCollisionEnter2D(Collision2D collision) { }
    public virtual void OnCollisionExit2D(Collision2D collision) { }
    public virtual void OnCollisionStay2D(Collision2D collision) { }

}
