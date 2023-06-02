using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Smoke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(Anim());  
    }

   
    public IEnumerator Anim()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        Destroy(gameObject);
    }
}
