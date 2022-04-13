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
using System.Collections.Generic;
using System.Linq;

public class PowerupManager : MonoBehaviour {

	public float speedUpValue;
	public float speedDownValue;
	public float enlargeValue;
	public float shrinkValue;
	public float ghostingValue;

	public List<GameObject> powerupPrefabs;

	public bool canSpawnPowerup;
	public float newPowerupInterval;
	public float powerupImpactDuration;

	public List<Powerup> spawnedPowerupList;

	public IEnumerator SpawnPowerup()
	{
		while (canSpawnPowerup)
		{
			Vector2 randomPosition = Random.insideUnitCircle * 2.4f;
			int randomType = Random.Range (0, powerupPrefabs.Count);
			GameObject clone = (GameObject)Instantiate(powerupPrefabs[randomType], randomPosition, transform.rotation);
			spawnedPowerupList.Add (clone.GetComponent<Powerup>());
			yield return new WaitForSeconds (newPowerupInterval);
		}
		yield return null;
	}

	public void DestroyPowerups()
	{
		foreach (Powerup p in spawnedPowerupList.ToList())
		{
			p.DisablePowerup ();

		}
	}

}
