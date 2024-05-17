using System;

namespace ReflectDynamicConsoleApp;




[AttributeUsage(validOn: AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
public class AssemblyAttr : Attribute
{
    private string _message;

    public AssemblyAttr(string message)
    {
        _message = message;
    }

    public string Message
    {
        get => _message;
        set => _message = value;
    }
}
