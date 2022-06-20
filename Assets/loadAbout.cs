using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class aboutGroup
{
	public string aboutTitle;
	public TextAsset txtFile;
}
public class loadAbout : MonoBehaviour
{
	public aboutGroup[] abouts;
	public Animator aboutPanelAnim;
	public GameObject sharePanel;
	public string[] socials;

	[Header("Text Objects")]
	public TextMeshProUGUI titleText;
	public TextMeshProUGUI contentText;

	public void OpenDonate()
	{
		Application.OpenURL("https://paypal.me/******");
	}

	public void SetSocial(int on)
	{
		if (on == 0)
			sharePanel.SetActive(true);
		else
			sharePanel.SetActive(false);
	}

	public void OpenSocial(int socialNum)
	{
		Application.OpenURL(socials[socialNum]);
	}

	public void SetAbout(int num)
	{
		titleText.text = abouts[num].aboutTitle;
		contentText.text = abouts[num].txtFile.text;
		aboutPanelAnim.Play("about_open");
	}
}
