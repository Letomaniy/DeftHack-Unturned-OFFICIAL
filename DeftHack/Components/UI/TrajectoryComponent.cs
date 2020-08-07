using HighlightingSystem;
using SDG.Unturned;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;



 
[Component]
public class TrajectoryComponent : MonoBehaviour
{  
    public static Highlighter Highlighted { get; private set; }
     
    [Initializer]
    public static void Initialize()
    {
        ColorUtilities.addColor(new ColorVariable("_TrajectoryPredictionInRange", "B.D. Predict (In Range)", Color.cyan, true));
        ColorUtilities.addColor(new ColorVariable("_TrajectoryPredictionOutOfRange", "B.D. Predict (Out of Range)", Color.red, true));
    }
     
    public void OnGUI()
    {
        Player mainPlayer = OptimizationVariables.MainPlayer;
        object obj;
        if (mainPlayer == null)
        {
            obj = null;
        }
        else
        {
            PlayerEquipment equipment = mainPlayer.equipment;
            obj = ((equipment != null) ? equipment.useable : null);
        }
        UseableGun useableGun = obj as UseableGun;
        bool flag = useableGun == null || TrajectoryComponent.spying || !WeaponOptions.EnableBulletDropPrediction || !Provider.modeConfigData.Gameplay.Ballistics;
        if (flag)
        {
            bool flag2 = TrajectoryComponent.Highlighted != null;
            if (flag2)
            {
                TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
                TrajectoryComponent.Highlighted = null;
            }
        }
        else
        {
            List<Vector3> list = TrajectoryComponent.PlotTrajectory(useableGun, out RaycastHit raycastHit, 255);
            bool flag3 = Vector3.Distance(list.Last<Vector3>(), OptimizationVariables.MainPlayer.look.aim.position) > useableGun.equippedGunAsset.range;
            ColorVariable color = ColorUtilities.getColor("_TrajectoryPredictionInRange");
            ColorVariable color2 = ColorUtilities.getColor("_TrajectoryPredictionOutOfRange");
            bool flag4 = WeaponOptions.HighlightBulletDropPredictionTarget && raycastHit.collider != null;
            if (flag4)
            {
                Transform transform = raycastHit.transform;
                GameObject gameObject = null;
                bool flag5 = DamageTool.getPlayer(transform) != null;
                if (flag5)
                {
                    gameObject = DamageTool.getPlayer(transform).gameObject;
                }
                else
                {
                    bool flag6 = DamageTool.getZombie(transform) != null;
                    if (flag6)
                    {
                        gameObject = DamageTool.getZombie(transform).gameObject;
                    }
                    else
                    {
                        bool flag7 = DamageTool.getAnimal(transform) != null;
                        if (flag7)
                        {
                            gameObject = DamageTool.getAnimal(transform).gameObject;
                        }
                        else
                        {
                            bool flag8 = DamageTool.getVehicle(transform) != null;
                            if (flag8)
                            {
                                gameObject = DamageTool.getVehicle(transform).gameObject;
                            }
                        }
                    }
                }
                bool flag9 = gameObject != null;
                if (flag9)
                {
                    Highlighter highlighter = gameObject.GetComponent<Highlighter>() ?? gameObject.AddComponent<Highlighter>();
                    bool flag10 = !highlighter.enabled;
                    if (flag10)
                    {
                        highlighter.occluder = true;
                        highlighter.overlay = true;
                        highlighter.ConstantOnImmediate(flag3 ? color2 : color);
                    }
                    bool flag11 = TrajectoryComponent.Highlighted != null && highlighter != TrajectoryComponent.Highlighted;
                    if (flag11)
                    {
                        TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
                    }
                    TrajectoryComponent.Highlighted = highlighter;
                }
                else
                {
                    bool flag12 = TrajectoryComponent.Highlighted != null;
                    if (flag12)
                    {
                        TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
                        TrajectoryComponent.Highlighted = null;
                    }
                }
            }
            else
            {
                bool flag13 = !WeaponOptions.HighlightBulletDropPredictionTarget && TrajectoryComponent.Highlighted != null;
                if (flag13)
                {
                    TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
                    TrajectoryComponent.Highlighted = null;
                }
            }
            ESPComponent.GLMat.SetPass(0);
            GL.PushMatrix();
            GL.LoadProjectionMatrix(OptimizationVariables.MainCam.projectionMatrix);
            GL.modelview = OptimizationVariables.MainCam.worldToCameraMatrix;
            GL.Begin(2);
            GL.Color(flag3 ? color2 : color);
            foreach (Vector3 vector in list)
            {
                GL.Vertex(vector);
            }
            GL.End();
            GL.PopMatrix();
        }
    }
     
    public static void RemoveHighlight(Highlighter h)
    {
        bool flag = h == null;
        if (!flag)
        {
            h.occluder = false;
            h.overlay = false;
            h.ConstantOffImmediate();
        }
    }
     
    public static List<Vector3> PlotTrajectory(UseableGun gun, out RaycastHit hit, int maxSteps = 255)
    {
        hit = default(RaycastHit);
        Transform transform = (OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.FIRST) ? OptimizationVariables.MainPlayer.look.aim : OptimizationVariables.MainCam.transform;
        Vector3 vector = transform.position;
        Vector3 forward = transform.forward;
        ItemGunAsset equippedGunAsset = gun.equippedGunAsset;
        float num = equippedGunAsset.ballisticDrop;
        Attachments attachments = (Attachments)TrajectoryComponent.thirdAttachments.GetValue(gun);
        List<Vector3> list = new List<Vector3>
            {
                vector
            };
        bool flag = ((attachments != null) ? attachments.barrelAsset : null) != null;
        if (flag)
        {
            num *= attachments.barrelAsset.ballisticDrop;
        }
        for (int i = 1; i < maxSteps; i++)
        {
            vector += forward * equippedGunAsset.ballisticTravel;
            forward.y -= num;
            forward.Normalize();
            bool flag2 = Physics.Linecast(list[i - 1], vector, out hit, RayMasks.DAMAGE_CLIENT);
            if (flag2)
            {
                list.Add(hit.point);
                break;
            }
            list.Add(vector);
        }
        return list;
    }
     
    [OnSpy]
    public static void OnSpy()
    {
        bool flag = TrajectoryComponent.Highlighted != null;
        if (flag)
        {
            TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
        }
        TrajectoryComponent.spying = true;
    }
     
    [OffSpy]
    public static void OffSpy()
    {
        TrajectoryComponent.spying = false;
    } 
    public static readonly FieldInfo thirdAttachments = typeof(UseableGun).GetField("thirdAttachments", BindingFlags.Instance | BindingFlags.NonPublic);
     
    public static bool spying;
}

