using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DolphinPlayer.UI
{
    public class UINormal : UIWindowBase
    {
        private static string BTN_COMPASS = "btnCompass";
        private static string BTN_MY_MUSIC = "btnMyMusic";
        private static string BTN_FRIEND = "btnFriend";
        private static string BTN_ACCOUNT = "btnAccount";

        private static string BTN_LOCALMUSIC = "localMusic";

        public override void Init()
        {
            base.Init();
            
        }

        // Use this for initialization
        override public void Start()
        {

        }

        // Update is called once per frame
        override public void Update()
        {

        }


        public void OnMyMusic()
        {

        }

        override public void OnButtonClick(string strObjName)
        {
            if(strObjName == BTN_MY_MUSIC)
            {
                this.OnMyMusic();
            }
            else if(strObjName == BTN_COMPASS)
            {

            }
            else if(strObjName == BTN_FRIEND)
            {

            }
            else if(strObjName == BTN_ACCOUNT)
            {

            }
        }
    }
}

