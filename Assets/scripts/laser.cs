using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField] private float _laserSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // move the laser upward
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        // if the laser is above 7f AKA ( off the screen) destroy the laser
        if (transform.position.y > 6f)
        {
            if (transform.parent == null)
            {
                // if the laser has no parent destroy it.
                Destroy(this.gameObject);
            }
            else
            {
                // if the laser has parent destroy it and destroy the parent.
                Destroy(this.gameObject);
                Destroy(this.transform.parent.gameObject);
            }
        }


    }
}
