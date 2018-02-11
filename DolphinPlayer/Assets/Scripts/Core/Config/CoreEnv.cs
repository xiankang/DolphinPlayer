using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Config
{
    public static class CoreEnv
    {

        public static string _Game = "";

        static CoreEnv()
        {
            _Game = "DolphinPlayer";
        }
    }
}

