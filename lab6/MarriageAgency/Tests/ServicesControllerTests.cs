using Azure;
using MarriageAgency.Data;
using MarriageAgency.Models;
using MarriageAgency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Tests
{
    public class ServicesControllerTests
    {
        [Fact]
        public void GetServiceList()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var services = TestDataHelper.GetFakeServicesList();
            marriageAgencyContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            //Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var result = servicesController.Get();

            // Assert
            var viewResult = Assert.IsType<List<ServicesViewModel>>(result);
            Assert.NotNull(viewResult);
            Assert.Equal(services.Count, result.Count);
        }

        [Fact]
        public void GetFilteredServiceLists()
        {
            // Arrange
            int clientId = 1;
            int employeeId = 1;
            int addirionalServiceId = 1;
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();

            var services = TestDataHelper.GetFakeServicesList()
                .Where(op => op.ClientId == clientId)
                .Where(op => op.EmployeeId == employeeId)
                .Where(op => op.AdditionalServiceId == addirionalServiceId)
                .ToList();
            marriageAgencyContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            //Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var result = servicesController.GetFilteredServices(employeeId, clientId, addirionalServiceId);

            // Assert
            var viewResult = Assert.IsType<List<ServicesViewModel>>(result);
            Assert.NotNull(viewResult);
            Assert.Equal(services.Count, result.Count);
        }

        [Fact]
        public void GetClients()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var clients = TestDataHelper.GetFakeClientsList();
            marriageAgencyContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            //Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var result = servicesController.GetClients();

            // Assert
            var viewResult = Assert.IsType<List<Client>>(result);
            Assert.NotNull(viewResult);
            Assert.Equal(clients.Count, viewResult.Count);
        }
        [Fact]
        public void GetEmployees()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var employees = TestDataHelper.GetFakeEmployeesList();
            marriageAgencyContextMock.Setup(x => x.Employees).ReturnsDbSet(employees);

            //Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var result = servicesController.GetEmployees();

            // Assert
            var viewResult = Assert.IsType<List<Employee>>(result);
            Assert.NotNull(viewResult);
            Assert.Equal(employees.Count, viewResult.Count);
        }

        [Fact]
        public void GetService()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var services = TestDataHelper.GetFakeServicesList();
            marriageAgencyContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            //Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var notFoundResult = servicesController.Get(400);
            var foundResult = servicesController.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.IsType<ObjectResult>(foundResult);
        }

        [Fact]
        public void GetAdditionalService()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var additionalServices = TestDataHelper.GetFakeAdditionalServicesList();
            marriageAgencyContextMock.Setup(x => x.AdditionalServices).ReturnsDbSet(additionalServices);

            //Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var result = servicesController.GetAdditionalServices();

            // Assert
            var viewResult = Assert.IsType<List<AdditionalService>>(result);
            Assert.NotNull(viewResult);
            Assert.Equal(additionalServices.Count, viewResult.Count);
        }

        [Fact]
        public void Post_ReturnsBadRequestResult()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var services = TestDataHelper.GetFakeServicesList();
            marriageAgencyContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            //Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            servicesController.ModelState.AddModelError("error", "some error");
            var result = servicesController.Post(service: null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Post_ReturnsOkObjectResult()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var services = TestDataHelper.GetFakeServicesList();
            marriageAgencyContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            Service service = new()
            {
                ServiceId = 200,
                EmployeeId = 3,
                ClientId = 3,
                AdditionalServiceId = 3,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Cost = 232,
                Client = TestDataHelper.GetFakeClientsList().SingleOrDefault(m => m.ClientId == 3),
                Employee = TestDataHelper.GetFakeEmployeesList().SingleOrDefault(m => m.EmployeeId == 3),
                AdditionalService = TestDataHelper.GetFakeAdditionalServicesList().SingleOrDefault(m => m.AdditionalServiceId == 3)
            };


            // Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var result = servicesController.Post(service);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public void Put_ReturnsNotFoundResult()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var services = TestDataHelper.GetFakeServicesList();
            marriageAgencyContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            Service service = new()
            {
                ServiceId = 200,
                EmployeeId = 3,
                ClientId = 3,
                AdditionalServiceId = 3,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Cost = 232,
                Client = TestDataHelper.GetFakeClientsList().SingleOrDefault(m => m.ClientId == 3),
                Employee = TestDataHelper.GetFakeEmployeesList().SingleOrDefault(m => m.EmployeeId == 3),
                AdditionalService = TestDataHelper.GetFakeAdditionalServicesList().SingleOrDefault(m => m.AdditionalServiceId == 3)
            };

            // Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            servicesController.ModelState.AddModelError("error", "some error");
            var result = servicesController.Put(service);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void Put_ReturnsOkObjectResult()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var services = TestDataHelper.GetFakeServicesList();
            marriageAgencyContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            Service service = new()
            {
                ServiceId = 1,
                EmployeeId = 3,
                ClientId = 3,
                AdditionalServiceId = 3,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Cost = 232,
                Client = TestDataHelper.GetFakeClientsList().SingleOrDefault(m => m.ClientId == 3),
                Employee = TestDataHelper.GetFakeEmployeesList().SingleOrDefault(m => m.EmployeeId == 3),
                AdditionalService = TestDataHelper.GetFakeAdditionalServicesList().SingleOrDefault(m => m.AdditionalServiceId == 3)
            };

            // Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var result = servicesController.Put(service);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            marriageAgencyContextMock.Verify();
        }


        [Fact]
        public void Delete_ReturnsNotFound()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var services = TestDataHelper.GetFakeServicesList();
            marriageAgencyContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            //Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var notFoundResult = servicesController.Delete(40);


            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void Delete_ReturnsOkObjectResult()
        {
            // Arrange
            var marriageAgencyContextMock = new Mock<MarriageAgencyContext>();
            var services = TestDataHelper.GetFakeServicesList();
            marriageAgencyContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            //Act
            ServicesController servicesController = new(marriageAgencyContextMock.Object);
            var result = servicesController.Delete(3);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            marriageAgencyContextMock.Verify();

        }
    }
}