using Moq;
using PropertyApp.Application.Interfaces;
using PropertyApp.Application.Services;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Tests;

[TestFixture]
public class PropertyServiceTests
{
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private IPropertyService _propertyService;

    [SetUp]
    public void Setup()
    {
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _propertyService = new PropertyService(_propertyRepositoryMock.Object);
    }

    [Test]
    public async Task GetPropertiesAsync_ShouldReturnFilteredResults()
    {
        var properties = new List<(PropertyE Property, string? Image)>
            {
                (new PropertyE { Id = "1", Name = "Casa Bonita", Address = "Calle 1", Price = 100000, IdOwner = "O1" }, "img1.jpg"),
                (new PropertyE { Id = "2", Name = "Apartamento Moderno", Address = "Calle 2", Price = 200000, IdOwner = "O2" }, "img2.jpg"),
                (new PropertyE { Id = "3", Name = "Casa Económica", Address = "Calle 3", Price = 50000, IdOwner = "O3" }, "img3.jpg")
            };

        _propertyRepositoryMock
            .Setup(r => r.GetFilteredAsync("Casa", null, null, null, 1, 10))
            .ReturnsAsync((
                properties.Where(p => p.Property.Name.Contains("Casa")),
                properties.Count(p => p.Property.Name.Contains("Casa"))
            ));

        var result = await _propertyService.GetPropertiesAsync("Casa", null, null, null, 1, 10);

        Assert.That(result.Items.Count(), Is.EqualTo(2));
        Assert.That(result.Total, Is.EqualTo(2));
        Assert.IsTrue(result.Items.All(p => p.Name.Contains("Casa")));
    }

    [Test]
    public async Task GetPropertiesAsync_ShouldReturnPaginatedResults()
    {
        var properties = Enumerable.Range(1, 15)
            .Select(i => (
                new PropertyE
                {
                    Id = i.ToString(),
                    Name = $"Propiedad {i}",
                    Address = $"Calle {i}",
                    Price = i * 1000,
                    IdOwner = $"O{i}"
                },
                $"img{i}.jpg"
            ));

        _propertyRepositoryMock
            .Setup(r => r.GetFilteredAsync(null, null, null, null, 2, 5))
            .ReturnsAsync((
                properties.Skip(5).Take(5),
                properties.Count()
            ));

        var result = await _propertyService.GetPropertiesAsync(null, null, null, null, 2, 5);

        Assert.That(result.Items.Count(), Is.EqualTo(5));
        Assert.That(result.Total, Is.EqualTo(15));
        Assert.That(result.Items.First().Name, Is.EqualTo("Propiedad 6"));
    }

    [Test]
    public async Task GetPropertiesAsync_ShouldReturnEmpty_WhenNoResults()
    {
        _propertyRepositoryMock
            .Setup(r => r.GetFilteredAsync("Inexistente", null, null, null, 1, 10))
            .ReturnsAsync((Enumerable.Empty<(PropertyE, string?)>(), 0));

        var result = await _propertyService.GetPropertiesAsync("Inexistente", null, null, null, 1, 10);

        Assert.That(result.Items, Is.Empty);
        Assert.That(result.Total, Is.EqualTo(0));
    }

    [Test]
    public async Task GetPropertyByIdAsync_ShouldReturnProperty_WhenExists()
    {
        var property = new PropertyE
        {
            Id = "1",
            Name = "Casa Bonita",
            Address = "Calle 1",
            Price = 100000,
            IdOwner = "O1"
        };

        _propertyRepositoryMock
            .Setup(r => r.GetByIdAsync("1"))
            .ReturnsAsync((property, "Juan Pérez", "img1.jpg"));

        var result = await _propertyService.GetPropertyByIdAsync("1");

        Assert.IsNotNull(result);
        Assert.That(result!.Id, Is.EqualTo("1"));
        Assert.That(result.Name, Is.EqualTo("Casa Bonita"));
        Assert.That(result.OwnerName, Is.EqualTo("Juan Pérez"));
        Assert.That(result.MainImage, Is.EqualTo("img1.jpg"));
    }

    [Test]
    public async Task GetPropertyByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        _propertyRepositoryMock
            .Setup(r => r.GetByIdAsync("99"))
            .ReturnsAsync((ValueTuple<PropertyE, string?, string?>?)null);

        var result = await _propertyService.GetPropertyByIdAsync("99");

        Assert.IsNull(result);
    }

    [Test]
    public async Task GetPropertyByIdAsync_ShouldReturnEmptyImage_WhenNoImage()
    {
        var property = new PropertyE
        {
            Id = "2",
            Name = "Apartamento Moderno",
            Address = "Calle 2",
            Price = 200000,
            IdOwner = "O2"
        };

        _propertyRepositoryMock
            .Setup(r => r.GetByIdAsync("2"))
            .ReturnsAsync((property, "María López", null));

        var result = await _propertyService.GetPropertyByIdAsync("2");

        Assert.IsNotNull(result);
        Assert.That(result!.MainImage, Is.EqualTo(string.Empty));
    }
}
