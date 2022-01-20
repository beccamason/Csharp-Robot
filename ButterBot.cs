using System;
using BotInterface.Bot;
using BotInterface.Game;

namespace ButterBot
{
    public class ButterBot : IBot
    {
        public Move MakeMove(Gamestate gamestate)
        {
            switch (randomMove(gamestate, DynamiteTracker(gamestate)))
            {
                case "D":
                    return Move.D;
                case "R":
                    return Move.R;
                case "P":
                    return Move.P;
                case "S":
                    return Move.S;
                case "W":
                    return Move.W;
                default:
                    return Move.S;
            };
        }

        public static int DynamiteTracker(Gamestate gamestate)
        {
            int dynamiteUsage = 0;
            Round[] previous = gamestate.GetRounds();
            foreach (Round r in previous)
            {
                if (r.GetP1() == Move.D)
                {
                    dynamiteUsage++;
                }
            }
            return dynamiteUsage;
        }

        public static int MovesSoFar(Gamestate gamestate)
        {
            Round[] previous = gamestate.GetRounds();
            return previous.Length;
        }

        public static string randomMove(Gamestate gamestate, int dynamiteUsage)
        {
            Round[] previous = gamestate.GetRounds();
            bool recentDynamite = false;
            bool roundDraw = false; 
            if (previous.Length > 2)
            {
                Round r1 = previous[(previous.Length - 1)];
                Round r2 = previous[(previous.Length - 2)];
                Round r3 = previous[(previous.Length - 3)];
                if ((r1.GetP2() == Move.D) && r2.GetP2() == Move.D)
                {
                    return "W";
                }
                if (r1.GetP1() == Move.D && r2.GetP1() == Move.D)
                {
                    recentDynamite = true;
                }
                if (r1.GetP1() ==  r1.GetP2())
                {
                    roundDraw = true;
                }
                if (r1.GetP2() == r2.GetP1() && r2.GetP2() == r3.GetP1())
                {
                    switch (r1.GetP1())
                    {
                        case Move.D:
                            return "W";
                        case Move.W:
                            return "P";
                        case Move.S:
                            return "R";
                        case Move.P:
                            return "S";
                        case Move.R:
                            return "P";
                    }
                }
                if (r1.GetP2() == r2.GetP2() && r2.GetP2() == r3.GetP2())
                {
                    switch (r1.GetP2())
                    {
                        case Move.D:
                            return "W";
                        case Move.W:
                            return "P";
                        case Move.S:
                            return "R";
                        case Move.P:
                            return "S";
                        case Move.R:
                            return "P";
                    }
                }


            }

            if ((dynamiteUsage < 100) && (recentDynamite == false) )
            {
                if (roundDraw == true)
                {
                    return "D";
                }
                else
                {
                    string[] possibleMoves = { "P", "S", "R", "D" };
                    Random rnd = new Random();
                    int randomNumber = rnd.Next(4);
                    return possibleMoves[randomNumber];
                }
            }
            else
            {
                string[] possibleMoves = { "R", "P", "S" };
                Random rnd = new Random();
                int randomNumber = rnd.Next(3);
                return possibleMoves[randomNumber];
            }
        }
    }
}
