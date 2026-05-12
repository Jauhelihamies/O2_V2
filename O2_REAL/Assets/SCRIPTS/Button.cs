using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour

{
    public string Level_NAME;
    public void OpenLevel()
    {
        SceneManager.LoadScene(Level_NAME);

    }
}
