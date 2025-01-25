using MelonLoader;
using HarmonyLib;
using System.Reflection;
using LabFusion.Representation;
using LabFusion.Network;

[assembly: MelonInfo(typeof(NoFusionBackdoor.Main), "NoFusionBackdoor", "1.0.0", "VeygaX")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
namespace NoFusionBackdoor;
public class Main : MelonMod
{
    public override void OnInitializeMelon()
    {
        var targetType = typeof(FusionMasterList);
        var verifyMethod = targetType.GetMethod("VerifyPlayer", BindingFlags.Public | BindingFlags.Static);
        
        if (verifyMethod != null)
        {
            var prefix = new HarmonyMethod(typeof(Main).GetMethod(nameof(VerifyPlayerPrefix), BindingFlags.Static | BindingFlags.NonPublic));
            HarmonyInstance.Patch(verifyMethod, prefix: prefix);

#if DEBUG
            // Test the patch with Lakatrazz's ID
            var result = FusionMasterList.VerifyPlayer(76561198198752494, "Lakatrazz");
            MelonLogger.Msg($"Test verification result for Lakatrazz: {result}");
#endif
        }
    }

    private static bool VerifyPlayerPrefix(ref FusionMasterResult __result)
    {
        __result = FusionMasterResult.NORMAL;
        return false;
    }
}
