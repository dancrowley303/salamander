using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.defrobo.salamander
{
    public interface ICommandService
    {
        Task<Dictionary<Currency, Balance>> GetBalances();
        double? GetAverageTime(CommandName commandName);
        double? GetStdDevTime(CommandName commandName);
        double? GetMinTime(CommandName commandName);
        double? GetMaxTime(CommandName commandName);
    }
}
