using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        // Make the cursor visible
        Cursor.visible = true;
    }
    // Start is called before the first frame update
    public void restartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
