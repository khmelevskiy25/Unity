using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    public string userName;
    public string password;


    private void Start()
    {
        StartCoroutine(RegisterUser());
        Debug.Log(userName + password);
    }

    private IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", userName);
        form.AddField("Pass", password);

        var www = UnityWebRequest.Post("http://unitytest/", form);

        yield return www.SendWebRequest();

        if(www.error != null)
        {
           Debug.Log("Ошибка " + www.error);
           yield break;
        }

        StringBuilder sb = new StringBuilder();
        foreach (var dict in www.GetResponseHeaders())
            sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");

        Debug.Log("Сервер ответил " + sb.ToString());

        Debug.Log(www.downloadHandler.text);
    }
}
