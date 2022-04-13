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

public class MenuBackButton : MonoBehaviour {

	public void OnClickBackButton()
	{
		Managers.Audio.PlayClickSound ();
		transform.parent.gameObject.SetActive (false);
		MainMenu.Instance.menuButtons.SetActive (true);
		PlayerPrefs.SetFloat ("Sound",Managers.Audio.soundSource.volume);
		PlayerPrefs.SetFloat ("Music",Managers.Audio.musicSource.volume);
	}
}
