main = {}

local GameApp = require("Game.GameApp")

main.init = function ()
    -- init Lua framework modules
    -- print("init")
    -- end

    -- Enter game logic
    GameApp.EnterGame()
end

main.update = function ()
    -- print("update")
end

main.fixedUpdate = function ()
    -- print("fixed update")
end

