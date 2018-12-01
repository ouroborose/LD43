using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuLib
{
    public interface IShowHide
    {
        void Show(bool instant = false);
        void Hide(bool instant = false);
    }
}

