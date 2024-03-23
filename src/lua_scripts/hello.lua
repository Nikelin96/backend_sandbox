-- todo learn how to use luarocks lua-cjson
local function readFromFile(path)
    local file = io.open(path, "r")
    if file then
        local content = file:read("*a")
        file:close()
        return content
    else
        return nil
    end
end

local hello = "Hi Mom"
print(hello)
local res = readFromFile('input.txt')
print(res)
