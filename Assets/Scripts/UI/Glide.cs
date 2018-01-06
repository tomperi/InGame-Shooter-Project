using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glide : MonoBehaviour
{
    public int dieAfterCount;
    private int count;

    void dieAfter()
    {
        if (dieAfterCount == count)
        {
            Destroy(this.gameObject);
        }
        count++;
    }

}
