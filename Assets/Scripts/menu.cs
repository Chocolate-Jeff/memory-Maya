using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {


    private bool isFocus = false;
    private bool isProcessing = false;
    public GameObject aboutPanel;


    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
    void Start () {
		
	}
	
    public void setGamemode(bool singleplayer)
    {
        if (singleplayer == true)
        {
            PlayerPrefs.SetInt("gamemode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("gamemode", 0);
        }
    }

    public void LoadLevel(int diff)
    {
        if (diff == -1)
        {
            Application.Quit();
        }
        PlayerPrefs.SetInt("diff", diff);
        StartCoroutine(lvlLoad("SampleScene"));
    }
    IEnumerator lvlLoad(string name)
    {
        GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>().Play("exitmenu");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(name);
    }

    public void ShareText()
    {
        string shareText = "I'm loving Memory Maya, play it with me! Play and buy great games at http://gamesforseva.org/";
#if UNITY_ANDROID
        if (!isProcessing) {
			StartCoroutine (ShareTextInAnroid (shareText));
		}
#else
        aboutPanel.SetActive(true);
        aboutPanel.GetComponent<loadAbout>().SetSocial(0);
#endif
    }



#if UNITY_ANDROID
	public IEnumerator ShareTextInAnroid (string sharpaleitem) {

		var shareSubject = "Memory Maya";

		isProcessing = true;

		if (!Application.isEditor) {
			//Create intent for action send
			AndroidJavaClass intentClass = 
				new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = 
				new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject> 
				("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));

			//put text and subject extra
			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject> 
				("putExtra", intentClass.GetStatic<string> ("EXTRA_SUBJECT"), shareSubject);
			intentObject.Call<AndroidJavaObject> 
				("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), shareitem);

			//call createChooser method of activity class
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = 
				unity.GetStatic<AndroidJavaObject> ("currentActivity");
			AndroidJavaObject chooser = 
				intentClass.CallStatic<AndroidJavaObject> 
				("createChooser", intentObject, "Share your high score");
			currentActivity.Call ("startActivity", chooser);
		}

		yield return new WaitUntil (() => isFocus);
		isProcessing = false;
	}
#endif
}
