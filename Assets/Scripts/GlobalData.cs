using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
	#region Scene Names
	public enum SceneName
	{
		MainMenu = 0,
		Credits = 1,
		Overworld = 2,
		Cave = 3,
	}
	#endregion


	#region Player
	[Header("Next player position")]
	//Default position
	public static Vector3 playerStartPosition = new Vector3(-6.5f, -5.5f, 0f);
	//Default rotation
	public static Vector2 PlayerStartRotation = new Vector2(0, -1);
	public static float currentLife = -1f;
	#endregion

	#region Lamps
	public static List<int> listOfLamps = new List<int>();
	#endregion
}
