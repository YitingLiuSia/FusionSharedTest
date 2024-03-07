using UnityEngine;

public class KeyController : MonoBehaviour
{
    public KeyCode playKey;
    public KeyCode stopKey;
    public KeyCode selectIndexOneKey;
    public KeyCode selectIndexTwoKey;
    [SerializeField] TimelineController timelineController;
    void Update()
    {
        if (Input.GetKeyDown(playKey))
        {
            timelineController.Play();
        }
        if (Input.GetKeyDown(stopKey))
        {
            timelineController.Stop();
        }
        if (Input.GetKeyDown(selectIndexOneKey))
        {
            timelineController.PlayFromIndex(0);
        }
        if (Input.GetKeyDown(selectIndexTwoKey))
        {
            timelineController.PlayFromIndex(1);

        }
    }
}
