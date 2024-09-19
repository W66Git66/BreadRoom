using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class coinCollect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) 
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                //CoinManager.Instance.AddCoins(1);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
