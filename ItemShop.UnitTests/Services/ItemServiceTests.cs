using AutoMapper;
using ItemShop.Exceptions;
using ItemShop.Mappers;
using ItemShop.Models.DTOs;
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

            //Act and Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await itemService.GetItem(id));

        }

        [Fact]
        public async Task GetAll_WithItems_ReturnsItemsList()
        {
            //Arrange
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

            //Act
            var result = await itemService.GetAllItems();

            //Assert
            Assert.Equal(expectedItems, result);
        }

        [Fact]
        public async Task GetAll_WithoutItems_ThrowsNoItemsFoundException()
        {
            //Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            var mapper = configuration.CreateMapper();
            var testRepository = new Mock<IEFItemRepository>();
            var itemService = new ItemService(testRepository.Object, mapper);

            testRepository.Setup(m => m.Get()).Returns(Task.FromResult(new List<Item>()));

            //Act and Assert
            await Assert.ThrowsAsync<NoItemsFoundException>(itemService.GetAllItems);
        }

        [Fact]
        public async Task CreateItem_ValidDto_CallsRepositoryCreate()
        {
            //Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            var mapper = configuration.CreateMapper();
            var testRepository = new Mock<IEFItemRepository>();
            var itemService = new ItemService(testRepository.Object, mapper);
            var createItemDto = new CreateItemDto
            {
                Name = "test",
                Price = 2,
            };

            await itemService.CreateItem(createItemDto);
            //Act and Assert
            testRepository.Verify(m => m.Create(It.Is<Item>(item => item.Name == createItemDto.Name && item.Price == createItemDto.Price)), Times.Once());

        }
        [Fact]
        public async Task DeleteItem_GivenValidId_CallsRepositoryDelete()
        {
            //Arange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            var mapper = configuration.CreateMapper();
            var testRepository = new Mock<IEFItemRepository>();
            var itemService = new ItemService(testRepository.Object, mapper);
            var itemId = 1;
            var existingItem = new Item { Id = itemId };
            testRepository.Setup(m => m.Get(itemId)).Returns(Task.FromResult(existingItem));

            //Act
            await itemService.DeleteItem(itemId);

            //Assert
            testRepository.Verify(m => m.Get(itemId), Times.Once());
            testRepository.Verify(m => m.Delete(existingItem), Times.Once());
        }

        [Fact]
        public async Task DeleteItem_GivenInvalidId_ThrowsItemNotFoundException()
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

            //Act and Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await itemService.DeleteItem(id));

            testRepository.Verify(m => m.Get(id), Times.Once());
            testRepository.Verify(m => m.Delete(It.IsAny<Item>()), Times.Never());
        }

        [Fact]
        public async Task UpdateItem_GivenValidId_CallsRepositoryUpdate()
        {
            //Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            var mapper = configuration.CreateMapper();
            var testRepository = new Mock<IEFItemRepository>();
            var itemService = new ItemService(testRepository.Object, mapper);
            var itemId = 1;
            var updateItemDto = new UpdateItemDto { Id = itemId };
            var existingItem = new Item { Id = updateItemDto.Id };
            testRepository.Setup(m => m.Get(updateItemDto.Id)).Returns(Task.FromResult(existingItem));

            //Act
            await itemService.UpdateItem(updateItemDto);
            //Assert
            testRepository.Verify(m => m.Get(updateItemDto.Id), Times.Once());
            testRepository.Verify(m => m.Update(It.IsAny<Item>()), Times.Once());
        }
        [Fact]
        public async Task UpdateItem_GivenInvalidId_ThrowsItemNotFoundException()
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
            var updateItemDto = new UpdateItemDto { Id = id };

            //Act and Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await itemService.UpdateItem(updateItemDto));

            testRepository.Verify(m => m.Get(id), Times.Once());
            testRepository.Verify(m => m.Update(It.IsAny<Item>()), Times.Never());
        }

    }
}