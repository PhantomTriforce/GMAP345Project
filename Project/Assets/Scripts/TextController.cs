using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour {
    
    public void OnClickHome()
    {
        SceneManager.LoadScene("PatrickTest");
    }
}