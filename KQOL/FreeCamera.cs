using System;
using BepInEx;
using Reptile;
using UnityEngine;

namespace KQOL
{
	public class FreeCamera : MonoBehaviour
	{
		private IInputSystem input;
		private Vector3 mousePrev = Vector3.zero;
		private float moveSpeed = 1f;

		public static bool Created { get; private set; }
		private static Camera OurCamera;
		private static Camera OriginCamera;

		public static void MakeFreecam()
		{
			if (Created)
			{
				Plugin.Log.LogWarning("Tried to create multiple freecam cameras!");
				return;
			}

			if (GameplayCamera.instance == null)
			{
				Plugin.Log.LogWarning("Tried to create a freecam during invalid state!");
				return;
			}

			OurCamera = new GameObject("KerillianWasHere").AddComponent<Camera>();
			DontDestroyOnLoad(OurCamera.gameObject);

			OurCamera.gameObject.tag = "MainCamera";
			OurCamera.gameObject.hideFlags = HideFlags.HideAndDontSave;
			OurCamera.gameObject.AddComponent<FreeCamera>();
			
			OriginCamera = Camera.current;
			OurCamera.transform.position = GameplayCamera.instance.transform.position;
			OurCamera.transform.rotation = GameplayCamera.instance.transform.rotation;
			OriginCamera.enabled = false;

			OurCamera.enabled = true;
			Created = true;
			
			WorldHandler.instance.currentPlayer.userInputEnabled = false;
			WorldHandler.instance.currentPlayer.phone.AllowPhone(false, false, false);
		}

		public static void DestroyCamera()
		{
			if (!Created)
			{
				return;
			}
			
			OurCamera.enabled = false;
			Destroy(OurCamera.gameObject);

			OriginCamera.enabled = true;
			
			OurCamera = null;
			OriginCamera = null;
			Created = false;

			WorldHandler.instance.currentPlayer.userInputEnabled = true;
			WorldHandler.instance.currentPlayer.phone.AllowPhone(true, false, false);
		}

		private void Awake()
		{
			input = UnityInput.Current;
		}

		private void Update()
		{
			if (!Application.isFocused)
			{
				return;
			}
		
			moveSpeed = Mathf.Clamp(moveSpeed + input.mouseScrollDelta.y * 0.3f, 1f, 100f);
			float desiredSpeed = moveSpeed;

			if (input.GetKey(KeyCode.LeftShift))
			{
				desiredSpeed *= 2;
			}

			Transform tr = OurCamera.transform;
			
			if (input.GetKey(KeyCode.W))
			{
				tr.position += tr.forward * (desiredSpeed * Time.unscaledDeltaTime);
			}
			
			if (input.GetKey(KeyCode.S))
			{
				tr.position -= tr.forward * (desiredSpeed * Time.unscaledDeltaTime);
			}
			
			if (input.GetKey(KeyCode.A))
			{
				tr.position -= tr.right * (desiredSpeed * Time.unscaledDeltaTime);
			}
			
			if (input.GetKey(KeyCode.D))
			{
				tr.position += tr.right * (desiredSpeed * Time.unscaledDeltaTime);
			}

			if (input.GetKey(KeyCode.Space))
			{
				tr.position += Vector3.up * (desiredSpeed * Time.unscaledDeltaTime);
			}

			if (input.GetKeyDown(KeyCode.C))
			{
				tr.position -= Vector3.up * (desiredSpeed * Time.unscaledDeltaTime);
			}

			if (input.GetKeyDown(KeyCode.Q))
			{
				WorldHandler.instance.currentPlayer.transform.position = tr.position;
			}

			Vector3 delta = input.mousePosition - mousePrev;
			float x = tr.localEulerAngles.y + delta.x * 0.2f;
			float y = tr.localEulerAngles.x - delta.y * 0.2f;

			tr.localEulerAngles = new Vector3(y, x);
			mousePrev = input.mousePosition;
		}
	}
}