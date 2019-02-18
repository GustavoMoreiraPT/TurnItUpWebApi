namespace Infrastructure.CrossCutting
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}
