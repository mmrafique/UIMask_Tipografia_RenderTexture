using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");

        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
