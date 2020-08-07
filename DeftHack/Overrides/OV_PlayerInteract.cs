using HighlightingSystem;
using SDG.Framework.Utilities;
using SDG.Unturned;
using System;
using System.Reflection;
using UnityEngine;


 
public class OV_PlayerInteract
{
 
    [Initializer]
    public static void Init()
    {
        OV_PlayerInteract.FocusField = typeof(PlayerInteract).GetField("focus", ReflectionVariables.publicStatic);
        OV_PlayerInteract.TargetField = typeof(PlayerInteract).GetField("target", ReflectionVariables.publicStatic);
        OV_PlayerInteract.InteractableField = typeof(PlayerInteract).GetField("_interactable", ReflectionVariables.publicStatic);
        OV_PlayerInteract.Interactable2Field = typeof(PlayerInteract).GetField("_interactable2", ReflectionVariables.publicStatic);
        OV_PlayerInteract.PurchaseAssetField = typeof(PlayerInteract).GetField("purchaseAsset", ReflectionVariables.publicStatic);
    }
     
    public static Transform focus
    {
        get => (Transform)OV_PlayerInteract.FocusField.GetValue(null);
        set => OV_PlayerInteract.FocusField.SetValue(null, value);
    }
     
    public static Transform target
    {
        get => (Transform)OV_PlayerInteract.TargetField.GetValue(null);
        set => OV_PlayerInteract.TargetField.SetValue(null, value);
    } 
    public static Interactable interactable
    {
        get => (Interactable)OV_PlayerInteract.InteractableField.GetValue(null);
        set => OV_PlayerInteract.InteractableField.SetValue(null, value);
    } 
    public static Interactable2 interactable2
    {
        get => (Interactable2)OV_PlayerInteract.Interactable2Field.GetValue(null);
        set => OV_PlayerInteract.Interactable2Field.SetValue(null, value);
    }
     
    public static ItemAsset purchaseAsset
    {
        get => (ItemAsset)OV_PlayerInteract.PurchaseAssetField.GetValue(null);
        set => OV_PlayerInteract.PurchaseAssetField.SetValue(null, value);
    }
     
    public float salvageTime
    {
        get
        {
            if (MiscOptions.CustomSalvageTime)
            {
                return MiscOptions.SalvageTime;
            }
            if (!OptimizationVariables.MainPlayer.channel.owner.isAdmin)
            {
                return 8f;
            }
            return 1f;
        }
    }
     
    public void hotkey(byte button)
    {
        VehicleManager.swapVehicle(button);
    }
     
    public void onPurchaseUpdated(PurchaseNode node)
    { 
        if (node == null)
        {
            OV_PlayerInteract.purchaseAsset = null;
        }
        else
        {
            OV_PlayerInteract.purchaseAsset = (ItemAsset)Assets.find(EAssetType.ITEM, node.id);
        }
    }
     
    public static void highlight(Transform target, Color color)
    {
         
        if (!target.CompareTag("Player") || target.CompareTag("Enemy") || target.CompareTag("Zombie") || target.CompareTag("Animal") || target.CompareTag("Agent"))
        {
            Highlighter highlighter = target.GetComponent<Highlighter>();
             
            if (highlighter == null)
            {
                highlighter = target.gameObject.AddComponent<Highlighter>();
            }
            highlighter.ConstantOn(color, 0.25f);
        }
    }
     
    [OnSpy]
    public static void OnSpied()
    {
         
            Transform transform = OptimizationVariables.MainCam.transform;
            if (transform != null)
            {
                PhysicsUtility.raycast(new Ray(transform.position, transform.forward), out OV_PlayerInteract.hit, (OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.THIRD) ? 6 : 4, RayMasks.PLAYER_INTERACT, 0);
            }

 
    }

