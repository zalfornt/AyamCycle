using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitionManagement : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
