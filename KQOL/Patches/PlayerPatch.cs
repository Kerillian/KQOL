using HarmonyLib;
using Reptile;

namespace KQOL.Patches
{
	[HarmonyPatch(typeof(Player))]
	public class PlayerPatch
	{
		private static void Switch(Player player, MoveStyle moveStyle)
		{
			player.AddBoostCharge(-Plugin.Konfig.QuickSwitchCost.Value);
			player.InitMovement(moveStyle);
			player.SwitchToEquippedMovestyle(true,
				// Don't do air tricks if we're on the ground, grinding, or wall running.
				!player.IsGrounded() &&
				!player.IsGrinding() &&
				player.ability != player.wallrunAbility
			);
		}
		
		/*
		 * First time using a Publicizer, and honestly I'm in love with it.
		 * Being able to use nameof on "private" methods looks nicer to me.
		 */
		[HarmonyPostfix, HarmonyPatch(nameof(Player.UpdatePlayer))]
		private static void UpdatePlayerPatch(Player __instance)
		{
			if (!Plugin.Konfig.QuickSwitch.Value)
			{
				return;
			}
			
			if (__instance.inputBuffer.switchStyleButtonHeld && __instance.boostCharge < Plugin.Konfig.QuickSwitchCost.Value)
			{
				if (__instance.inputBuffer.trick1ButtonNew)
				{
					Switch(__instance, MoveStyle.BMX);
				}
				else if (__instance.inputBuffer.trick2ButtonNew)
				{
					Switch(__instance, MoveStyle.SKATEBOARD);
				}
				else if (__instance.inputBuffer.trick3ButtonNew)
				{
					Switch(__instance, MoveStyle.INLINE);
				}
			}
		}
	}
}