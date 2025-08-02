using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InputActionSystem inputActions;

    private void Awake()
    {
        inputActions = new InputActionSystem();
        inputActions.Enable(); // Make sure it's enabled
    }
    void Update()
    {
        if (inputActions.Controls.ExitGame.WasPerformedThisFrame())
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }   
     }
}
