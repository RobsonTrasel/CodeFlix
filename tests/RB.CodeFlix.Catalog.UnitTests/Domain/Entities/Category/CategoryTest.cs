using Xunit;
using DomainEntities = RB.CodeFlix.Catalog.Domain.Entities;
namespace RB.CodeFlix.Catalog.UnitTests.Domain.Entities.Category;

public class CategoryTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var dateTimeBefore = DateTime.Now;
        
        var category = new DomainEntities.Category(validData.Name, validData.Description);

        var dateTimeAfter = DateTime.Now;

        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > dateTimeBefore);
        Assert.True(category.CreatedAt < dateTimeAfter);
        Assert.True(category.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateWithActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithActive(bool IsActive)
    {
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var dateTimeBefore = DateTime.Now;

        var category = new DomainEntities.Category(validData.Name, validData.Description, IsActive);

        var dateTimeAfter = DateTime.Now;

        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > dateTimeBefore);
        Assert.True(category.CreatedAt < dateTimeAfter);
        Assert.Equal(IsActive, category.IsActive);
    }
}

