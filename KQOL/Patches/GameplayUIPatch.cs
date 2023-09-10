using HarmonyLib;
using Reptile;

namespace KQOL.Patches
{
	[HarmonyPatch(typeof(GameplayUI))]
	public class GameplayUIPatch
	{
		/*
		 * Had to use Postfix here instead of Prefix
		 * The UI would stop working after loading into a new scene if I just returned out of the method.
		 */
		[HarmonyPostfix, HarmonyPatch(nameof(GameplayUI.OnStageInitialized))]
		private static void OnStageInitializedPatch(GameplayUI __instance)
		{
			if (Plugin.Konfig.HudDisabled)
			{
				__instance.gameObject.SetActive(false);
			}
		}
	}
}