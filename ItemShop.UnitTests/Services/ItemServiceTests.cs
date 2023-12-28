using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using ItemShop.Mappers;
using ItemShop.Models.DTOs.ItemDtos;
using ItemShop.Models.Entities;
using ItemShop.Services;
using Moq;

namespace ItemShop.UnitTests.Services
{
    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _itemRepositoryMock;
        private readonly ItemService _itemService;
        private readonly IMapper _mapper;

        public ItemServiceTests()
        {
            _itemRepositoryMock = new Mock<IItemRepository>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MyAutomapperProfile>();
            });
            _mapper = configuration.CreateMapper();
            _itemService = new ItemService(_itemRepositoryMock.Object, _mapper);
        }
        [Theory]
        [AutoData]
        public async Task Get_GivenValidId_ReturnsEntity(int id)
        {
            //Arrange
            _itemRepositoryMock.Setup(x => x.Get(id)).ReturnsAsync(new Item() 
            { 
                Id = id
            });

            //Act
            var result = await _itemService.Get(id);

            //Assert
            result.Id.Should().Be(id);

        }

        [Theory]
        [AutoData]
        public async Task Get_GivenInvalidId_ThrowsItemNotFoundException(int id)
        {
            //Arrange
            _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<Item>(null));

            //Act and Assert
            await _itemService.Invoking(async x => await x.Get(id)).Should().ThrowAsync<ItemNotFoundException>();

        }

        [Theory]
        [AutoData]
        public async Task GetAll_WithItems_ReturnsItemsList(List<Item> expectedItems)
        {
            //Arrange

            _itemRepositoryMock.Setup(m => m.Get()).Returns(Task.FromResult(expectedItems));

            //Act
            var result = await _itemService.GetAll();

            //Assert
            result.Should().BeEquivalentTo(expectedItems);
        }

        [Theory]
        [AutoData]
        public async Task CreateItem_ValidDto_CallsRepositoryCreate(CreateItemDto createItemDto)
        {
            //Arrange

            await _itemService.Create(createItemDto);
            //Act and Assert
            _itemRepositoryMock.Verify(m => m.Create(It.Is<Item>(item => item.Name == createItemDto.Name && item.Price == createItemDto.Price)), Times.Once());

        }
        [Theory]
        [AutoData]
        public async Task DeleteItem_GivenValidId_CallsRepositoryDelete(int itemId)
        {
            //Arange
            var existingItem = new Item { Id = itemId };
            _itemRepositoryMock.Setup(m => m.Get(itemId)).Returns(Task.FromResult(existingItem));

            //Act
            await _itemService.Delete(itemId);

            //Assert
            _itemRepositoryMock.Verify(m => m.Get(itemId), Times.Once);
            _itemRepositoryMock.Verify(m => m.Delete(existingItem), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task DeleteItem_GivenInvalidId_ThrowsItemNotFoundException(int id)
        {
            //Arrange
            _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<Item>(null));

            //Act and Assert
            await _itemService.Invoking(async x => await x.Delete(id)).Should().ThrowAsync<ItemNotFoundException>();

            _itemRepositoryMock.Verify(m => m.Get(id), Times.Once);
            _itemRepositoryMock.Verify(m => m.Delete(It.IsAny<Item>()), Times.Never);

        }

        [Theory]
        [AutoData]
        public async Task UpdateItem_GivenValidId_CallsRepositoryUpdate(UpdateItemDto updateItemDto)
        {
            //Arrange
            var existingItem = new Item { Id = updateItemDto.Id };
            _itemRepositoryMock.Setup(m => m.Get(updateItemDto.Id)).Returns(Task.FromResult(existingItem));

            //Act
            await _itemService.Update(updateItemDto);
            //Assert
            _itemRepositoryMock.Verify(m => m.Get(updateItemDto.Id), Times.Once);
            _itemRepositoryMock.Verify(m => m.Update(It.IsAny<Item>()), Times.Once);
        }
        [Theory]
        [AutoData]
        public async Task UpdateItem_GivenInvalidId_ThrowsItemNotFoundException(int id)
        {
            //Arrange
            _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<Item>(null));

            var updateItemDto = new UpdateItemDto { Id = id };

            //Act and Assert
            await _itemService.Invoking(async x => await x.Update(updateItemDto)).Should().ThrowAsync<ItemNotFoundException>();

            _itemRepositoryMock.Verify(m => m.Get(id), Times.Once);
            _itemRepositoryMock.Verify(m => m.Update(It.IsAny<Item>()), Times.Never);
        }

    }
}