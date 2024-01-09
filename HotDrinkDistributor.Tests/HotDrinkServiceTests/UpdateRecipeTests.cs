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
    public class UpdateRecipeTests
    {
        private readonly IHotDrinkService _hotDrinkService;
        private readonly IHotDrinkRepository _mockRepository;
        private IOptions<MargeConfig> _mockConfig;

        public UpdateRecipeTests()
        {
            _mockRepository = A.Fake<IHotDrinkRepository>();
            _mockConfig = A.Fake<IOptions<MargeConfig>>();
            _hotDrinkService = new HotDrinkService(_mockRepository,_mockConfig);
        }

        [Fact]
        public void UpdateRecipe_WithValidId_UpdatesRecipe()
        {
            // Arrange
            var existingRecipe = new Recipe { Id = 1, Name = "ExistingRecipe", Ingredients = new List<Ingredient>() };
            var modifiedRecipe = new Recipe { Id = 1, Name = "ModifiedRecipe", Ingredients = new List<Ingredient>() };
            A.CallTo(() => _mockRepository.GetRecipe(1)).Returns(existingRecipe);

            // Act
            _hotDrinkService.Invoking(s => s.UpdateRecipe(modifiedRecipe)).Should().NotThrow();

            // Assert
            A.CallTo(() => _mockRepository.UpdateRecipe(existingRecipe)).MustHaveHappenedOnceExactly();
            existingRecipe.Should().BeEquivalentTo(modifiedRecipe);
        }

        [Fact]
        public void UpdateRecipe_WithInvalidId_ThrowsException()
        {
            // Arrange
            var modifiedRecipe = new Recipe { Id = 999, Name = "ModifiedRecipe", Ingredients = new List<Ingredient>() };
            A.CallTo(() => _mockRepository.GetRecipe(999)).Returns((Recipe)null);

            // Act & Assert
            _hotDrinkService.Invoking(s => s.UpdateRecipe(modifiedRecipe))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Recipe Not Found");
        }
    }
}
