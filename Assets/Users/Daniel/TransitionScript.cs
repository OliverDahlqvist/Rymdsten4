using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that is used by objects for exiting a level.
/// Object using this script must be on Transition layer!
/// </summary>
public class TransitionScript : MonoBehaviour
{
    //To which level are we going to?
    public int TargetedSceneIndex;

    public Transform TargetPlayerLocation;

    public void Interact()
    {
        //Assign the transition target location.
        GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;

        //NEW:
        GlobalControl.Instance.IsSceneBeingTransitioned = true;
        GlobalControl.Instance.FireSaveEvent();

        SceneManager.LoadScene(TargetedSceneIndex);
    }
}