using SDG.Unturned;
using System.Collections;
using UnityEngine;



[DisallowMultipleComponent]
public class RaycastComponent : MonoBehaviour
{
    public void Awake()
    {
        base.StartCoroutine(RedoSphere());
        base.StartCoroutine(CalcSphere());
    }

    public IEnumerator CalcSphere()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(0.1f);
            bool flag = Sphere;
            if (flag)
            {
                Rigidbody rb = base.gameObject.GetComponent<Rigidbody>();
                bool flag2 = rb;
                if (flag2)
                {
                    float sizeBias = 1f - Provider.ping * rb.velocity.magnitude * 2f;
                    Sphere.transform.localScale = new Vector3(sizeBias, sizeBias, sizeBias);
                }
                rb = null;
            }
        }
    }
    public IEnumerator RedoSphere()
    {
        for (; ; )
        {
            GameObject tmp = Sphere;
            Sphere = IcoSphere.Create("HitSphere", SphereOptions.SpherePrediction ? 15.5f : SphereOptions.SphereRadius, SphereOptions.RecursionLevel);
            Sphere.layer = LayerMasks.AGENT;
            Sphere.transform.parent = base.transform;
            Sphere.transform.localPosition = Vector3.zero;
            UnityEngine.Object.Destroy(tmp);
            yield return new WaitForSeconds(1f);
            tmp = null;
        }
    }

    public GameObject Sphere;
}

