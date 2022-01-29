using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using XLua;

public class xLuaMgr : Singleton<xLuaMgr>
{
    LuaEnv env = null;
    private bool isGameStarted = false;
    private static string luaScriptFolder = "LuaScripts";

    public override void Awake()
    {
        base.Awake();
        InitLuaEnv();

    }

    public byte[] LuaScriptLoader(ref string filePath)
    {
        // Debug.Log("####" + filePath);
        string scriptPath = string.Empty;
        filePath = filePath.Replace(".", "/") + ".lua"; // game/init.lua

        scriptPath = Path.Combine(Application.dataPath, luaScriptFolder);
        scriptPath = Path.Combine(scriptPath, filePath);

        byte[] data = GameUtility.SafeReadAllBytes(scriptPath);
        return data;
    }

    private void InitLuaEnv()
    {
        env = new LuaEnv();
        env.AddLoader(LuaScriptLoader);
        isGameStarted = false;
    }

    public void EnterGame()
    {
        // Enter game logic
        isGameStarted = true;

        // Load and run Lua scripts
        // Lua code: print("hello world!")
        env.DoString("require(\"main\")");
        env.DoString("main.init()");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (this.isGameStarted)
        {
            env.DoString("main.update()");
        }
    }

    public void FixedUpdate()
    {
        if (this.isGameStarted)
        {
            env.DoString("main.fixedUpdate()");
        }
    }
}
