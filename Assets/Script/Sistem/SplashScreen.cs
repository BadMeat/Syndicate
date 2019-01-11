using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	public Text[] allSpalsh;
	public float speed = 0.5f;

	void Start(){
		StartCoroutine(StartSplash());
	}

	private IEnumerator StartSplash(){
		for(int a=0;a<allSpalsh.Length;a++){
			float time = 0;
			while(time < 1){
				time+=speed * Time.deltaTime;
				Color tempColor = allSpalsh[a].color;
				tempColor.a = time;
				allSpalsh[a].color = tempColor;
				yield return null;
			}
			while(time > 0){
				time-=speed * Time.deltaTime;
				Color tempColor = allSpalsh[a].color;
				tempColor.a = time;
				allSpalsh[a].color = tempColor;
				yield return null;
			}
		}
		SceneManager.LoadScene("MenuUtama");
	}
}
