using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlDatabase;
namespace TicTacToeGame.Controllers
{
    [Route("api/Game")]
    [ApiController]
    [Authorize]
    [Logger]
    [LogException]
    public class GameController : ControllerBase
    {
        static string[] board = new string[9] { "0","0","0","0","0","0","0","0","0"};
        static int countNoOfPlayers = 0;
        static List<string> availableAccessTokens = new List<string>();
        static string lastPlayerPlayed="";
        [HttpPut("{boxId}")]
        public string MakeMove(int boxId, [FromHeader] string accessToken)
        {
            if (GetStatus().Equals("In Progress"))
            {
                if (!availableAccessTokens.Contains(accessToken))
                {
                    availableAccessTokens.Add(accessToken);
                    countNoOfPlayers++;
                }
                if (countNoOfPlayers <= 2)
                {
                    if (!lastPlayerPlayed.Equals(accessToken))
                    {
                        if (board[boxId - 1].Equals("0"))
                        {
                            board[boxId - 1] = accessToken;
                        }
                        else
                            throw new Exception("Invalid Move");
                        lastPlayerPlayed = accessToken;
                        return GetStatus();
                    }
                    else
                        throw new Exception("No player can play consecutively");
                }
                else
                    throw new Exception("Three Players can not play");
            }
            else
                throw new Exception("Game is over now");
        }
        [HttpGet]
        public string GetStatus()
        {
            string[,] twoDimensionBoard = new string[3,3];
            int makeRow=0, makeColumn=0;
            for (int index=0;index<9;index++)
            {
                if(index!=0 && index%3==0)
                {
                    makeRow++;
                    makeColumn = 0;
                }
                twoDimensionBoard[makeRow, makeColumn]= board[index];
                makeColumn++;
            }
            Database database = new Database();
            for(int row=0;row<3;row++)
            {
                if (!twoDimensionBoard[row,0].Equals("0") && twoDimensionBoard[row, 0].Equals(twoDimensionBoard[row, 1]) && twoDimensionBoard[row, 1].Equals(twoDimensionBoard[row, 2]))
                    return database.GetUsername(twoDimensionBoard[row,0])+" Won";
                if (!twoDimensionBoard[0, row].Equals("0") && twoDimensionBoard[0, row].Equals(twoDimensionBoard[1, row]) && twoDimensionBoard[1, row].Equals(twoDimensionBoard[2, row]))
                    return database.GetUsername(twoDimensionBoard[0, row])+" Won";
                if (!twoDimensionBoard[row, 0].Equals("0") && row == 0 && twoDimensionBoard[0, 0].Equals(twoDimensionBoard[1, 1]) && twoDimensionBoard[1, 1].Equals(twoDimensionBoard[2, 2]))
                    return database.GetUsername(twoDimensionBoard[0, 0])+" Won";
                if (!twoDimensionBoard[row, 0].Equals("0") && row == 2 && twoDimensionBoard[0, 2].Equals(twoDimensionBoard[1, 1]) && twoDimensionBoard[1, 1].Equals(twoDimensionBoard[2, 0]))
                    return database.GetUsername(twoDimensionBoard[0, 2])+" Won";
            }
            if (board.ToList().Contains("0"))
                return "In Progress";
            return "Draw";
        }
    }
}