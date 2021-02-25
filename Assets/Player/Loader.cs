using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        Reload();
    }

    /// <summary>
    /// This Reloads the scene when the player is out of bounds
    /// </summary>
    void Reload()
    {
        if(player.isOutBounds)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
