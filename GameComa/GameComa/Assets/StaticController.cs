using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaticController : MonoBehaviour
{

    private static bool created = false;

    public bool isBody;
    public string ip;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    public void Connect(string type)
    {
        isBody = type == "body";
        var ipTxt = GameObject.FindGameObjectWithTag("IP");
        if(ipTxt != null)
        {
            ip = ipTxt.GetComponent<Text>().text;
        }

        SceneManager.LoadScene(1);

    }

}
