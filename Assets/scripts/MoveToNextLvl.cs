using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DirectionDoor
{
    ToNext,
    ToPrevious
}
public class MoveToNextLvl : MonoBehaviour
{
    public DirectionDoor directionDoor;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            Cat cat = collision.transform.root.GetComponent<Cat>();


            if (cat != null)
            {
                int sceneInd = SceneManager.GetActiveScene().buildIndex;

                if (directionDoor == DirectionDoor.ToNext && sceneInd <= EditorBuildSettings.scenes.Length)
                {
                    PlayerPrefs.SetString("isGoingBack", "false");
                    sceneInd++;
                }
                else if (directionDoor == DirectionDoor.ToPrevious && sceneInd > 0)
                {
                    PlayerPrefs.SetString("isGoingBack", "true");
                    sceneInd--;
                }
                else return;

                if (Player.instance.ActiveCat != null)
                    Destroy(Player.instance.ActiveCat.gameObject);
                SceneManager.LoadSceneAsync(sceneInd);
            }
        }
    }

}
