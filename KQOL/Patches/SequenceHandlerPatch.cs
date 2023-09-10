using HarmonyLib;
using Reptile;

namespace KQOL.Patches
{
	[HarmonyPatch(typeof(SequenceHandler))]
	public class SequenceHandlerPatch
	{
		/*
		 * Okay so this is sorta aids
		 */
		[HarmonyPostfix, HarmonyPatch(nameof(SequenceHandler.UpdateSequenceHandler))]
		private static void UpdateSequenceHandlerPatch(SequenceHandler __instance)
		{
			if (
				Plugin.Konfig.AutoSkip.Value && 
			    __instance.IsEnabled && 
			    __instance.skipTextActiveState != SequenceHandler.SkipState.NOT_SKIPPABLE &&
				!__instance.disabledExit &&
				__instance.exitSequenceRoutine == null
			)
			{
				__instance.ExitCurrentSequence();
			}
		}
	}
}