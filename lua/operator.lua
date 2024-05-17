print(3+1)
print(3-1)
print(3*1)
print(5/3)-- 浮点除法
print(5//3)-- 向下取整除法 相当于 对结果 floor
print(7%3)
print(3^3)
print(-2)

-- 不等于 ~=
local a,b =10,20
print(a ~= b)

-- 遵循短路逻辑

local a  =  ( 10 or 20 )            --> 10
print(a)
a = 10 or error()       --> 10
print(a)
a = nil or "a"          --> "a"
print(a)
a = nil and 10          --> nil
print(a)
a = false and error()   --> false
print(a)
a = false and nil       --> false
print(a)
a = false or nil        --> nil
print(a)
a = 10 and 20           --> 20
print(a)


-- 字符串拼接符号 ..
local a = "hello"
print(a .. "world")

-- 长度操作符
print(#"hello world")

local t = {10, nil, 30, nil}
print(#t)
local mt = {
    __len = function ()
        return 4
    end
}
setmetatable(t, mt)
print("==> ", getmetatable(t).__len())
print(#t)
