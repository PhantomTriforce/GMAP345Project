using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour {
    
    public void OnClickHome()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickGame()
    {
        SceneManager.LoadScene("PatrickScene");
    }
}