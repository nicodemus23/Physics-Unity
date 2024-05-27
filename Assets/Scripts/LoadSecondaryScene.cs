using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSecondaryScene : MonoBehaviour
{
    private void Start()
    {
        // Load the secondary scene additively
        SceneManager.LoadScene("SecondarySceneName", LoadSceneMode.Additive);
    }
}