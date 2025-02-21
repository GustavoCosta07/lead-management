using System;
using System.Threading.Tasks;
using MyApp.Application.Interfaces;

namespace MyApp.Infrastructure.Services
{
    public class FakeEmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine($"Simulando envio de e-mail para {to}: {subject}");
            return Task.CompletedTask;
        }
    }
}
