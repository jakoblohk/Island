using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ensures that the Music Control GameObject is not deleted between the scenes
// and at the same time it ckecks that the GameObject is not duplicated if one reloads the menu screen
public class Music_control_script : MonoBehaviour
{
    // creates MusicControlScript instance
    public static Music_control_script instance;

    // Runs before void Start()
    private void Awake()
    {
        // Don't destroy this gameObject when loading different scenes
        DontDestroyOnLoad(this.gameObject);

        // If the MusicControlScript instance variable is null, it playes music
        // If there is already a MusicControlScript instance active it destroys the object
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}