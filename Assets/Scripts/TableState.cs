using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableState : MonoBehaviour
{

    public bool hasSeed = false;
    public GrowthState state = GrowthState.EMPTY;
    public enum GrowthState
    {
        EMPTY,
        STAGE1,
        STAGE2,
        FULL
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
