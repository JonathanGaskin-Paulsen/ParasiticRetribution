using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
   public bool isTransitioning = true;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioListener.volume = 0;
        StartCoroutine(WaitForRoomLoad(2f));
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveScene(string sceneName, float duration)
    {
        if (isTransitioning)
        {
            //Do Nothing
        }
        else
        {
            isTransitioning = true;
            StartCoroutine(Transition(sceneName, duration));
        }
    }

    IEnumerator Transition(string sceneName, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(1, 0, elapsedTime / duration);
            float alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            Color newColor = new Color(image.color.r, image.color.g, image.color.b, alpha);
            image.color = newColor;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }


    IEnumerator WaitForRoomLoad(float T)
    {
        yield return new WaitUntil(() => ((RoomController.instance.loadedRooms.Count != 0) && (RoomController.instance.roomQueue.Count == 0)));
        yield return new WaitForSeconds(T); //Handles race condition with the boss room since it is the last one
        float duration = 0.5f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume =  Mathf.Lerp(0, 1, elapsedTime / duration);
            float alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            Color newColor = new Color(image.color.r, image.color.g, image.color.b, alpha);
            image.color = newColor;
            yield return null;
        }
        isTransitioning = false;
    }
    
}
