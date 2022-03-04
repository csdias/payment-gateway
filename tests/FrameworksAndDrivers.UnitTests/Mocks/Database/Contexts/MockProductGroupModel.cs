using System.Collections.Generic;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts
{
    public class MockProductGroupModel
    {
        public readonly static List<ProductGroupModel> Data = new List<ProductGroupModel>
        {
            new ProductGroupModel
            {
                Id = "DEFAULT",
                Name = "Regra padrão",
                Description = "Regra padrão",
                Formula = "({rate}*{capital})/1000",
                FormulaWithoutCapital = "({amount}*1000)/{rate}",
                Fields = new List<string> { "age", "gender" },
                Defaults = new Dictionary<string, object> {{"class", "STANDARD"}},
                Others = new Dictionary<string, object>()
            }
        };
    }
}