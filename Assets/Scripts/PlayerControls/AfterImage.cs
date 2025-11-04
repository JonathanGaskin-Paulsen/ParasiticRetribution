using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public float repeatRate = 0.2f;
    public float lifetime = 0.5f;
    public float alpha = 1f;
    public bool enable = false;
    public static AfterImage instance;
    public bool color = false;
    int currentColor = 0;


    private float initializationTime;
    void Awake()
    {   
        if(instance == null){
            InvokeRepeating("SpawnTrail", 0, repeatRate);
            instance = this;
        }
        
    }
    void Update(){
   
        
    }
    void SpawnTrail()
    {
        if (enable) {
            GameObject trailPart = new GameObject();
            SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
            trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
            trailPartRenderer.sortingLayerName = "Player";
            trailPartRenderer.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
            trailPart.transform.position = transform.position;
            trailPart.transform.localScale = transform.localScale;
            Destroy(trailPart, lifetime);
            if(color){
                
                StartCoroutine("FadeTrailPartColor", trailPartRenderer);
            }
            else{
                StartCoroutine("FadeTrailPart", trailPartRenderer);
            }
        }
       
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a = alpha;
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }
    IEnumerator FadeTrailPartColor(SpriteRenderer trailPartRenderer)
    {
        float startTime = Time.time;
        int nextColor = (currentColor + 1) % 7;
        
        Color[] colors  = new Color[]{
            new Color(1f, 0f, 0f),
            new Color(1f, 0.5f, 0f),
            new Color(1f, 1f, 0f),
            new Color(0f, 1f, 0f),
            new Color(0f, 0f, 1f),
            new Color(0.5f, 0f, 1f),
            new Color(1f, 0f, 1f)
        };


        Shader shaderGUItext = Shader.Find("GUI/Text Shader");
      
        Color current = colors[currentColor % 7]; //Color.Lerp(colors[currentColor % 7], colors[nextColor], 0.5f);
        Color next = colors[(currentColor+1) % 7];
        trailPartRenderer.material.shader = shaderGUItext;
        trailPartRenderer.color = Color.Lerp(current,next,(Random.Range(0, 360) % 360)/360.0f);
      

        currentColor++;
        yield return new WaitForEndOfFrame();
    }
}
