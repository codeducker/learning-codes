function f(a, b)
    print("a=",a,"b=",b)
end
function g(a, b, ...)
    print("a=",a,"b=",b)
end
function r() return 1,2,3 end
function func(a,b,c)
    print("a=",a,"b=",b,"c=",c)
end

f(3)             -- a=3, b=nil
f(3, 4)          -- a=3, b=4
f(3, 4, 5)       -- a=3, b=4
f(r(), 10)       -- a=1, b=10
f(r())           -- a=1, b=2
func(r(),8,10)

g(3)             --  a=3, b=nil, ... -->  (nothing)
g(3, 4)          --  a=3, b=4,   ... -->  (nothing)
g(3, 4, 5, 8)    -- a=3, b=4,   ... -->  5  8
g(5, r())       -- a=5, b=1,   ... -->  2  3

local func = {}
function func:f(more)
    print("func",self,func.f,more)
end 

func:f(1)

sum  = function(a,b) 
    return a + b
end

print("sum",sum(1,2))

-- 当多返回函数作为 函数参数的最后一个值或者表达式的最后一个值，所有返回值都可用，否则只能使用返回值的第一位

local x,y,z = r()
print("x",x,"y",y,"z",z)

x,y,z= 2, 3, r()
print("x",x,"y",y,"z",z)

x,y,z= "hello", r() ,"match"
print("x",x,"y",y,"z",z)

-- 函数定义可变参数
function mutiParams(...)
    -- select 语法
    -- select(index,xxx) 返回指定位置以后的所有参数
    -- select("#",xxx) 返回所有接收到的参数的总个数
    print(select(1,...)) -- 返回所有不定参数值
    print(select("#",...)) -- 返回不定参数长度
    -- print(arg.n)
    local args = {...} -- /将不定参数赋值给单独的变量
    for k,v in pairs(args) do 
        print(k,v)
    end 
end 

mutiParams(1,2,3,4)

-- 使用 unpack 传递不定参数
function lockedNames()
    return "match", "word",1
end

function getValues(...)
    local args = {...}
    mutiParams(table.unpack(args)) -- 5.1 之前使用 unpack 之后的版本使用 table.unpack(args)
end 

getValues(10,121,23)


local functor= function(cb)
    cb()
end
 
local OnComplete = function(cb, ...)
    local agrs = {...}
    functor(function()
        cb(table.unpack(args))
    end)
end

-- 尾调用
-- 以下函数调用耗时比较久
-- function func2(i)
-- 	while i < 15 do
-- 		i = i + 1
-- 		print(i)
-- 		func2(i)
-- 	end
-- 	return i
-- end

-- func2(0)

-- 改变顺序，使用尾调函数，执行速度很快
function func3(i)
	if i > 15 then
		return i
	end
		
	i = i + 1
	print(i)
	func3(i)  -- func(i) + 1 不算尾调函数
end

-- func3(0)
local s= "hello"
print(true )
print(true and "bar" ) -- "bar"
print(false and "bar" or s) -- "hello"
print(s == "foo" and "bar" or s)

-- 头等值
do
    local oldprint = print
    -- 存储当前的print函数为oldprint
    function print(s)
        --[[重新定义print函数。新函数只有一个实际参数。
        平常的print函数仍可以通过oldprint使用。]]
        oldprint(s == "foo" and "bar" or s)
    end
end

print("foo")

function addto(x)
    -- 返回一个把实际参数加到x上
    return function(y)
      --[=[ 在我们引用变量x的时候，它在当前作用域的外部，
      它的生命期会比这个匿名函数短，Lua建立一个闭包。]=]
      return x + y
    end
end
fourplus = addto(4)
print(fourplus(3))  -- 打印7

--这也可以通过如下方式调用这个函数来完成：
print(addto(4)(3))
--[[这是因为这直接用视件参数3调用了从addto(4)返回的函数。
    如果经常这么调用的话会减少数据消耗并提升性能。]]

local count = 0
function create_a_counter()
    -- local counter = 0 此时该变量存在于闭包内部
    return function()
        count = count + 1
        return count
    end
end

print(create_a_counter()())
print(create_a_counter()())