using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	public string[] allLevel;
    public Text loadingText;
	public Image loadingImage;
    private bool onLoading;
    private AsyncOperation asc;
	private bool readyToLoad;

    public enum LoadingType {
        normal,
        back,
		characterSelection,
		map
    }

    public LoadingType lt;

	void Start () {
       	if(!onLoading) {
            StartCoroutine(LoadingPros());
			onLoading = true;
        }
	}

    private IEnumerator LoadingPros() {
		switch(lt){
		case(LoadingType.normal):
			for (int a = 0; a < 3; a++){
				if (Game.game.LevelEnter == a){
					asc = SceneManager.LoadSceneAsync(allLevel[a]);
					asc.allowSceneActivation = false;
				}
			}
			break;
		case(LoadingType.back):
			asc = SceneManager.LoadSceneAsync("MenuUtama");
			break;
		case(LoadingType.characterSelection):
			asc = SceneManager.LoadSceneAsync("CharacterSelection");
			break;
		case(LoadingType.map):
			asc = SceneManager.LoadSceneAsync("Map");
			break;
		}
			
      while (!asc.isDone){
			if(asc.progress <= 0.89){
				loadingImage.transform.localScale = new Vector3(Mathf.Clamp(asc.progress,0,1),loadingImage.transform.localScale.y,loadingImage.transform.localScale.z);
				loadingText.text = (int)(asc.progress * 100) +" %";
			}else{
				readyToLoad = true;
				loadingImage.transform.localScale = new Vector3(1f,loadingImage.transform.localScale.y,loadingImage.transform.localScale.z);
				if(lt == LoadingType.normal){
					loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b,Mathf.PingPong(Time.timeSinceLevelLoad,1f));
					loadingText.text = "Tap To Play";
				}else{
					loadingText.text = "100%";
				}
			}
			yield return null;
        }
    }

	void Update(){
		if(Input.GetKeyDown(KeyCode.H)){
			asc.allowSceneActivation = true;
		}

		if(Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Began && readyToLoad){
			asc.allowSceneActivation = true;
		}
	}
}
