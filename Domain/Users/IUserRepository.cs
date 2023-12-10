﻿
namespace Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User?> CreateAsync(User user);
    }
}
