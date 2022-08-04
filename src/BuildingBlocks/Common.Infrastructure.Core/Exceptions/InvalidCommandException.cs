namespace Common.Infrastructure.Core.Exceptions;

public class InvalidCommandException : Exception
{
    public int ErrorCode { get; }
    public string Details { get; }
    public string ParaName { get; }

    public InvalidCommandException(int errorCode, string details, string paraName = null) : base(details)
    {
        ErrorCode = errorCode;
        Details = details;
        ParaName = paraName;
    }
}