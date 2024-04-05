using Application.Models.Email;
using Domain.Users.UserDetails;
using Infrastructure.EmailProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace InfrastructureTests.EmailProviderTests;

[TestFixture]
public class EmailProviderTests
{
    private readonly Mock<IOptions<EmailOptions>> _emailProviderConfigurationMock = new();
    private readonly Mock<ILogger<EmailProvider>> _loggerMock = new();
    
    
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
        EmailMessageComposerModel emailModel = null!;
        
        var emailOptions = new EmailOptions
        {
            SenderEmail = "freelancingplatform478@gmail.com",
            Password = string.Empty,
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
        var emailModel = new EmailMessageComposerModel()
        {
            Content = new ConfirmationEmailModel()
            {
                ConfirmationToken = confirmationToken,
                EmailBody = "testBody"
            },
            CopyTo = null,
            Recipient = Email.BuildEmail("john.doe@example.com").Value!
        };
        
        var emailOptions = new EmailOptions
        {
            SenderEmail = "freelancingplatform478@gmail.com",
            Password = string.Empty,
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
        var emailModel = new EmailMessageComposerModel()
        {
            Content = new ConfirmationEmailModel()
            {
                ConfirmationToken = confirmationToken,
                EmailBody = "testBody"
            },
            CopyTo = null,
            Recipient = Email.BuildEmail("john.doe@example.com").Value!
        };
        
        var emailOptions = new EmailOptions
        {
            SenderEmail = "freelancingplatform478@gmail.com",
            Password = string.Empty,
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
        var emailModel = new EmailMessageComposerModel()
        {
            Content = new ConfirmationEmailModel()
            {
                ConfirmationToken = confirmationToken,
                EmailBody = "testBody"
            },
            CopyTo = null,
            Recipient = Email.BuildEmail("john.doe@example.com").Value!
        };
        
        var emailOptions = new EmailOptions
        {
            SenderEmail = "freelancingplatform478@gmail.com",
            Password = "invalid",
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