using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Reptile;
using UnityEngine;

namespace KQOL
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	[BepInProcess("Bomb Rush Cyberfunk.exe")]
	public class Plugin : BaseUnityPlugin
	{
		public static ManualLogSource Log;
		public static KqolConfig Konfig;
		public static Harmony Harmony;

		public static GameObject DebugText;
		
		private void Awake()
		{
			Log = Logger;
			Konfig = new KqolConfig(Config);
			Harmony = new Harmony(PluginInfo.PLUGIN_GUID);
			Harmony.PatchAll(GetType().Assembly);
		}

		private void Update()
		{
			if (UnityInput.Current.GetKeyDown(Konfig.ToggleHudKey.Value))
			{
				Core.instance.uiManager.gameplay.gameObject.SetActive(!Core.instance.uiManager.gameplay.gameObject.activeSelf);
				Konfig.HudDisabled = !Core.instance.uiManager.gameplay.gameObject.activeSelf;
			}

			if (UnityInput.Current.GetKeyDown(Konfig.FreecamKey.Value))
			{
				if (FreeCamera.Created)
				{
					FreeCamera.DestroyCamera();
				}
				else
				{
					FreeCamera.MakeFreecam();
				}
			}

			if (UnityInput.Current.GetKeyDown(Konfig.TimescaleUpKey.Value))
			{
				Time.timeScale = Mathf.Clamp(Time.timeScale + 0.1f, 0f, 2f);
			}
			
			if (UnityInput.Current.GetKeyDown(Konfig.TimescaleDownKey.Value))
			{
				Time.timeScale = Mathf.Clamp(Time.timeScale - 0.1f, 0f, 2f);
			}
			
			if (UnityInput.Current.GetKeyDown(Konfig.TimescaleResetKey.Value))
			{
				Time.timeScale = 1f;
			}
		}
	}
}