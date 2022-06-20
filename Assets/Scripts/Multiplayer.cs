using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Multiplayer : MonoBehaviour
{
	public dataStorer gameFile;
	public void saveData(dataStorer SaveData)
    {
		string filePath = Application.persistentDataPath+"/fileSave.MemoryMaya";
		FileStream dataStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter Converter = new BinaryFormatter();
		Converter.Serialize(dataStream, SaveData);
		dataStream.Close();
    }
	// Use this for initialization
	public void sendData()
    {
		saveData(gameFile);
		StartCoroutine(Upload());
	}
	public void getData()
    {
		StartCoroutine(Download());
    }
	IEnumerator Download()
	{
		WWWForm Form = new WWWForm();
		Form.AddField("Direction", "Down");
		Form.AddField("GameName", "memory-maya");
		Form.AddField("Gameid", "testing");
		using (UnityWebRequest request = UnityWebRequest.Post("https://zenhap.org/devs/arjun/multiplayer.php", Form))
		{
			yield return request.SendWebRequest();
			if (request.isNetworkError || request.isHttpError)
			{
				Debug.Log(request.error);
			}
			else
			{
				Debug.Log(request.downloadHandler.text);

			}
		}
	}
	IEnumerator Upload()
	{
		WWWForm Form = new WWWForm();
		string filePath = Application.persistentDataPath + "/fileSave.MemoryMaya";
		StreamReader sr = new StreamReader(filePath);
		string FileContents = sr.ReadToEnd();
		Form.AddField("Direction", "Up");
		Form.AddField("GameName", "memory-maya");
		Form.AddField("Gameid", "testing");
		Form.AddField("GameData", FileContents);
		sr.Close();
		using (UnityWebRequest request = UnityWebRequest.Post("https://zenhap.org/devs/arjun/multiplayer.php", Form))
		{
			yield return request.SendWebRequest();
			if (request.isNetworkError || request.isHttpError)
			{
				Debug.Log(request.error);
			}
			else
			{
				Debug.Log(request.downloadHandler.text);

			}
		}
	}
}