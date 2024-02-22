using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queBlockScript : MonoBehaviour
{

    Renderer rend;
    float offset = 0f;

    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {

        // Fix Later????

        offset += 0.2f;
        // rend.material.SetTextureOffset("gold_block", new Vector2(0, offset));

    }

    public void gotCoin()
    {

    }
}
