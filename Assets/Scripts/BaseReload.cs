using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseReload : MonoBehaviour
{
    private void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
