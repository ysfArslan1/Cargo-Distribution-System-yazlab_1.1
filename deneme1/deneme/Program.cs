using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace deneme
{
    static class Program
    {
        
        static void Main()
        {
            //GUI1 ekraninin baslangic  calistirilir
            Application.EnableVisualStyles();
            GUI1 ekran = new GUI1();
            ekran.Left = 675;
            ekran.Top = 0;
            Application.Run(ekran);
         
        }
     
    }
}
