using Shouldly;

namespace SimpleMediator.Tests;

public sealed class MediatorErrorExtensionsTests
{
    [Fact]
    public void GetMediatorCode_ReturnsMediatorCode_ForMediatorExceptions()
    {
        var error = MediatorErrors.Create("mediator.test", "boom");

        error.GetMediatorCode().ShouldBe("mediator.test");
    }

    [Fact]
    public void GetMediatorCode_ReturnsExceptionTypeName_WhenMetadataIsNonMediator()
    {
        var error = MediatorError.New("boom", new InvalidOperationException("oops"));

        error.GetMediatorCode().ShouldBe(nameof(InvalidOperationException));
    }

    [Fact]
    public void GetMediatorCode_DefaultsToUnknown_WhenMessageIsMissing()
    {
        var error = default(MediatorError);

        error.GetMediatorCode().ShouldBe("mediator.unknown");
    }

    [Fact]
    public void GetMediatorCode_UsesMessage_WhenNoMetadataAndMessagePresent()
    {
        var error = MediatorError.New("custom-code");

        error.GetMediatorCode().ShouldBe("custom-code");
    }

    [Fact]
    public void GetMediatorDetails_ReturnsDetails_FromMediatorException()
    {
        var details = new { Value = 42 };
        var error = MediatorErrors.Create("mediator.details", "boom", details: details);

        error.GetMediatorDetails().ShouldBe(details);
    }

    [Fact]
    public void GetMediatorDetails_ReturnsNull_ForNonMediatorMetadata()
    {
        var error = MediatorError.New("boom", new InvalidOperationException("oops"));

        error.GetMediatorDetails().ShouldBeNull();
    }

    [Fact]
    public void GetMediatorMetadata_ReturnsMetadata_FromMediatorException()
    {
        var details = new Dictionary<string, object?>
        {
            ["handler"] = "TestHandler",
            ["request"] = "TestRequest",
            ["stage"] = "handler"
        };

        var error = MediatorErrors.Create("mediator.metadata", "boom", details: details);

        var metadata = error.GetMediatorMetadata();

        metadata.ShouldNotBeNull();
        metadata.ShouldContainKey("handler");
        metadata["handler"].ShouldBe("TestHandler");
        metadata["stage"].ShouldBe("handler");
    }

    [Fact]
    public void GetMediatorMetadata_ReturnsEmpty_ForNonMediatorMetadata()
    {
        var error = MediatorError.New("boom", new InvalidOperationException("oops"));

        var metadata = error.GetMediatorMetadata();

        metadata.ShouldNotBeNull();
        metadata.ShouldBeEmpty();
    }
}
