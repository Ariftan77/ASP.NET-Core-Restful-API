using System.Runtime.Serialization;

namespace EmployeeManagement.Business.Exceptions;

[Serializable]
public class EmployeeNotFoundException : Exception
{
    public int Id;

    public EmployeeNotFoundException()
    {
    }

    public EmployeeNotFoundException(int id)
    {
        this.Id = id;
    }

    public EmployeeNotFoundException(string? message) : base(message)
    {
    }

    public EmployeeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EmployeeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}