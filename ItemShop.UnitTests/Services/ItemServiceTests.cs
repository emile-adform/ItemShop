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
        private readonly Mock<IEFItemRepository> _itemRepositoryMock;
        private readonly ItemService _itemService;
        private readonly IMapper _mapper;

        public ItemServiceTests()
        {
            _itemRepositoryMock = new Mock<IEFItemRepository>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Automapper>();
            });
            _mapper = configuration.CreateMapper();
            _itemService = new ItemService(_itemRepositoryMock.Object, _mapper);
        }
        [Fact]
        public async Task Get_GivenValidId_ReturnsEntity()
        {
            //Arrange
            int id = 1;
            _itemRepositoryMock.Setup(x => x.Get(id)).ReturnsAsync(new Item() 
            { 
                Id = id
            });

            //Act
            var result = await _itemService.GetItem(id);

            //Assert
            Assert.Equal(id, result.Id);

        }

        [Fact]
        public async Task Get_GivenInvalidId_ThrowsItemNotFoundException()
        {
            //Arrange
            int id = 1;
            _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<Item>(null));

            //Act and Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.GetItem(id));

        }

        [Fact]
        public async Task GetAll_WithItems_ReturnsItemsList()
        {
            //Arrange

            var expectedItems = new List<Item>
            {
                new Item{Id = 1},
                new Item{Id = 2},
            };
            _itemRepositoryMock.Setup(m => m.Get()).Returns(Task.FromResult(expectedItems));

            //Act
            var result = await _itemService.GetAllItems();

            //Assert
            Assert.Equal(expectedItems, result);
        }

        [Fact]
        public async Task GetAll_WithoutItems_ThrowsNoItemsFoundException()
        {
            //Arrange

            _itemRepositoryMock.Setup(m => m.Get()).Returns(Task.FromResult(new List<Item>()));

            //Act and Assert
            await Assert.ThrowsAsync<NoItemsFoundException>(_itemService.GetAllItems);
        }

        [Fact]
        public async Task CreateItem_ValidDto_CallsRepositoryCreate()
        {
            //Arrange

            var createItemDto = new CreateItemDto
            {
                Name = "test",
                Price = 2,
            };

            await _itemService.CreateItem(createItemDto);
            //Act and Assert
            _itemRepositoryMock.Verify(m => m.Create(It.Is<Item>(item => item.Name == createItemDto.Name && item.Price == createItemDto.Price)), Times.Once());

        }
        [Fact]
        public async Task DeleteItem_GivenValidId_CallsRepositoryDelete()
        {
            //Arange

            var itemId = 1;
            var existingItem = new Item { Id = itemId };
            _itemRepositoryMock.Setup(m => m.Get(itemId)).Returns(Task.FromResult(existingItem));

            //Act
            await _itemService.DeleteItem(itemId);

            //Assert
            _itemRepositoryMock.Verify(m => m.Get(itemId), Times.Once());
            _itemRepositoryMock.Verify(m => m.Delete(existingItem), Times.Once());
        }

        [Fact]
        public async Task DeleteItem_GivenInvalidId_ThrowsItemNotFoundException()
        {
            //Arrange
            int id = 1;

            _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<Item>(null));


            //Act and Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.DeleteItem(id));

            _itemRepositoryMock.Verify(m => m.Get(id), Times.Once());
            _itemRepositoryMock.Verify(m => m.Delete(It.IsAny<Item>()), Times.Never());
        }

        [Fact]
        public async Task UpdateItem_GivenValidId_CallsRepositoryUpdate()
        {
            //Arrange
            var itemId = 1;
            var updateItemDto = new UpdateItemDto { Id = itemId };
            var existingItem = new Item { Id = updateItemDto.Id };
            _itemRepositoryMock.Setup(m => m.Get(updateItemDto.Id)).Returns(Task.FromResult(existingItem));

            //Act
            await _itemService.UpdateItem(updateItemDto);
            //Assert
            _itemRepositoryMock.Verify(m => m.Get(updateItemDto.Id), Times.Once());
            _itemRepositoryMock.Verify(m => m.Update(It.IsAny<Item>()), Times.Once());
        }
        [Fact]
        public async Task UpdateItem_GivenInvalidId_ThrowsItemNotFoundException()
        {
            //Arrange
            int id = 1;
            _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<Item>(null));

            var updateItemDto = new UpdateItemDto { Id = id };

            //Act and Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.UpdateItem(updateItemDto));

            _itemRepositoryMock.Verify(m => m.Get(id), Times.Once());
            _itemRepositoryMock.Verify(m => m.Update(It.IsAny<Item>()), Times.Never());
        }

    }
}