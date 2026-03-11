using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    Button button;
    //==== START ====
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(NewScene);
    }

    void NewScene()
    {
        SceneManager.LoadScene("Testing Workshop");
    }

}
