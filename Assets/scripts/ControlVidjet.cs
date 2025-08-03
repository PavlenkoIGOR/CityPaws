using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlVidjet : MonoBehaviour
{
    Vector3 setposition;
    float _tmpTime;
    float _time = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateControlsVidget();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tmpTime <= _time)
        {
            transform.position = new Vector3(Player.instance.ActiveCat.transform.position.x, Player.instance.ActiveCat.transform.position.y + 2.0f, Player.instance.ActiveCat.transform.position.z);
            _tmpTime += Time.deltaTime;
        }
        else { transform.gameObject.SetActive(false); }
    }

    void InstantiateControlsVidget()
    {
        setposition = Player.instance.ActiveCat.transform.position;
        setposition.y = setposition.y + 20.0f;
        transform.position = setposition;
    }
}
