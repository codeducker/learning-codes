-- 类型
-- [[
--  字符串 string ""
--  数字  number 1
--  布尔   true
--  函数 function
--  表 table
--  自定义类型 userdata
--  线程 thread
--  空 nil
-- ]]
print(type("hello"))
-- 单行字符串
local a ="hello"
local b = [[
  你好
  特别
]]
local c = '\n' 

print(type(1))

local j = 2
local i = 0x8
local ii = 0.15
local iii = 1e3


print(type(true))
local cc = true

print(type(function() end))

function say()
  print("say")
end

print(type({}))

local tab = {opt="seek"}
tab[0] = "j" 
table.key = "key"
print(tab)
print(tab[0])
print(tab["opt"])

tab.func = function()
  print("this is a function")
end

table.insert(tab,1,"first value")

table.insert(tab,2,"waiting remove")


for k,v in pairs(tab) do 
  print(k,v)
end

table.remove(tab,1)

for k,v in pairs(tab) do 
  print(k,v)
end

print("---------")

-- 通过#table方式计算表长度不精确。自定义迭代方法统计长度
function table.len(t) 
  local i = 0
  for k,v in pairs(t) do 
    i = i +1
  end 
  return i
end

print(table.len(tab))


for i = 0, table.len(tab),1  do 
  print(tab[i])
end 
print(type(nil))

-- 按照自定义方式排序
table.sort(tab,function(a,b) 
  return a> b
end)


getmetatable(a)


a = 'alo\n123"'
a = "alo\n123\""
a = '\97lo\10\04923"'
a = [[alo
123"]]
print(a)
a = [==[
alo
123"]==]
print(a)
a=[====[abc]====]
print(a)

_ENV.X = true
print(_ENV.X)

local t = {}
t = {"name",k="nice","like",m="better"}
-- print(t[0]) lua 下标默认从 1 开始
print(t['k'])
print(t[1])
print(t.m)

t= {k="nice","name","like",m="better"}
-- print(t[0]) lua 下标默认从 1 开始
t.m=nil -- 删除元素
print(t['k'])
print(t[1])
print(t.m)


for k,v in pairs(t) do 
  print(k,v)
end

function t.f() 
  print "function"
end

print(t.f())

print(#t)

for i =1 , #t do  -- 按照表长度迭代
  print(t[i])
end

name = "屁屁"
print(_G['name'])

-- 通过表来实现面向对象
person = {}
person.name = "andy"
person.age = 18

function person.getName(p) 
  print("person name" , p['name'])
end

person.getName(person)


function person.clone() 
  local copy = {}
  for k,v in pairs(person) do 
    copy[k] = v
  end
  return copy
end

person.new = function(name, age)
  local p = person.clone()
  p.name = name 
  p.age = age
  return p
end

local clonePerson = person.clone()
for k,v in pairs(clonePerson) do
  print("person clone attrs " , k,"=",v)
end

local newPerson = person.new("lucy",11)

-- 实现继承
local Student = {}
Student.new = function(number,name,age) 
  local p = person.clone()
  p.name =name 
  p.age = age
  p.number = number
  return p
end

local stu = Student.new("123","小凤",24)
print(stu.getName(stu))


-- 闭包方式实现面向对象
function Sharp()
  local self = {}
  self.getArea = function()
    return 0
  end
  self.getName = function()
    return ""
  end
  return self
end 

function Circle(radius)
  local circle  = Sharp()
  circle.radius = radius
  circle.getArea = function()
    return math.pi * circle.radius * circle.radius
  end 
  circle.getName = function()
      return "this is a Circle"
  end
  return circle
end

local circle = Circle(2.3)
print(circle.getName())
print("area:",circle.getArea())



array = { "a", "b", "c", "d" }   -- 索引被自动赋予。
print(array[2])                  -- 打印"b"。在Lua中自动索引开始于1。
print(#array)                    -- 打印4。#是表格和字符串的长度算符。
array[0] = "z"                   -- 0是合法索引。
print(array[0])                  --  0 合法索引所以可以正常打印 z
print(#array)                    -- 仍打印4，因为Lua数组是基于1的。

lo  = { nil,"hello","make",nil,1,2,3,nil}
print(#lo,lo[1],lo[4])

-- 斐波那契数列
fibs = { 1, 1 } -- 给fibs[1]和fibs[2]初始值。

setmetatable(fibs, {
  __index = function(values, n)                --[[__index是Lua预定义的函数， 
                                                   如果"n"不存在则调用它。]]
    values[n] = values[n - 1] + values[n - 2]  -- 计算并记忆化fibs[n]。
    return values[n]
  end
})

print(fibs.__index(fibs,2))