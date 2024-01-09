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
    public class AddRecipeTests
    {
        private readonly IHotDrinkService _hotDrinkService;
        private readonly IHotDrinkRepository _mockRepository;
        private IOptions<MargeConfig> _mockConfig;

        public AddRecipeTests()
        {
            _mockRepository = A.Fake<IHotDrinkRepository>();
            _mockConfig = A.Fake<IOptions<MargeConfig>>();
            _hotDrinkService = new HotDrinkService(_mockRepository, _mockConfig);
        }

        [Fact]
        public void AddRecipe_WithUniqueNameAndIngredients_Succeeds()
        {
            // Arrange
            var newRecipe = new Recipe { Name = "NewRecipe", Ingredients = new List<Ingredient> { new Ingredient() } };
            A.CallTo(() => _mockRepository.GetAllRecipes()).Returns(new List<Recipe>());

            // Act
            _hotDrinkService.Invoking(s => s.AddRecipe(newRecipe)).Should().NotThrow();

            // Assert
            A.CallTo(() => _mockRepository.AddRecipe(newRecipe)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void AddRecipe_WithDuplicateName_ThrowsException()
        {
            // Arrange
            var existingRecipe = new Recipe { Name = "ExistingRecipe" };
            var newRecipe = new Recipe { Name = "ExistingRecipe" };
            A.CallTo(() => _mockRepository.GetAllRecipes()).Returns(new List<Recipe> { existingRecipe });

            // Act & Assert
            _hotDrinkService.Invoking(s => s.AddRecipe(newRecipe))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Recipe name must be unique.");
        }

        [Fact]
        public void AddRecipe_WithNoIngredients_ThrowsException()
        {
            // Arrange
            var newRecipe = new Recipe { Name = "NewRecipe", Ingredients = null };

            // Act & Assert
            _hotDrinkService.Invoking(s => s.AddRecipe(newRecipe))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("A recipe must have ingredients.");
        }
    }
}
