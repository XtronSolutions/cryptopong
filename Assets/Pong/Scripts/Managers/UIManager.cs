//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                  *
//   * Facebook: https://goo.gl/5YSrKw											      *
//   * Contact me: https://goo.gl/y5awt4								              *											
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Menus
{
	MAIN,
	INGAME,
	GAMEOVER
}

public class UIManager : MonoBehaviour {

	public MainMenu mainMenu;
	public InGameUI inGameUI;

	public void ActivateUI(Menus menutype)
	{
		if (menutype.Equals (Menus.MAIN))
		{
			inGameUI.gameObject.SetActive (false);
			mainMenu.gameObject.SetActive (true);
		}
		else if(menutype.Equals(Menus.INGAME))
		{
			inGameUI.gameObject.SetActive (true);
			mainMenu.gameObject.SetActive (false);
		}	
	}
}
