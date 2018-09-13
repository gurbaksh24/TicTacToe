namespace TicTacToeGame
{
    public interface ILoggerOperation
    {
        void Log(string request, string response, string exceptionMessage, string comment);
    }
}