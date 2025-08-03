using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarTextChanger : MonoBehaviour
{
    private TMP_Text _starsQuantityText;
    // Start is called before the first frame update
    void Start()
    {
        _starsQuantityText = GetComponent<TMP_Text>();  
    }

    // Update is called once per frame
    void Update()
    {
        _starsQuantityText.text = "x" + Player.instance.starsQuantity.ToString();
    }
}
