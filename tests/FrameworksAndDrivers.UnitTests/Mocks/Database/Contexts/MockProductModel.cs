using System.Linq;
using System.Collections.Generic;
using FrameworksAndDrivers.Database.Models;

namespace FrameworksAndDrivers.UnitTests.Mocks.Database.Contexts
{
    public class MockProductModel
    {
        public readonly static IList<CreditCardModel> Data = new List<CreditCardModel>
        {
            new CreditCardModel
            {
                Id = "WLUPF",
                Name = "Vida Inteira Único",
                Description = "Vida Inteira Único - Portfolio F - WLUPF",
                TypeId = MockProductTypeModel.Data.FirstOrDefault(a => a.Id == "BASIC").Id,
                GroupId = MockProductGroupModel.Data.FirstOrDefault(a => a.Id == "DEFAULT").Id,
                FamilyId = MockProductFamilyModel.Data.FirstOrDefault(a => a.Id == "WDU").Id,
                Others = new Dictionary<string, object>(),
                Type = MockProductTypeModel.Data.FirstOrDefault(a=>a.Id == "BASIC"),
                Group = MockProductGroupModel.Data.FirstOrDefault(a => a.Id == "DEFAULT"),
                Family = MockProductFamilyModel.Data.FirstOrDefault(a => a.Id == "WDU")
            },
            new CreditCardModel
            {
                Id = "AB05F",
                Name = "Morte Acidental por 5 anos",
                Description = "Morte Acidental por 5 anos - Portfolio F - AB05F",
                TypeId = MockProductTypeModel.Data.FirstOrDefault(a=>a.Id == "ADDT").Id,
                GroupId = MockProductGroupModel.Data.FirstOrDefault(a => a.Id == "DEFAULT").Id,
                FamilyId = MockProductFamilyModel.Data.FirstOrDefault(a => a.Id == "ADBU").Id,
                Others = new Dictionary<string, object>(),
                Type = MockProductTypeModel.Data.FirstOrDefault(a=>a.Id == "ADDT"),
                Group = MockProductGroupModel.Data.FirstOrDefault(a => a.Id == "DEFAULT"),
                Family = MockProductFamilyModel.Data.FirstOrDefault(a => a.Id == "ADBU")
            }
        };
    }
}