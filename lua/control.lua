local af = nil
if not af then 
    print("nil is false")
end


af = ''
if af then
    print("empty is true") 
end
-- 这里 exp 表示初始化  exp2表示最终状态  exp3 表示步长
for i= 0 , 10, 1 do
    print("i=",i)
end

function f(x)
    x = x+1
    return x
end 

f(1)

local m = 0
for x = 1 ,f(5) do
    print("x=",x) 
end

-- 通用 for 循环
a = {"one", "two", "three"}
for i, v in ipairs(a) do
    print(i, v)
end 

local i=0

while i <10 do
    print("while do", i)
    i = i+1
end

repeat
    print("util do" ,i)
    i =i +1
until i >15 -- 直到 该表达式 为 true 时 退出 循环

local a = 0
local b = 9 
if a ~=b then
    print("~ is false")
end

