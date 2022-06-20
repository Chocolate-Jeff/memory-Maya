using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splash : MonoBehaviour {
	void Start () {
        StartCoroutine(load());
	}

    IEnumerator load()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync("MENU");
    }
}
