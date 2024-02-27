using System.Net.Mail;
using Application.Models;
using Domain.Users.UserDetails;
using Infrastructure.EmailProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shared;

namespace InfrastructureTests.EmailProviderTests;

[TestFixture]
public class EmailProviderTests
{
    private readonly Mock<IOptions<EmailOptions>> _emailProviderConfigurationMock = new();
    private readonly Mock<ILogger<EmailProvider>> _loggerMock = new();
    private string _emailKey = string.Empty;

    [SetUp]
    public void SetUp()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _emailKey = configuration.GetSection("Email").GetValue<string>("Password")!;
    }
    
    [TearDown]
    public void TearDown()
    {
        _emailProviderConfigurationMock.Reset();
        _loggerMock.Reset();
    }
    
    [Test]
    public void Handle_WithInValidEmailMessageComposerObject_ShouldThrowArgumentNullException()
    {
        //Arrange
        EmailMessageComposer emailModel = null!;
        
        var emailOptions = new EmailOptions
        {
            SenderEmail = "freelancingplatform478@gmail.com",
            Password = _emailKey,
            Host = "smtp.gmail.com",
            Port = 587,
            ConfirmationEmailBody = "To confirm your email go to this URL ApiUrl/api/User/{UserId:Guid}/Confirm/Email/{ConfirmationToken:Guid}"
        };
        
        _emailProviderConfigurationMock.Setup(m => m.Value).Returns(emailOptions);

        //Act & Assert
        var emailProvider = new EmailProvider(_emailProviderConfigurationMock.Object, _loggerMock.Object);
        Assert.ThrowsAsync<ArgumentNullException>(() => emailProvider.SendAsync(emailModel, new CancellationToken()));
    }
    
    [Test]
    public void Handle_WithInValidSmtpServerName_ShouldThrowException()
    {
        //Arrange
        var confirmationToken = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var emailModel = new EmailMessageComposer()
        {
            Body = "testBody",
            ConfirmationEmail = new ConfirmationEmail()
            {
                ConfirmationToken = confirmationToken,
                UserId = userId
            },
            CopyTo = null,
            Recipient = EmailAddress.BuildEmail("john.doe@example.com").Value!
        };
        
        var emailOptions = new EmailOptions
        {
            SenderEmail = "freelancingplatform478@gmail.com",
            Password = _emailKey,
            Host = "tcp.gmail.com",
            Port = 587,
            ConfirmationEmailBody = "To confirm your email go to this URL ApiUrl/api/User/{UserId:Guid}/Confirm/Email/{ConfirmationToken:Guid}"
        };
        
        _emailProviderConfigurationMock.Setup(m => m.Value).Returns(emailOptions);

        //Act & Assert
        var emailProvider = new EmailProvider(_emailProviderConfigurationMock.Object, _loggerMock.Object);
        Assert.ThrowsAsync<Exception>(() => emailProvider.SendAsync(emailModel, new CancellationToken()));
        _loggerMock.Verify(logger => logger.Log(
            LogLevel.Error,
            0,
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
    }
    
    [Test]
    public void Handle_WithInValidHost_ShouldThrowException()
    {
        //Arrange
        var confirmationToken = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var emailModel = new EmailMessageComposer()
        {
            Body = "testBody",
            ConfirmationEmail = new ConfirmationEmail()
            {
                ConfirmationToken = confirmationToken,
                UserId = userId
            },
            CopyTo = null,
            Recipient = EmailAddress.BuildEmail("john.doe@example.com").Value!
        };
        
        var emailOptions = new EmailOptions
        {
            SenderEmail = "freelancingplatform478@gmail.com",
            Password = _emailKey,
            Host = "smtp.gmail.com",
            Port = 5,
            ConfirmationEmailBody = "To confirm your email go to this URL ApiUrl/api/User/{UserId:Guid}/Confirm/Email/{ConfirmationToken:Guid}"
        };
        
        _emailProviderConfigurationMock.Setup(m => m.Value).Returns(emailOptions);

        //Act & Assert
        var emailProvider = new EmailProvider(_emailProviderConfigurationMock.Object, _loggerMock.Object);
        Assert.ThrowsAsync<Exception>(() => emailProvider.SendAsync(emailModel, new CancellationToken()));
        _loggerMock.Verify(logger => logger.Log(
            LogLevel.Error,
            0,
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
    }
    
    [Test]
    public void Handle_WithInValidKey_ShouldThrowException()
    {
        //Arrange
        var confirmationToken = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var emailModel = new EmailMessageComposer()
        {
            Body = "testBody",
            ConfirmationEmail = new ConfirmationEmail()
            {
                ConfirmationToken = confirmationToken,
                UserId = userId
            },
            CopyTo = null,
            Recipient = EmailAddress.BuildEmail("john.doe@example.com").Value!
        };
        
        var emailOptions = new EmailOptions
        {
            SenderEmail = "freelancingplatform478@gmail.com",
            Password = _emailKey + "invalid",
            Host = "smtp.gmail.com",
            Port = 5,
            ConfirmationEmailBody = "To confirm your email go to this URL ApiUrl/api/User/{UserId:Guid}/Confirm/Email/{ConfirmationToken:Guid}"
        };
        
        _emailProviderConfigurationMock.Setup(m => m.Value).Returns(emailOptions);

        //Act & Assert
        var emailProvider = new EmailProvider(_emailProviderConfigurationMock.Object, _loggerMock.Object);
        Assert.ThrowsAsync<Exception>(() => emailProvider.SendAsync(emailModel, new CancellationToken()));
        _loggerMock.Verify(logger => logger.Log(
            LogLevel.Error,
            0,
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
    }
}