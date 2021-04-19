using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfInTime : MonoBehaviour
{
    public GameObject explodeParticle;
    private float explodeTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        explodeTimer -= Time.deltaTime;
        DestroyAndExplode();
    }

    private void DestroyAndExplode() {
        if (explodeTimer < 0) {
            if (explodeParticle) {
                Instantiate(explodeParticle, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
