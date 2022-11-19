using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Say_Hard : MonoBehaviour
{
    public Animator anim;
    int i =0;

    public Image img;

    public static bool yes;

    private void Start()
    {
        yes = false;
    }

    private void Update()
    {
        if (BodySourceViewHh.i == 1)
        {
            anim.SetBool("love", true);
            anim.StartPlayback();
        }
        else { 
            anim.StopPlayback();
        }

        if (img.fillAmount >= 0.5f) {
            yes = true;
        
        }

        if (img.fillAmount>=0.86f){
            Invoke("wait", 3);
        }
    }

    void wait() { 
            SceneManager.LoadScene("final start");

    }
}