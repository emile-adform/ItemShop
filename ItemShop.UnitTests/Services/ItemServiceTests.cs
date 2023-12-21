using AutoMapper;
using ItemShop.Mappers;
using ItemShop.Repositories;
using ItemShop.Services;
using Moq;

namespace ItemShop.UnitTests.Services
{
    public class ItemServiceTests
    {
        [Fact]
        public async Task Get_GivenValidId_ReturnsEntity()
        {
            int id = 1;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            var mapper = configuration.CreateMapper();
            var testRepository = new Mock<IEFItemRepository>();
            testRepository.Setup(x => x.Get(id)).ReturnsAsync(new Models.Entities.Item() 
            { 
                Id = id
            });

            var itemService = new ItemService((EFItemRepository)testRepository.Object, mapper);

        }
    }
}