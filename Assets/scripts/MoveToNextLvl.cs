using System.Collections;
using System.Collections.Generic;
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
        print("street");
        if (collision != null)
        {
            print("collision");
            Cat cat = collision.transform.root.GetComponent<Cat>();
            

            if (cat != null)
            {
                //print("cat");
                int sceneInd = SceneManager.GetActiveScene().buildIndex;
                print($"active sceneInd {sceneInd} q {SceneManager.sceneCount}");

                if (directionDoor == DirectionDoor.ToNext && sceneInd <= EditorBuildSettings.scenes.Length)
                {
                    sceneInd++;
                }
                else if (directionDoor == DirectionDoor.ToPrevious && sceneInd > 0)
                {
                    sceneInd--;
                }
                else return;
                    //Player.isTeleported = true;
                    SceneManager.LoadSceneAsync(sceneInd);
                print("!!!!street");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Player.isTeleported = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision != null)
        //{
        //    Cat cat = collision.GetComponent<Cat>();
        //    if (cat != null)
        //    {
        //        int sceneInd = SceneManager.GetActiveScene().buildIndex;
        //        //SceneManager.LoadSceneAsync(nameof(Scenes.GameScene_Street));
        //        print("street");
        //    }
        //}
    }
}
