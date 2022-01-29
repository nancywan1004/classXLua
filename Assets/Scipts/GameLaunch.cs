using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunch : Singleton<GameLaunch>
{

    public override void Awake()
    {
        base.Awake();
        // Init Game components: Lua Script, Sound Manager, Asset Manager, Network Manager
        gameObject.AddComponent<xLuaMgr>();
}

    IEnumerator checkHotUpdate()
    {
        yield return 0;
    }

    IEnumerator GameStart()
    {
        yield return StartCoroutine(checkHotUpdate());

        // Enter game, Lua VM, Start executing Lua logic
        // Debug.Log("Game start");
        xLuaMgr.Instance.EnterGame();    
    }
        

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
