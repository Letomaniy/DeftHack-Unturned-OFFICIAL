using UnityEngine;
[Component]
public class AimbotComponent : MonoBehaviour
{
    public void Start()
    {
        CoroutineComponent.LockCoroutine = base.StartCoroutine(AimbotCoroutines.SetLockedObject());
        CoroutineComponent.AimbotCoroutine = base.StartCoroutine(AimbotCoroutines.AimToObject());
        base.StartCoroutine(RaycastCoroutines.UpdateObjects());
    }
}

