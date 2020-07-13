using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
    private GameObject Loading_bar;
    private GameObject Crossfade;
    Animator anim;
    private float aux_d=0;
    private int Random_a = 0;//Limit a for random loading time
    private int Random_b = 1 ;//Limit b for random loading time
    private bool loading = true;
    // Use this for initialization
    void Start () {
        Loading_bar = GameObject.Find ("Bar");
        Crossfade = GameObject.Find("Crossfade");
        anim = Crossfade.GetComponent<Animator> ();
        aux_d = Loading_bar.GetComponent<Renderer>().bounds.size.x;
        Invoke ("Loading_Event", 1);//Time waiting for start loading
    }

    void Loading_bar_Down(){
        float aux_c = Loading_bar.GetComponent<Renderer>().bounds.size.x;
        Loading_bar.transform.localScale += new Vector3(-aux_d/Random.Range(70, 230), 0, 0);
        Vector3 auxve = new Vector3(Loading_bar.transform.position.x,Loading_bar.transform.position.y,Loading_bar.transform.position.z);
        auxve.x=auxve.x-(aux_c - Loading_bar.GetComponent<Renderer>().bounds.size.x)/2;
        Loading_bar.transform.position=auxve;
        loading = false;
        Loading_bar_End();
    }
    void Loading_bar_End(){
        if((Loading_bar.GetComponent<Renderer>().bounds.size.x<=0)||(Loading_bar.transform.localScale.x<=0)){
            Vector3 aux_v = new Vector3(0,0,0);
            Loading_bar.transform.localScale = aux_v;
            Invoke ("Exit_",1);
        }
    }

    void Exit_(){
        anim.SetBool("out",true);
        Invoke ("Next_Scene",2);
    }

    void Loading_Event(){
        loading = false;
    }

    void Next_Scene(){
        Application.LoadLevel("02_UGUI_Title");
    }

    void FixedUpdate(){
        if(loading ==false){
            int aux = Random.Range(Random_a, Random_b);
            loading = true;
            Invoke("Loading_bar_Down",aux);
        }

    }
    // Update is called once per frame
    void Update () {
	
    }
}