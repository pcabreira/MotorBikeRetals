using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Enums;

namespace MotorBikeRetals.UnitTests.Core.Entities
{
    public class ContractTests
    {
        [Fact]
        public void TestIfContractStartWorks()
        {
            var contract = new Contract(Guid.NewGuid(), 7, Guid.NewGuid(), "A", Guid.NewGuid(), 210.0M);

            contract.Start();

            Assert.Equal(ContractStatusEnum.InProgress, contract.Status);
        }
    }
}
