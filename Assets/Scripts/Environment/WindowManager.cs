using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameObject glass;
    private AudioSource audioSource;
    public AudioClip breakSound;
    public GameObject glassParticles;
    private bool isbreak = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isbreak && collision.gameObject.tag == "bulletNormal" || collision.gameObject.tag == "bulletPenetrate" || collision.gameObject.tag == "bulletBounce")
        {
            glassParticles.SetActive(true);
            isbreak = true;
            Destroy(glass);
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(breakSound);
        }
    }
}
