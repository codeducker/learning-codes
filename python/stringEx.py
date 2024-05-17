name = "andy li"
print(name.title()) #首字母大写
print(name.upper())
print(name.lower())

message = f"hello {name}"
print(message)

format_message = f"hello {name.title()}" #该方式3.6版本引入，之前版本使用.format方式
print(format_message)

val = "this value"

mess = "You will get {}".format(val)
print(mess)

gen_value = "you make things easy   "
print(gen_value.rstrip() == gen_value)

left_gen_value = "   right !"
print(left_gen_value.lstrip())

print(5/3)
print(5//3)
print(5%3) 