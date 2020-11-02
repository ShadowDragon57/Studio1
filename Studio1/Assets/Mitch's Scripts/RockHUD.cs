using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHUD : MonoBehaviour
{
    public Texture rockHUD0;
    public Texture rockHUD1;
    public Texture rockHUD2;
    public Texture rockHUD3;
    public Texture rockHUD4;
    public Texture rockHUD5;
    public Texture rockHUD6;
    public Texture rockHUD7;
    public Texture rockHUD8;
    public Texture rockHUD9;
    public Texture rockHUD10;
    public Texture rockHUD11;
    public Texture rockHUD12;
    public Texture rockHUD13;
    public Texture rockHUD14;
    public Texture rockHUD15;
    public Texture rockHUD16;

    // Update is called once per frame
    public Texture returnRock(int rocksLeft)
    {
        Texture TextureSelected = rockHUD0; 
        switch(rocksLeft)
         {
            case 0:
                TextureSelected = rockHUD0;
                break;
            case 1:
                TextureSelected = rockHUD1;
                break;
            case 2:
                TextureSelected = rockHUD2;
                break;
            case 3:
                TextureSelected = rockHUD3;
                break;
            case 4:
                TextureSelected = rockHUD4;
                break;
            case 5:
                TextureSelected = rockHUD5;
                break;
            case 6:
                TextureSelected = rockHUD6;
                break;
            case 7:
                TextureSelected = rockHUD7;
                break;
            case 8:
                TextureSelected = rockHUD8;
                break;
            case 9:
                TextureSelected = rockHUD9;
                break;
            case 10:
                TextureSelected = rockHUD10;
                break;
            case 11:
                TextureSelected = rockHUD11;
                break;
            case 12:
                TextureSelected = rockHUD12;
                break;
            case 13:
                TextureSelected = rockHUD13;
                break;
            case 14:
                TextureSelected = rockHUD14;
                break;
            case 15:
                TextureSelected = rockHUD15;
                break;
            case 16:
                TextureSelected = rockHUD16;
                break;
        }
        return TextureSelected;
    }
}
