using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FIMSpace.FTail;

public class TailController : MonoBehaviour
{
    #region Singleton
    public static TailController instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion
    public TailAnimator2 sagTail, solTail;
 
}
