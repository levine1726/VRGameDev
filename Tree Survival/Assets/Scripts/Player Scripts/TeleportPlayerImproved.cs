using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportPlayerImproved : MonoBehaviour
{

    public LineRenderer linePrefab;
    public float offsetFromTree = 1f;
    public Dictionary<Arrow, LineRenderer> arrowLines = new Dictionary<Arrow, LineRenderer>();
    


    private void FixedUpdate()
    {
        if (!Arrow.haveEventsBeenSubscribedTo)
        {
            Arrow.OnArrowReleased += Arrow_OnArrowReleased;
            Arrow.OnArrowLanded += Arrow_OnArrowLanded;
            Arrow.haveEventsBeenSubscribedTo = true;
        }
    }

    private void Arrow_OnArrowReleased(Arrow me, string collisionName, Transform treeTeleportationTransform)
    {
        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 0;
        arrowLines.Add(me, line);

        StartCoroutine(ArrowTimeOut(me, line));
    }

    private void Arrow_OnArrowLanded(Arrow me, string collisionName, Transform treeTeleportationTransform)
    {
        LineRenderer l = arrowLines[me];

        arrowLines.Remove(me);
        Destroy(me.gameObject);
        StopAllCoroutines();
        if (!EndGameDetection.endgameTriggered)
        {
            if(collisionName == "Tree")
            {


                Destroy(l.gameObject);

                Player.instance.transform.position = treeTeleportationTransform.position;
            }
            else
            {

                
                Debug.Log("line renderer destroyed");
                if (ArrowHand.GetIsArrowInAir())
                {
                    ArrowHand.SetIsArrowInAir(false);
                }
            }
        }

        if (l != null)
        {
            Destroy(l.gameObject);
        }
    }

    //IEnumerator ArrowTeleport(LineRenderer l, Transform arrowTip)
    //{
    //    Vector3[] pos = new Vector3[l.positionCount];
    //    l.GetPositions(pos);
    //    for (int i = 0; i < pos.Length - 5; i += 5)
    //    {

    //        Player.instance.transform.position = l.GetPosition(i);
    //        yield return null;

    //    }
    //    Player.instance.transform.position = arrowTip.position;

    //    if (ArrowHand.GetIsArrowInAir())
    //    {
    //        ArrowHand.SetIsArrowInAir(false);
    //        Debug.Log("Arrow terminated from arrowTimeOut method");
    //    }



    //}

    IEnumerator ArrowTimeOut(Arrow me, LineRenderer l)
    {
        if (l != null)
        {
            yield return new WaitForSecondsRealtime(10f);
            arrowLines.Remove(me);
            Destroy(me);
            Destroy(l);
            if (ArrowHand.GetIsArrowInAir())
            {
                ArrowHand.SetIsArrowInAir(false);
                Debug.Log("Arrow terminated from arrowTimeOut method");
            }

        }

    }


    private void Update()
    {
        foreach (Arrow a in arrowLines.Keys)
        {
            if (a != null)
            {
                arrowLines[a].positionCount++;
                arrowLines[a].SetPosition(arrowLines[a].positionCount - 1, a.transform.position);
            }
        }
    }


}
