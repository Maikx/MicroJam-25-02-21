using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    /// <summary>
    /// This Reloads the scene when the player is out of bounds
    /// </summary>
    public void ReloadScene()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
