using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollisionCheck
{
    public static string PLAYERTAG = "Player";
    public static string ENEMYTAG = "Enemy";
    public static string CLOWNTAG = "Clown";
    public static string NPCTAG = "NPC";
    public static string BLOCKERTAG = "Blocker";
    public static string GROUNDTAG = "Ground";
    public static string BULLETTAG = "Bullet";
    public static string BGTAG = "BG";

    public static bool CheckIfBG(GameObject gameObject)
    {
        if (gameObject.tag == BGTAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckIfBullet(GameObject gameObject)
    {
        if (gameObject.tag == BULLETTAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckIfGround(GameObject gameObject)
    {
        if (gameObject.tag == GROUNDTAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckIfPlayer(GameObject gameObject)
    {
        if(gameObject.tag == PLAYERTAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckIfEnemy(GameObject gameObject)
    {
        if (gameObject.tag == ENEMYTAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckIfClown(GameObject gameObject)
    {
        if (gameObject.tag == CLOWNTAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckIfNPC(GameObject gameObject)
    {
        if (gameObject.tag == NPCTAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckIfBlocker(GameObject gameObject)
    {
        if (gameObject.tag == BLOCKERTAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
