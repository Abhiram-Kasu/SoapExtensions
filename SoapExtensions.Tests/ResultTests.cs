using static NUnit.Framework.Assert;

namespace SoapExtensions.Tests;

[TestFixture]
public class ResultTests
{
    [Test]
    public void Result_Success_IsSuccessTrue()
    {
        var result = new Result<int, StandardError>(5, null);
        That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Result_Error_IsSuccessFalse()
    {
        var error = new StandardError("An error occurred");
        var result = new Result<int, StandardError>(default, error);
        That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Result_ImplicitConversionFromValue_IsSuccessTrue()
    {
        Result<int, StandardError> result = 5;
        That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Result_ImplicitConversionFromError_IsSuccessFalse()
    {
        Result<int, StandardError> result = new StandardError("An error occurred");
        That(result.IsSuccess, Is.False);
    }
}