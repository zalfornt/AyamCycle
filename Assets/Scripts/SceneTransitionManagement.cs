using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitionManagement : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
     public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
