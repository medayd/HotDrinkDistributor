using Xunit;
using FakeItEasy;
using FluentAssertions;
using HotDrinkDistributor.Domain.Ports;
using HotDrinkDistributor.Domain.Models;
using HotDrinkDistributor.Application.Services;
using HotDrinkDistributor.Domain.Config;
using Microsoft.Extensions.Options;
using HotDrinkDistributor.Application.Extensions;

namespace HotDrinkDistributor.Tests.HotDrinkServiceTests
{
    // Test class for GetRecipe method
    public class GetRecipeTests
    {
        private readonly IHotDrinkService _hotDrinkService;
        private readonly IHotDrinkRepository _hotDrinkRepository;
        private IOptions<MargeConfig> _mockConfig;
        private Product cafe = new Product { Id = 1, Name = "Café", Price = 1.0m };
        private Product eau = new Product { Id = 5, Name = "Eau", Price = 0.2m };
        private Recipe allonge;

        public GetRecipeTests()
        {
            _hotDrinkRepository = A.Fake<IHotDrinkRepository>();
            _mockConfig = A.Fake<IOptions<MargeConfig>>();
            _hotDrinkService = new HotDrinkService(_hotDrinkRepository, _mockConfig);
            allonge = new Recipe
            {
                Id = 10,
                Name = "Allongé",
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Product = cafe, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 2 }
                }
            };
        }

        [Fact]
        public void GetRecipe_WithValidId_ReturnsRecipe()
        {
            // Arrange
            int validId = 1;
            A.CallTo(() => _hotDrinkRepository.GetRecipe(validId)).Returns(allonge);
            A.CallTo(() => _mockConfig.Value.Marge).Returns(1.3m);

            // Act
            var result = _hotDrinkService.GetRecipeDto(validId);
            Recipe recipe = result.ToDomainRecipe();
            // Assert
            result.Should().NotBeNull();
            result.TotalPrice.Should().Be(1.82m);
            recipe.Should().BeEquivalentTo(allonge);
        }

        [Fact]
        public void GetRecipe_WithInvalidId_ThrowsException()
        {
            // Arrange
            int invalidId = 999;
            A.CallTo(() => _hotDrinkRepository.GetRecipe(invalidId)).Returns((Recipe)null);
            A.CallTo(() => _mockConfig.Value.Marge).Returns(1.3m);

            // Act & Assert
            _hotDrinkService.Invoking(s => s.GetRecipeDto(invalidId))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Recipe Not Found");
        }
    }
}
