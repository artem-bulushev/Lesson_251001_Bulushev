using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestarLevel()
    {
        SceneManager.LoadScene("GreyBox");
    }
}
