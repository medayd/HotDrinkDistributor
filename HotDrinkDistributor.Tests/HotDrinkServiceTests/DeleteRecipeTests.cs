using Xunit;
using FakeItEasy;
using FluentAssertions;
using HotDrinkDistributor.Domain.Ports;
using HotDrinkDistributor.Domain.Models;
using HotDrinkDistributor.Application.Services;
using HotDrinkDistributor.Domain.Config;
using Microsoft.Extensions.Options;

namespace HotDrinkDistributor.Tests.HotDrinkServiceTests
{
    public class DeleteRecipeTests
    {
        private readonly IHotDrinkService _hotDrinkService;
        private readonly IHotDrinkRepository _mockRepository;
        private IOptions<MargeConfig> _mockConfig;

        public DeleteRecipeTests()
        {
            _mockRepository = A.Fake<IHotDrinkRepository>();
            _mockConfig = A.Fake<IOptions<MargeConfig>>();
            _hotDrinkService = new HotDrinkService(_mockRepository, _mockConfig);
        }

        [Fact]
        public void DeleteRecipe_WithValidId_DeletesRecipe()
        {
            // Arrange
            var existingRecipe = new Recipe { Id = 1, Name = "ExistingRecipe", Ingredients = new List<Ingredient>() };
            A.CallTo(() => _mockRepository.GetRecipe(1)).Returns(existingRecipe);

            // Act
            _hotDrinkService.Invoking(s => s.DeleteRecipe(1)).Should().NotThrow();

            // Assert
            A.CallTo(() => _mockRepository.DeleteRecipe(existingRecipe)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void DeleteRecipe_WithInvalidId_ThrowsException()
        {
            // Arrange
            A.CallTo(() => _mockRepository.GetRecipe(999)).Returns((Recipe)null);

            // Act & Assert
            _hotDrinkService.Invoking(s => s.DeleteRecipe(999))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Recipe Not Found");
        }
    }
}
