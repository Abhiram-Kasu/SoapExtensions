using static NUnit.Framework.Assert;

namespace SoapExtensions.Tests;

[TestFixture]
public class ErrorTests
{
    [Test]
    public void Error_Message_IsCorrect()
    {
        var error = new StandardError("An error occurred");
        That(error.Message, Is.EqualTo("An error occurred"));
    }

    [Test]
    public void Error_Exception_IsNullByDefault()
    {
        var error = new StandardError("An error occurred");
        That(error.Exception, Is.Null);
    }

    [Test]
    public void Error_Exception_CanBeSet()
    {
        var exception = new Exception("An exception occurred");
        var error = new StandardError("An error occurred", exception);
        That(error.Exception, Is.EqualTo(exception));
    }

    record WritingError(string Message) : Error(Message);
    [Test]
    public void Different_Errors_Are_Returned()
    {
        const string message = "An error occurred";
        var error = new WritingError(message);
        Result<int, WritingError> result = error;
        That(result.Error, Is.Not.Null);
        
        That(result.Error!.Message, Is.EqualTo(message));
    }
}