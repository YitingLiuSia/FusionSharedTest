using UnityEngine;

namespace SiaX.Utility { 
public class Slideshow : MonoBehaviour
{
    public Texture2D[] slideshowContent;
    public float slideTime;

    public Material localMaterial;
    public int indexActive;

    private float currentTime;
    private void Start()
    {
        localMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        Timer();
        
        localMaterial.mainTexture = slideshowContent[indexActive];
    }

    public void Timer()
    {
        currentTime += Time.deltaTime;
        
        if (currentTime >= slideTime)
        {
            indexActive += 1;

            //LOOP
            if (indexActive >= slideshowContent.Length)
            {
                indexActive = 0;
            }

            currentTime = 0;
        }
    }
}
}