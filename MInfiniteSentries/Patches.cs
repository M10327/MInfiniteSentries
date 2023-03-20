using HarmonyLib;
using Rocket.Unturned.Items;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MInfiniteSentries
{
    internal static class Patches
    {
        // thanks to BlazingFlame#0001 in the imperial plugin discord for giving the example code to help me make this work
        private static Harmony PatcherInstance;
        // run in Load()
        internal static void PatchAll()
        {
            PatcherInstance = new Harmony("MInfiniteSentries");
            PatcherInstance.PatchAll();
            Rocket.Core.Logging.Logger.Log("Patched sentries");
        }
        // run in Unload()
        internal static void UnpatchAll()
        {
            PatcherInstance.UnpatchAll("MInfiniteSentries");
            Rocket.Core.Logging.Logger.Log("Unpatched sentries");
        }
        [HarmonyPatch]
        internal static class InternalPatches
        {
            [HarmonyPatch(typeof(InteractableSentry))]
            [HarmonyPatch("shoot")]
            [HarmonyPostfix]
            static void SentryUpdate(byte __state, InteractableSentry __instance)
            {
                if (MInfiniteSentries.Instance.Configuration.Instance.UseForAllSentries || MInfiniteSentries.Instance.Configuration.Instance.UserList.Contains((ulong)__instance.owner))
                {
                    foreach (var x in __instance.items.items)
                    {
                        if (MInfiniteSentries.Instance.Configuration.Instance.AllowAllGuns || MInfiniteSentries.Instance.Configuration.Instance.GunWhitelist.Contains(x.item.id))
                        {
                            ushort id = GetId(x.item.state[8], x.item.state[9]);
                            if (MInfiniteSentries.Instance.Configuration.Instance.AllowAllMags || MInfiniteSentries.Instance.Configuration.Instance.MagWhitelist.Contains(id))
                            {
                                var magazine = Assets.find(EAssetType.ITEM, id) as ItemMagazineAsset;
                                if (x.item.metadata[10] != magazine.amount)
                                {
                                    x.item.metadata[10] = magazine.amount;
                                }
                            }
                        }
                    }
                }
            }

            public static ushort GetId(byte b1, byte b2)
            {
                byte[] bytes = new byte[2] { b1, b2 };
                return BitConverter.ToUInt16(bytes, 0);
            }

        }
    }
}
