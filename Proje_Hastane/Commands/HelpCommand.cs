using Consolas.Core;
using Proje_Hastane.Args;

namespace Proje_Hastane.Commands
{
    public class HelpCommand : Command
    {
        public string Execute(HelpArgs args)
        {
            return "Using: Proje_Hastane.exe ...";
        }
    }
}