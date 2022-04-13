using UnityEngine;
using System.Collections;

public class MenuState : _StatesBase {
	
	#region implemented abstract members of GameState

	public override void OnActivate ()
	{		
		Debug.Log ("<color=green>Menu State</color> OnActive");	
		Managers.UI.ActivateUI (Menus.MAIN);
		Managers.Audio.PlayGameMusic ();
		Managers.PowUps.canSpawnPowerup = false;
		Managers.UI.inGameUI.gameBackButton.gameObject.SetActive (false);

		if (Managers.Game.isGameActive)
		{
			Managers.UI.mainMenu.continueButton.SetActive (true);
		} 
		else
		{
			Managers.UI.mainMenu.continueButton.SetActive (false);
		}
	}

	public override void OnDeactivate ()
	{
		Debug.Log ("<color=red>Menu State</color> OnDeactivate");
	}

	public override void OnUpdate ()
	{
		Debug.Log ("<color=yellow>Menu State</color> OnUpdate");
	}

	#endregion
}
