using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderwebController : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Burn(GameObject player)
    {
        if (!player.GetComponent<PlayerController>().HasTorch())
        {
            TextManager.Instance.ShowTextOverCharacter("Necesito fuego para quemar esto");
            return;
        }

        StartCoroutine(BurnAnimation());
        
    }

    IEnumerator BurnAnimation()
    {
        anim.SetTrigger("Burn");
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);

    }
}
