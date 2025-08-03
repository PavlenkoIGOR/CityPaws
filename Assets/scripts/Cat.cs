using UnityEngine;
using UnityEngine.Events;

public class Cat : MonoBehaviour
{
    public UnityEvent EventOnDeath;

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            Player.canMove = true;
        }
        if (Player.canMove)
        {
            transform.GetComponent<CatController>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Star star = collision.GetComponent<Star>();
            if (star != null)
            {
                Player.instance.AddStar(1);
                Destroy(star.gameObject);
            }
        }
    }
}
