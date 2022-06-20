using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelHa : MonoBehaviour {
    public void loadLev(string level)
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(level);
    }
}
