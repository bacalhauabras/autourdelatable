using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OneByOne : MonoBehaviour
{
    public List<VideoPlayer> Players;
    public List<GameObject> Quads;
    public float WaitTime1;
    public float WaitTime2;
    bool CanSwitch;
    int cpt;
    // Start is called before the first frame update
    void Start()
    {
        CanSwitch = true;
        cpt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSwitch)
            Wait(WaitTime1, Quads[cpt++], Players[cpt++]);
    }

    IEnumerator Wait(float waitTime, GameObject quad, VideoPlayer player)
    {
        CanSwitch = false;
        yield return new WaitForSeconds(waitTime);

        player.gameObject.SetActive(true);
        quad.SetActive(true);
        CanSwitch = true;
    }
}
