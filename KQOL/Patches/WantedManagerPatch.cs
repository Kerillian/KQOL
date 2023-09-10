using HarmonyLib;
using Reptile;

namespace KQOL.Patches
{
	[HarmonyPatch(typeof(WantedManager))]
	public class WantedManagerPatch
	{
		[HarmonyPrefix, HarmonyPatch(nameof(WantedManager.DoNewStarSequence))]
		private static bool DoNewStarSequencePatch()
		{
			return Plugin.Konfig.DisableStarCutscenes.Value;
		}
	}
}