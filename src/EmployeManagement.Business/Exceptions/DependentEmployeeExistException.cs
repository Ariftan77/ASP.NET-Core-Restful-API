using EmployeeManagement.Common.Model;
using System.Runtime.Serialization;

namespace EmployeeManagement.Business.Exceptions;

[Serializable]
public class DependentEmployeeExistException : Exception
{
    public List<Employee> Employees;

    public DependentEmployeeExistException()
    {
    }

    public DependentEmployeeExistException(List<Employee> employees)
    {
        this.Employees = employees;
    }

    public DependentEmployeeExistException(string? message) : base(message)
    {
    }

    public DependentEmployeeExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DependentEmployeeExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}