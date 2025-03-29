using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IProgressiveWork : IWorkplace
{
    public float requiredWorkAmount
    {
        get;
    }
    public float workedAmount
    {
        get; set;
    }
    public virtual void DoWork(Humanoid worker, float workAmount)
    {
        workedAmount += workAmount;
        if (workedAmount >= requiredWorkAmount)
        {
            OnWorkFinish(worker);
            workedAmount = 0.0f;
        }
    }
    public void OnWorkFinish(Humanoid worker);
}
