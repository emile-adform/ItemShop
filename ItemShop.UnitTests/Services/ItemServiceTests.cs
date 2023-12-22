using AutoMapper;
using ItemShop.Exceptions;
using ItemShop.Mappers;
using ItemShop.Models.Entities;
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
            //Arrange
            int id = 1;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            var mapper = configuration.CreateMapper();
            var testRepository = new Mock<IEFItemRepository>();
            testRepository.Setup(x => x.Get(id)).ReturnsAsync(new Item() 
            { 
                Id = id
            });

            var itemService = new ItemService(testRepository.Object, mapper);

            //Act
            var result = await itemService.GetItem(id);

            //Assert
            Assert.Equal(id, result.Id);

        }

        [Fact]
        public async Task Get_GivenInvalidId_ThrowsItemNotFoundException()
        {
            //Arrange
            int id = 1;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            var mapper = configuration.CreateMapper();
            var testRepository = new Mock<IEFItemRepository>();
            testRepository.Setup(m => m.Get(id)).Returns(Task.FromResult<Item>(null));

            var itemService = new ItemService(testRepository.Object, mapper);

            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await itemService.GetItem(id));

        }

        [Fact]
        public async Task GetAll_WithItems_ReturnsItemsList()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            var mapper = configuration.CreateMapper();
            var testRepository = new Mock<IEFItemRepository>();
            var itemService = new ItemService(testRepository.Object, mapper);
            var expectedItems = new List<Item>
            {
                new Item{Id = 1},
                new Item{Id = 2},
            };
            testRepository.Setup(m => m.Get()).Returns(Task.FromResult(expectedItems));
            var result = await itemService.GetAllItems();
            Assert.Equal(expectedItems, result);
        }

        [Fact]
        public async Task GetAll_WithoutItems_ThrowsNoItemsFoundException()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            var mapper = configuration.CreateMapper();
            var testRepository = new Mock<IEFItemRepository>();
            var itemService = new ItemService(testRepository.Object, mapper);

            testRepository.Setup(m => m.Get()).Returns(Task.FromResult(new List<Item>()));

            await Assert.ThrowsAsync<NoItemsFoundException>(itemService.GetAllItems);
        }

        [Fact]

    }
}