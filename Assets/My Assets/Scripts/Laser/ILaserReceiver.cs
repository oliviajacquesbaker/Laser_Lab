using UnityEngine;

public interface ILaserReceiver : ILaserTarget
{
    bool IsLaserConditionSatisfied();
    void ResetLaserCondition();
}
