using RB.CodeFlix.Catalog.Domain.Exceptions;
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

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]

    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        Action action =
            () => new DomainEntities.Category(name!, "Category Description");
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be empty or null", exception.Message);

    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Aggregates")]

    public void InstantiateErrorWhenDescriptionIsNull()
    {
        Action action =
            () => new DomainEntities.Category("Category Name", null!);
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should not be empty or null", exception.Message);

    }
    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThanThreeCharacters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("a")]
    [InlineData("ab")]
    public void InstantiateErrorWhenNameIsLessThanThreeCharacters(string invalidName)
    {
        Action action =
            () => new DomainEntities.Category(invalidName, "Category Description");
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be at least three characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsLessThanThreeCharacters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
        Action action =
            () => new DomainEntities.Category(invalidName, "Category Description");
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsGreaterThan10000Characters()
    {
        var invalidDescription = String.Join(null, Enumerable.Range(1, 10001).Select(_ => "a").ToArray());
        Action action =
            () => new DomainEntities.Category("Category Name", invalidDescription);
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should be less or equal 10000 characters long", exception.Message);
    }
}

