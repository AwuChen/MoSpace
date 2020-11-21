/*
 * Copyright 2016, Gregg Tavares.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     * Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above
 * copyright notice, this list of conditions and the following disclaimer
 * in the documentation and/or other materials provided with the
 * distribution.
 *     * Neither the name of Gregg Tavares. nor the names of its
 * contributors may be used to endorse or promote products derived from
 * this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Collections.Generic;
using Byn.Awrtc;
using Byn.Awrtc.Unity;
using Byn.Unity.Examples;

public class ClickAndGetSpriteImage : MonoBehaviour
{
    string picUrl;
    public Image photo;
    public GameObject picture;
    public GameObject imageParent;
    int photoCount = 0;

    //useful for any gameObject to access this class without the need of instances her or you declare her
    public static ClickAndGetImage instance;


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Image photos = Instantiate(photo, new Vector3(0, 0, 0), Quaternion.identity, imageParent.transform) as Image;
        //    photos.transform.localScale = new Vector3(0.9302326f, 0.9302326f, 12.16841f);
        //}
    }

    private void Start()
    {
        //NetworkManager.instance.CheckPic();
    }
    public void OnClick()
    {

        // NOTE: gameObject.name MUST BE UNIQUE!!!!
        GetImage.GetImageFromUserAsync(gameObject.name, "SendImage");

    }

    static string s_dataUrlPrefix = "data:image/png;base64,";
    public void SendImage(string dataUrl)
    {
        if (dataUrl.StartsWith(s_dataUrlPrefix))
        {
            byte[] pngData = System.Convert.FromBase64String(dataUrl.Substring(s_dataUrlPrefix.Length));


            // Create a new Texture (or use some old one?)
            Texture2D tex = new Texture2D(1, 1); // does the size matter?
            if (tex.LoadImage(pngData))
            {
                SendPic(dataUrl);
                UpdateInGamePic(dataUrl);
            }
            else
            {
                Debug.LogError("could not send decode image");
            }

        }
        else
        {
            Debug.LogError("Error sending image:" + dataUrl);
        }
    }

    void UpdateInGamePic(String dataUrl)
    {
        if (dataUrl.StartsWith(s_dataUrlPrefix))
        {
            byte[] pngData = System.Convert.FromBase64String(dataUrl.Substring(s_dataUrlPrefix.Length));

            // Create a new Texture (or use some old one?)
            Texture2D tex = new Texture2D(1, 1); // does the size matter?
            if (tex.LoadImage(pngData))
            {
                Renderer renderer = picture.GetComponent<Renderer>();
                renderer.material.mainTexture = tex;
            }
        }
    }

    static string r_dataUrlPrefix = "data:image/png;base64,";
    public void ReceiveImage(string dataUrl)
    {
        if (dataUrl.StartsWith(r_dataUrlPrefix))
        {
            byte[] pngData = System.Convert.FromBase64String(dataUrl.Substring(r_dataUrlPrefix.Length));


            // Create a new Texture (or use some old one?)
            Texture2D tex = new Texture2D(1, 1); // does the size matter?
            if (tex.LoadImage(pngData))
            {
                //Renderer renderer = photo.GetComponent<Renderer>();

                //renderer.material.mainTexture = tex;
                //picUrl = dataUrl;
                //SendPic(picUrl);

                Image photos = Instantiate(photo, new Vector3(0, 0, 0), Quaternion.identity, imageParent.transform) as Image;
                photos.transform.localScale = new Vector3(0.9302326f, 0.9302326f, 12.16841f);

                photos.sprite = Sprite.Create(tex, photos.sprite.rect, new Vector2(0.5f, 0.5f));

                // update name, main texture and shader, these all seem to be required... even thou you'd think it already has a shader :|
                photos.sprite.name = photos.name + "_sprite";
                //photos.material.mainTexture = tex as Texture;
                //photos.material.shader = Shader.Find("UI/Default");
                //if (photoCount < spritePhoto.Length)
                //{

                //    // replace the current sprite with the desired sprite, but using the loaded sprite as a cut out reference via 'rect'
                //    spritePhoto[photoCount].sprite = Sprite.Create(tex, spritePhoto[photoCount].sprite.rect, new Vector2(0.5f, 0.5f));

                //    // update name, main texture and shader, these all seem to be required... even thou you'd think it already has a shader :|
                //    spritePhoto[photoCount].sprite.name = spritePhoto[photoCount].name + "_sprite";
                //    spritePhoto[photoCount].material.mainTexture = tex as Texture;
                //    spritePhoto[photoCount].material.shader = Shader.Find("Sprites/Default");

                //    photoCount++;
                //}
                //UpdateInGamePic(dataUrl);

            }
            else
            {
                Debug.LogError("could not decode image");
            }
        }
        else
        {
            Debug.LogError("Error getting image:" + dataUrl);
        }
    }

    public void SendPic(string bytes, bool reliable = true)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["pic"] = bytes;
        NetworkManager.instance.SavePic(data);
    }

    public void ReceiveIncommingPhoto(string msg)
    {
        string r_dataUrlPrefix = "data:image/png;base64,";
        // if photo ... 
        if (msg.StartsWith(r_dataUrlPrefix))
        {
            ReceiveImage(msg);
        }
    }

    public void UpdateInGamePhoto(string msg)
    {
        string r_dataUrlPrefix = "data:image/png;base64,";
        // if photo ... 
        if (msg.StartsWith(r_dataUrlPrefix))
        {
            UpdateInGamePic(msg);
        }
    }
}



