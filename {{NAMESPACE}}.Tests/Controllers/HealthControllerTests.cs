using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using {{NAMESPACE}}.controllers;
using {{NAMESPACE}}.services;
using {{NAMESPACE}}.services.http;

namespace {{NAMESPACE}}.Tests.Controllers;

public class HealthControllerTests
{
    [Fact]
    public void HealthController_CanBeInstantiated()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<HealthController>>();
        var appSettingsMock = new Mock<AppSettings>();
        var echoServiceMock = new Mock<EchoService>(Mock.Of<ILogger<EchoService>>());
        var httpServiceMock = new Mock<DefaultHttpService>(
            Mock.Of<ILogger<DefaultHttpService>>(),
            new HttpClient(),
            appSettingsMock.Object
        );

        // Act
        var controller = new HealthController(
            loggerMock.Object,
            appSettingsMock.Object,
            echoServiceMock.Object,
            httpServiceMock.Object
        );

        // Assert
        Assert.NotNull(controller);
    }

    // Add more tests as needed
}

