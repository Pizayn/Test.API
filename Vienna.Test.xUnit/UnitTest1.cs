using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vienna.Test.API.Controllers;
using Vienna.Test.API.Entites;
using Vienna.Test.API.Repositories;
using Vienna.Test.API.Services;
using Vienna.Test.API.Services.Implementation;
using Xunit;

namespace Vienna.Test.xUnit
{
    public class UnitTest1
    {
        private readonly Mock<IGithubService> _mockGithubService;
        private readonly Mock<IMonarchRepository> _mockMonarchRepository;
        private readonly MonarchController _controller;

        private readonly IMonarchService _monarchService;


        public UnitTest1()
        {
            // Mock the IGithubService
            _mockGithubService = new Mock<IGithubService>();
            _mockMonarchRepository = new Mock<IMonarchRepository>();


            // Instantiate the controller with the mocked service
            _controller = new MonarchController(_mockMonarchRepository.Object);
            _monarchService = new MonarchService(_mockGithubService.Object, _mockMonarchRepository.Object);
        }

        [Fact]
        public async Task ShouldReturnMonarch()
        {
            // Arrange
            var mockMonarchs = new List<Monarch>
            {
                new Monarch { Nm = "Henry VIII" },
                new Monarch { Nm = "Elizabeth I" }
            };
            _mockGithubService.Setup(service => service.GetAllStats()).ReturnsAsync(mockMonarchs);

           // _mockGithubService.Setup(service => service.GetAllStats()).ReturnsAsync(mockMonarchs);

            // Act
            var result = await _controller.GetAllMonarchList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnMonarchs = Assert.IsType<List<Monarch>>(okResult.Value);
            Assert.Equal(2, returnMonarchs.Count);
            Assert.Equal("Henry VIII", returnMonarchs[0].Nm);
            Assert.Equal("Elizabeth I", returnMonarchs[1].Nm);
        }

        [Fact]
        public async Task ShouldReturnMonarchList()
        {
            var monarchList = new List<Monarch>
            {
                new Monarch {Nm = "Edward"},
                new Monarch {Nm ="Elizabet"}
            };

            //given
            _mockGithubService.Setup(service => service.GetAllStats()).ReturnsAsync(monarchList);
            var result = await _monarchService.GetAll();
            var returnMonarchs = Assert.IsType<List<Monarch>>(result);

            Assert.Equal(2, result.Count());
            Assert.Equal("Edward", returnMonarchs[0].Nm);
            Assert.Equal("Elizabet", returnMonarchs[1].Nm);

        }

        [Fact]
        public async Task ShouldCalculateYear()
        {
            var monarchList = new List<Monarch>()
            {
                new Monarch {Yrs = "1991"},
                new Monarch {Yrs = "1992"}
            };
            _mockGithubService.Setup(service => service.GetAllStats()).ReturnsAsync(monarchList);


            var result = await _monarchService.GetAll();
            var resultType = Assert.IsType<List<Monarch>>(result);

            Assert.Equal(2, resultType.Count());

            result.First().Yrs.Should().Be("1991");
        }

    }
}

// Fluent Assertions ile Yapýlabilecek Bazý Yaygýn Doðrulamalar:

// Deðer eþitliðini kontrol etmek:
// result.Should().Be(expectedValue);

// Nesnenin null olmadýðýný kontrol etmek:
// result.Should().NotBeNull();

// Bir koleksiyonun belirli bir elemana sahip olduðunu kontrol etmek:
// collection.Should().Contain(item);

// Bir koleksiyonun boþ olmadýðýný kontrol etmek:
// collection.Should().NotBeEmpty();

// Bir string'in belirli bir metni içerip içermediðini kontrol etmek:
// text.Should().Contain("expected substring");

// Bir nesnenin belirli bir türe ait olup olmadýðýný kontrol etmek:
// result.Should().BeOfType<ExpectedType>();

// Bir sayýnýn belirli bir deðere yakýn olup olmadýðýný kontrol etmek (kayan noktalý sayýlar için):
// result.Should().BeApproximately(expectedValue, precision);

// Bir koleksiyonun belirli bir sýraya sahip olup olmadýðýný kontrol etmek:
// collection.Should().BeInAscendingOrder();

// Bir nesnenin baþka bir nesneyle ayný özelliklere (property) sahip olup olmadýðýný kontrol etmek:
// result.Should().BeEquivalentTo(expectedObject);

// Bir iþlemin belirli bir süre içinde tamamlanýp tamamlanmadýðýný kontrol etmek:
// action.ExecutionTime().Should().BeLessThan(TimeSpan.FromSeconds(5));

// Bir koleksiyonun belirli bir sayýda öðeye sahip olup olmadýðýný kontrol etmek:
// collection.Should().HaveCount(expectedCount);

// Bir string'in belirli bir metinle baþlayýp baþlamadýðýný kontrol etmek:
// text.Should().StartWith("expected start");

// Bir string'in belirli bir metinle bitip bitmediðini kontrol etmek:
// text.Should().EndWith("expected end");

// Bir koþulun her zaman doðru olup olmadýðýný kontrol etmek (örneðin, bir döngü veya koleksiyon üzerinde):
// collection.Should().OnlyContain(x => x.SomeProperty == expectedValue);

// Bir nesnenin belirli bir özelliðin deðerine sahip olup olmadýðýný kontrol etmek:
// result.Should().Match<ExpectedType>(x => x.SomeProperty == expectedValue);
