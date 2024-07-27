namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IArticleRepository Articles { get; }
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}