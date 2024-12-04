namespace CinemaApp.Services.Tests
{
    using MockQueryable;
    using Moq;

    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using CinemaApp.Data.Seeding.DataTransferObjects;
    using Data;
    using Data.Interfaces;
    using Mapping;
    using Web.ViewModels;
    using Web.ViewModels.Movie;

    [TestFixture]
    public class Tests
    {
        // Two types of Unit Tests:
        // 1. Tests with Mocked Objects -> Ensures Atomic Testing, without impact from the module dependencies code
        //      * Pros -> Testing only the Service, ensures the independent testing of your code
        //      * Cons -> It differs from the real-life behavior of the application
        // 2. Test with In-Memory Database -> Tests the behavior of the unit taking into account the behavior of the ORM (EF Core)
        //      * Pros -> Close to the real-life scenario when working with a real database
        //      * Cons -> The tested unit is not atomic, you are testing both your unit and EF Core together
        // Setup particular Test Scenario
        private IList<Movie> moviesData = new List<Movie>()
        {
            new Movie()
            {
                Id = Guid.Parse("C994999B-02DD-46C2-ABC4-00C4787E629F"),
                Title = "Avatar",
                Genre = "Sci-Fi",
                ReleaseDate = new DateTime(2009, 12, 18),
                Director = "James Cameron",
                Duration = 162,
                Description = "A paraplegic Marine dispatched to the moon Pandora becomes torn between following orders and protecting his home.",
                ImageUrl = "https://www.movieposters.com/cdn/shop/files/avatar.adv.24x36_480x.progressive.jpg?v=1707410703",
            },
            new Movie()
            {
                Id = Guid.Parse("4571BF2F-DBB3-446C-A92A-07CB77F47ED0"),
                Title = "Inception",
                Genre = "Sci-Fi",
                ReleaseDate = new DateTime(2010, 7, 16),
                Director = "Christopher Nolan",
                Duration = 148,
                Description = "A skilled thief is given a chance at redemption if he can successfully perform an inception on a target's mind.",
                ImageUrl = "https://cdn.shopify.com/s/files/1/0057/3728/3618/files/inception.mpw.123395_9e0000d1-bc7f-400a-b488-15fa9e60a10c_500x749.jpg?v=1708527589",
            }
        };

        private Mock<IRepository<Movie, Guid>> movieRepository;
        private Mock<IRepository<Cinema, Guid>> cinemaRepository;
        private Mock<IRepository<CinemaMovie, object>> cinemaMovieRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly, 
                typeof(ImportMovieDto).Assembly);
        }

        [SetUp]
        public void Setup()
        {
            this.movieRepository = new Mock<IRepository<Movie, Guid>>();
            this.cinemaRepository = new Mock<IRepository<Cinema, Guid>>();
            this.cinemaMovieRepository = new Mock<IRepository<CinemaMovie, object>>();
        }

        [Test]
        public async Task GetAllMoviesNoFilterPositive()
        {
            IQueryable<Movie> moviesMockQueryable = moviesData.BuildMock();
            this.movieRepository
                .Setup(r => r.GetAllAttached())
                .Returns(moviesMockQueryable);

            IMovieService movieService = new MovieService(movieRepository.Object, cinemaRepository.Object, cinemaMovieRepository.Object);

            IEnumerable<AllMoviesIndexViewModel> allMoviesActual = await movieService
                .GetAllMoviesAsync(new AllMoviesSearchFilterViewModel());

            Assert.IsNotNull(allMoviesActual);
            Assert.AreEqual(this.moviesData.Count(), allMoviesActual.Count());

            allMoviesActual = allMoviesActual
                .OrderBy(m => m.Id)
                .ToList();

            int i = 0;
            foreach (AllMoviesIndexViewModel returnedMovie in allMoviesActual)
            {
                Assert.AreEqual(this.moviesData.OrderBy(m => m.Id).ToList()[i++].Id.ToString(), returnedMovie.Id);
            }
        }

        [Test]
        [TestCase("Av")]
        [TestCase("av")]
        public async Task GetAllMoviesSearchQueryPositive(string searchQuery)
        {
            int expectedMoviesCount = 1;
            string expectedMovieId = "C994999B-02DD-46C2-ABC4-00C4787E629F";

            IQueryable<Movie> moviesMockQueryable = moviesData.BuildMock();
            this.movieRepository
                .Setup(r => r.GetAllAttached())
                .Returns(moviesMockQueryable);

            IMovieService movieService = new MovieService(movieRepository.Object, cinemaRepository.Object, cinemaMovieRepository.Object);

            IEnumerable<AllMoviesIndexViewModel> allMoviesActual = await movieService
                .GetAllMoviesAsync(new AllMoviesSearchFilterViewModel()
                {
                    SearchQuery = "Av",
                });

            Assert.IsNotNull(allMoviesActual);
            Assert.AreEqual(expectedMoviesCount, allMoviesActual.Count());
            Assert.AreEqual(expectedMovieId.ToLower(), allMoviesActual.First().Id.ToLower());
        }

        [Test]
        public async Task GetAllMoviesNullFilterNegative()
        {
            IQueryable<Movie> moviesMockQueryable = moviesData.BuildMock();
            this.movieRepository
                .Setup(r => r.GetAllAttached())
                .Returns(moviesMockQueryable);

            IMovieService movieService = new MovieService(movieRepository.Object, cinemaRepository.Object, cinemaMovieRepository.Object);

            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                IEnumerable<AllMoviesIndexViewModel> allMoviesActual = await movieService
                    .GetAllMoviesAsync(null);
            });
        }
    }
}