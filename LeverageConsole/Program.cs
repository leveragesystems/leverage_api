using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeverageApi;

namespace LeverageConsole {
  class Program {
    static void Main(string[] args) {
      Seed s = new Seed();
      s.Create();
      Console.ReadLine();
    }
  }
}
