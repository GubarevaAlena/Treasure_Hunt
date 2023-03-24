using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace treasure
{
    class Chest
    {
        public const int points1 = Data.points1; 
        public const int points2 = Data.points2;
        public const int Speed = Data.Speed;
        public const int timeleft = Data.timeleft;
        public const int score = Data.score;
        public const int indchesttime = Data.indchesttime;
        public const int chestcount = Data.chestcount;
        public const int chesttime = Data.chesttime;
        public const int Width = Data.Width;
        public const int Height = Data.Height;
        public const int chestOffSetY = Data.chestOffSetY;
        public const int chestOffSetX = Data.chestOffSetX;

        public static bool IsEmpty(int x, int y)
        {
            if (Data.empty[x, y] == 0)
                return true;
            else
                return false;    
        } 

    }
}