    [Override(typeof(PlayerInteract), "Update", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public void OV_Update()
    {
        if (!DrawUtilities.ShouldRun())
        {
            return;
        }
        if (OptimizationVariables.MainPlayer.stance.stance != EPlayerStance.DRIVING && OptimizationVariables.MainPlayer.stance.stance != EPlayerStance.SITTING && !OptimizationVariables.MainPlayer.life.isDead && !OptimizationVariables.MainPlayer.workzone.isBuilding)
        {
            if (Time.realtimeSinceStartup - OV_PlayerInteract.lastInteract > 0.1f)
            {
                int num = 0;
                if (InteractionOptions.InteractThroughWalls && !PlayerCoroutines.IsSpying)
                {
                    if (!InteractionOptions.NoHitBarricades)
                    {
                        num |= RayMasks.BARRICADE;

                    }
                    if (!InteractionOptions.NoHitItems)
                    {
                        num |= RayMasks.ITEM;
                    }
                    if (!InteractionOptions.NoHitResources)
                    {
                        num |= RayMasks.RESOURCE;
                    }
                    if (!InteractionOptions.NoHitStructures)
                    {
                        num |= RayMasks.STRUCTURE;
                    }
                    if (!InteractionOptions.NoHitVehicles)
                    {
                        num |= RayMasks.VEHICLE;
                    }
                    if (!InteractionOptions.NoHitEnvironment)
                    {
                        num |= (RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND);
                    }
                }
                else
                {
                    num = RayMasks.PLAYER_INTERACT;
                }

                OV_PlayerInteract.lastInteract = Time.realtimeSinceStartup;
                float num2 = (InteractionOptions.InteractThroughWalls && !PlayerCoroutines.IsSpying) ? 20f : 4f;
                PhysicsUtility.raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out OV_PlayerInteract.hit, (OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.THIRD) ? (num2 + 2f) : num2, num, 0);
            }
            Transform transform = (!(OV_PlayerInteract.hit.collider != null)) ? null : OV_PlayerInteract.hit.collider.transform;
            if (transform != OV_PlayerInteract.focus)
            {
                if (OV_PlayerInteract.focus != null && PlayerInteract.interactable != null)
                {
                    InteractableDoorHinge componentInParent = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
                    if (componentInParent != null)
                    {
                        HighlighterTool.unhighlight(componentInParent.door.transform);
                    }
                    else
                    {
                        HighlighterTool.unhighlight(PlayerInteract.interactable.transform);
                    }
                }

                OV_PlayerInteract.focus = null;

                OV_PlayerInteract.target = null;

                OV_PlayerInteract.interactable = null;

                OV_PlayerInteract.interactable2 = null;
                if (transform != null)
                {

                    OV_PlayerInteract.focus = transform;

                    OV_PlayerInteract.interactable = OV_PlayerInteract.focus.GetComponentInParent<Interactable>();

                    OV_PlayerInteract.interactable2 = OV_PlayerInteract.focus.GetComponentInParent<Interactable2>();
                    if (PlayerInteract.interactable != null)
                    {

                        OV_PlayerInteract.target = TransformRecursiveFind.FindChildRecursive(PlayerInteract.interactable.transform, "Target");
                        if (PlayerInteract.interactable.checkInteractable())
                        {
                            if (PlayerUI.window.isEnabled)
                            {
                                if (PlayerInteract.interactable.checkUseable())
                                {
                                    if (!PlayerInteract.interactable.checkHighlight(out Color color))
                                    {
                                        color = Color.green;
                                    }
                                    InteractableDoorHinge componentInParent2 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
                                    if (componentInParent2 != null)
                                    {
                                        HighlighterTool.highlight(componentInParent2.door.transform, color);
                                    }
                                    else
                                    {
                                        HighlighterTool.highlight(PlayerInteract.interactable.transform, color);
                                    }
                                }
                                else
                                {
                                    Color color = Color.red;
                                    InteractableDoorHinge componentInParent3 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
                                    if (componentInParent3 != null)
                                    {
                                        HighlighterTool.highlight(componentInParent3.door.transform, color);
                                    }
                                    else
                                    {
                                        HighlighterTool.highlight(PlayerInteract.interactable.transform, color);
                                    }
                                }
                            }
                        }
                        else
                        {

                            OV_PlayerInteract.target = null;

                            OV_PlayerInteract.interactable = null;
                        }
                    }
                }
            }
        }
        else
        {
            if (OV_PlayerInteract.focus != null && PlayerInteract.interactable != null)
            {
                InteractableDoorHinge componentInParent4 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
                if (componentInParent4 != null)
                {
                    HighlighterTool.unhighlight(componentInParent4.door.transform);
                }
                else
                {
                    HighlighterTool.unhighlight(PlayerInteract.interactable.transform);
                }
            }

            OV_PlayerInteract.focus = null;

            OV_PlayerInteract.target = null;

            OV_PlayerInteract.interactable = null;

            OV_PlayerInteract.interactable2 = null;
        }
        if (OptimizationVariables.MainPlayer.life.isDead)
        {
            return;
        }
        if (PlayerInteract.interactable != null)
        {
            if (PlayerInteract.interactable.checkHint(out EPlayerMessage eplayerMessage, out string text, out Color color2) && !PlayerUI.window.showCursor)
            {
                if (PlayerInteract.interactable.CompareTag("Item"))
                {
                    PlayerUI.hint((!(OV_PlayerInteract.target != null)) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, eplayerMessage, text, color2, new object[]
                    {
                        ((InteractableItem)PlayerInteract.interactable).item,
                        ((InteractableItem)PlayerInteract.interactable).asset
                    });
                }
                else
                {
                    PlayerUI.hint((!(OV_PlayerInteract.target != null)) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, eplayerMessage, text, color2, new object[0]);
                }
            }
        }
        else if (OV_PlayerInteract.purchaseAsset != null && OptimizationVariables.MainPlayer.movement.purchaseNode != null && !PlayerUI.window.showCursor)
        {
            PlayerUI.hint(null, EPlayerMessage.PURCHASE, string.Empty, Color.white, new object[]
            {

                OV_PlayerInteract.purchaseAsset.itemName,
                OptimizationVariables.MainPlayer.movement.purchaseNode.cost
            });
        }
        else if (OV_PlayerInteract.focus != null && OV_PlayerInteract.focus.CompareTag("Enemy"))
        {
            Player player = DamageTool.getPlayer(OV_PlayerInteract.focus);
            if (player != null && player != Player.player && !PlayerUI.window.showCursor)
            {
                PlayerUI.hint(null, EPlayerMessage.ENEMY, string.Empty, Color.white, new object[]
                {
                    player.channel.owner
                });
            }
        }
        if (PlayerInteract.interactable2 != null && PlayerInteract.interactable2.checkHint(out EPlayerMessage eplayerMessage2, out float num3) && !PlayerUI.window.showCursor)
        {
            PlayerUI.hint2(eplayerMessage2, (!OV_PlayerInteract.isHoldingKey) ? 0f : ((Time.realtimeSinceStartup - OV_PlayerInteract.lastKeyDown) / salvageTime), num3);
        }
        if ((OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SITTING) && !Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                hotkey(0);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                hotkey(1);
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                hotkey(2);
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                hotkey(3);
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                hotkey(4);
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                hotkey(5);
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                hotkey(6);
            }
            if (Input.GetKeyDown(KeyCode.F8))
            {
                hotkey(7);
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                hotkey(8);
            }
            if (Input.GetKeyDown(KeyCode.F10))
            {
                hotkey(9);
            }
        }
        if (Input.GetKeyDown(ControlsSettings.interact))
        {

            OV_PlayerInteract.lastKeyDown = Time.realtimeSinceStartup;

            OV_PlayerInteract.isHoldingKey = true;
        }
        if (Input.GetKeyDown(ControlsSettings.inspect) && ControlsSettings.inspect != ControlsSettings.interact && OptimizationVariables.MainPlayer.equipment.canInspect)
        {
            OptimizationVariables.MainPlayer.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
        }
        if (OV_PlayerInteract.isHoldingKey)
        {
            if (Input.GetKeyUp(ControlsSettings.interact))
            {

                OV_PlayerInteract.isHoldingKey = false;
                if (PlayerUI.window.showCursor)
                {
                    if (OptimizationVariables.MainPlayer.inventory.isStoring && OptimizationVariables.MainPlayer.inventory.shouldInteractCloseStorage)
                    {
                        PlayerDashboardUI.close();
                        PlayerLifeUI.open();
                        return;
                    }
                    if (PlayerBarricadeSignUI.active)
                    {
                        PlayerBarricadeSignUI.close();
                        PlayerLifeUI.open();
                        return;
                    }
                    if (PlayerBarricadeStereoUI.active)
                    {
                        PlayerBarricadeStereoUI.close();
                        PlayerLifeUI.open();
                        return;
                    }
                    if (PlayerBarricadeLibraryUI.active)
                    {
                        PlayerBarricadeLibraryUI.close();
                        PlayerLifeUI.open();
                        return;
                    }
                    if (PlayerBarricadeMannequinUI.active)
                    {
                        PlayerBarricadeMannequinUI.close();
                        PlayerLifeUI.open();
                        return;
                    }
                    if (PlayerNPCDialogueUI.active)
                    {
                        if (PlayerNPCDialogueUI.dialogueAnimating)
                        {
                            PlayerNPCDialogueUI.skipText();
                            return;
                        }
                        if (PlayerNPCDialogueUI.dialogueHasNextPage)
                        {
                            PlayerNPCDialogueUI.nextPage();
                            return;
                        }
                        PlayerNPCDialogueUI.close();
                        PlayerLifeUI.open();
                        return;
                    }
                    else
                    {
                        if (PlayerNPCQuestUI.active)
                        {
                            PlayerNPCQuestUI.closeNicely();
                            return;
                        }
                        if (PlayerNPCVendorUI.active)
                        {
                            PlayerNPCVendorUI.closeNicely();
                            return;
                        }
                    }
                }
                else
                {
                    if (OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SITTING)
                    {
                        VehicleManager.exitVehicle();
                        return;
                    }
                    if (OV_PlayerInteract.focus != null && PlayerInteract.interactable != null)
                    {
                        if (PlayerInteract.interactable.checkUseable())
                        {
                            PlayerInteract.interactable.use();
                            return;
                        }
                    }
                    else if (OV_PlayerInteract.purchaseAsset != null)
                    {
                        if (OptimizationVariables.MainPlayer.skills.experience >= OptimizationVariables.MainPlayer.movement.purchaseNode.cost)
                        {
                            OptimizationVariables.MainPlayer.skills.sendPurchase(OptimizationVariables.MainPlayer.movement.purchaseNode);
                            return;
                        }
                    }
                    else if (ControlsSettings.inspect == ControlsSettings.interact && OptimizationVariables.MainPlayer.equipment.canInspect)
                    {
                        OptimizationVariables.MainPlayer.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
                        return;
                    }
                }
            }
            else if (Time.realtimeSinceStartup - OV_PlayerInteract.lastKeyDown > salvageTime)
            {

                OV_PlayerInteract.isHoldingKey = false;
                if (!PlayerUI.window.showCursor && PlayerInteract.interactable2 != null)
                {
                    PlayerInteract.interactable2.use();
                }
            }
        }
    }
     
    public static FieldInfo FocusField;
     
    public static FieldInfo TargetField;
     
    public static FieldInfo InteractableField;
     
    public static FieldInfo Interactable2Field;
     
    public static FieldInfo PurchaseAssetField;
     
    public static bool isHoldingKey;
     
    public static float lastInteract;
     
    public static float lastKeyDown;
     
    public static RaycastHit hit;
}

