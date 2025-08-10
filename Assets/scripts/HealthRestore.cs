using UnityEngine;

public class HealthRestore : MonoBehaviour
{
    [SerializeField] private int restoreValue = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("hr");
        Destructible cat = collision.transform.root.GetComponent<Destructible>();
        if (cat.GetComponent<Cat>() != null)
        {
            if (cat)
            {
                cat.AddHitPoints(restoreValue);
                Destroy(transform.gameObject);
            }
        }
        
    }
}
