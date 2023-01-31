using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticle;

    [SerializeField] private TextMeshPro tmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHitParticle(Transform transform , int dmg )
    {
     
            tmp.SetText("+"+ dmg.ToString());
      
            

        ParticleSystem p = Instantiate(hitParticle, transform.position + Vector3.right + (Vector3.forward *2), Quaternion.identity);
     
        p.gameObject.SetActive(true);
        p.transform.GetChild(0).GetComponent<Rigidbody>().velocity = Vector3.up;
        

    }


}

