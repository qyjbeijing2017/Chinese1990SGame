using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayerBuffBase : System.Object
{
    public string Name = "buff";

    public float MaxTime = 0.0f;

    [HideInInspector]public PlayerBuffManager BuffManager;

    protected CDBase buffcd; 

    public PlayerBuffBase() { }
    public PlayerBuffBase(string name, float maxTime) { Name = name; MaxTime = maxTime; }

    public void StartBefore()
    {
        buffcd = new CDBase(MaxTime);
        buffcd.OnTimeOut += EndBefore;
        BuffStart();
    }

    private void EndBefore()
    {
        BuffManager.RemoveBuff(Name);
    }

    public virtual void BuffStart() { }

    public virtual void BuffEnd() { }

    public virtual void Update() { }

    public abstract PlayerBuffBase Copy();

    public virtual void OnTriggerEnter2D(Collider2D collision) { }
    public virtual void OnTriggerExit2D(Collider2D collision) { }
    public virtual void OnTriggerStay2D(Collider2D collision) { }
    public virtual void OnCollisionEnter2D(Collision2D collision) { }
    public virtual void OnCollisionExit2D(Collision2D collision) { }
    public virtual void OnCollisionStay2D(Collision2D collision) { }

}
