




using UnityEngine;



[Component]
public class SpectatorComponent : MonoBehaviour
{
    public void FixedUpdate()
    {
        bool flag = !DrawUtilities.ShouldRun();
        if (!flag)
        {
            bool flag2 = MiscOptions.SpectatedPlayer != null && !PlayerCoroutines.IsSpying;
            if (flag2)
            {
                OptimizationVariables.MainPlayer.look.isOrbiting = true;
                OptimizationVariables.MainPlayer.look.orbitPosition = MiscOptions.SpectatedPlayer.transform.position - OptimizationVariables.MainPlayer.transform.position;
                OptimizationVariables.MainPlayer.look.orbitPosition += new Vector3(0f, 3f, 0f);
            }
            else
            {
                OptimizationVariables.MainPlayer.look.isOrbiting = MiscOptions.Freecam;
            }
        }
    }
}
