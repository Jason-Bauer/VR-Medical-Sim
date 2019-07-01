using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTutorialScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTutorialScene()
    {
        Destroy(GameObject.Find("Player"));
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    void OnMouseDown()
    {
       // SceneManager.LoadScene("Tutorial", LoadSceneMode.Additive);
    }
}
