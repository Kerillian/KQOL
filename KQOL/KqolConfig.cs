using BepInEx.Configuration;
using UnityEngine;

namespace KQOL
{
	public class KqolConfig
	{
		public readonly ConfigEntry<bool> QuickSwitch;
		public readonly ConfigEntry<float> QuickSwitchCost;

		public readonly ConfigEntry<bool> AutoSkip;
		public readonly ConfigEntry<bool> DisableStarCutscenes;

		public readonly ConfigEntry<KeyCode> ToggleHudKey;
		public readonly ConfigEntry<KeyCode> FreecamKey;
		public readonly ConfigEntry<KeyCode> TimescaleUpKey;
		public readonly ConfigEntry<KeyCode> TimescaleDownKey;
		public readonly ConfigEntry<KeyCode> TimescaleResetKey;
		public bool HudDisabled = false;

		public KqolConfig(ConfigFile file)
		{
			this.QuickSwitch = file.Bind(
				"General",
				"QuickSwitch",
				true,
				"Grants the ability to switch movement style on the fly."
			);
			
			this.QuickSwitchCost = file.Bind(
				"General",
				"QuickSwitchCost",
				2f,
				"Boost cost of preforming a quick switch."
			);

			this.AutoSkip = file.Bind(
				"General",
				"AutoSkip",
				false,
				"Automatically skip skippable cutscenes."
			);
			
			this.DisableStarCutscenes = file.Bind(
				"General",
				"DisableStarCutscenes",
				true,
				"Disables the cutscenes that play when gaining a wanted star."
			);

			this.ToggleHudKey = file.Bind(
				"Keybinds",
				"ToggleHud",
				KeyCode.Backspace,
				"This key can enable and disable the in-game hud."
			);
			
			this.FreecamKey = file.Bind(
				"Keybinds",
				"Freecam",
				KeyCode.Backslash,
				"This key can enable and disable a freecam."
			);

			this.TimescaleUpKey = file.Bind(
				"Keybinds",
				"TimescaleUp",
				KeyCode.Plus,
				"Increase the timescale by 0.1"
			);
			
			this.TimescaleDownKey = file.Bind(
				"Keybinds",
				"TimescaleDown",
				KeyCode.Minus,
				"Decrease the timescale by 0.1"
			);
			
			this.TimescaleResetKey = file.Bind(
				"Keybinds",
				"TimescaleReset",
				KeyCode.Alpha0,
				"Reset the timescale to 1.0"
			);
		}
	}
}