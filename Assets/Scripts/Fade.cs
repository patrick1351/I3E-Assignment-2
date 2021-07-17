using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    private Vector3 areaCordinate;
    public GameObject gameover;

    public void FadeOut()
    {
        animator.SetTrigger("Fade");
    }

    public void FadeIn()
    {
        player.transform.position = areaCordinate;
        animator.SetTrigger("Fade");
    }

    public void LocationCordinate(float x, float y, float z)
    {
        areaCordinate = new Vector3(x, y, z);
    }

    public void EndGameFade()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("end");
        

    }
}
