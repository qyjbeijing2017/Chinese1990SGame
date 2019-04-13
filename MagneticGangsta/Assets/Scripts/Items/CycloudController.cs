using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloudController : MonoBehaviour
{

    [SerializeField] CyloudMove Cycloud;

 
    [SerializeField] List<CycloudTargetPosition> targetPositions = new List<CycloudTargetPosition>();
    [SerializeField] CDBase CreatCd = new CDBase(3.0f);



    // Start is called before the first frame update
    void Start()
    {
        CreatCd.OnTimeOut += OnCreatCycloud;
        CreatCd.Start();
    }

    void OnCreatCycloud()
    {
        int targetIndex = Random.Range(0, targetPositions.Count);

        CyloudMove cyloud = Instantiate(Cycloud);
        cyloud.TargetPosition = targetPositions[targetIndex];
        cyloud.OnDestory += OnCycloudDestory;
    }
    void OnCycloudDestory()
    {
        CreatCd.Start();
    }

}

[System.Serializable]
public class CycloudTargetPosition
{
    public Transform StartTransform;
    public Transform EndTransform;

    public Vector2 StartPositon { get { return StartTransform.position; } }
    public Vector2 EndPosition { get { return EndTransform.position; } }

    public Vector2 Start2End { get { return EndPosition - StartPositon; } }

}