using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class PlayerAd : MonoBehaviour {

	public void ShowAd(){
		if(Advertisement.IsReady()){
			Advertisement.Show();
		}
	}
}
