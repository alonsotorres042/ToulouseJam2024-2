using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCalls : MonoBehaviour
{
    [SerializeField] GameObject _call;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            _call.SetActive(true);
        }
        
        
    }
}
