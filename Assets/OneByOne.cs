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
    float _currentWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        CanSwitch = true;
        cpt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSwitch && cpt < Players.Count)
        {
            if (cpt == 0)
                GiveToCoroutine(0f, Quads[cpt], Players[cpt]);
            else if (cpt == 1)
                GiveToCoroutine(WaitTime1, Quads[cpt], Players[cpt]);
            else if (cpt == 2)
                GiveToCoroutine(WaitTime2, Quads[cpt], Players[cpt]);
        }
    }

    void GiveToCoroutine(float waitTime, GameObject quad, VideoPlayer player)
    {
        CanSwitch = false;
        cpt++;
        _currentWaitTime = waitTime;
        StartCoroutine(Wait(quad, player));
        StartCoroutine(DeleteAfterPlay(quad, player));
    }

    IEnumerator Wait(GameObject quad, VideoPlayer player)
    {
        yield return new WaitForSeconds(_currentWaitTime);
        player.gameObject.SetActive(true);
        quad.SetActive(true);
        CanSwitch = true;
    }

    IEnumerator DeleteAfterPlay(GameObject quad, VideoPlayer player)
    {
        while (!player.isPaused)
        {
            yield return null;

        }
        quad.SetActive(false);
        player.frame = 0;

        if (!Players.Exists(p => !p.isPaused))
            FindObjectOfType<ARTapToPlaceObject>().PlaceObject();
    }
}
