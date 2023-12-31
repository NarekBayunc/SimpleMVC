﻿using SimpleMVC.Data.Enums;
using SimpleMVC.Models;

namespace SimpleMVC.Data
{
    public class AppDbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
               var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();
               context!.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            Name = "123",
                            Email = "123@gmail.com",
                            Age = 18,
                            Login = "123",
                            Password = Helper.HashPassword("123"),
                            Role = UserRole.Default,
                        },
                        new User()
                        {
                            Name = "321",
                            Email = "321@gmail.com",
                            Age = 18,
                            Login = "321",
                            Password = Helper.HashPassword("321"),
                            Role = UserRole.Admin,
                        }
                    });
                    await context.SaveChangesAsync();
                }

                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Cinema 1",
                            Description = "This is the description of the first cinema",
                            LogoData = Helper.FromImgToBytes("wwwroot/img/cinema-1.jpg")
                        },
                        new Cinema()
                        {
                            Name = "Cinema 2",
                            Description = "This is the description of the second cinema",
                            LogoData = Helper.FromImgToBytes("wwwroot/img/cinema-2.jpg")
                        },
                        new Cinema()
                        {
                            Name = "Cinema 3",
                            Description = "This is the description of the third cinema",
                            LogoData = Helper.FromImgToBytes("wwwroot/img/cinema-3.jpg")
                        },
                        new Cinema()
                        {
                            Name = "Cinema 4",
                            Description = "This is the description of the fourth cinema",
                            LogoData = Helper.FromImgToBytes("wwwroot/img/cinema-4.jpg")
                        },
                        new Cinema()
                        {
                            Name = "Cinema 5",
                            Description = "This is the description of the fifth cinema",
                            LogoData = Helper.FromImgToBytes("wwwroot/img/cinema-5.jpg")
                        }
                    });
                    await context.SaveChangesAsync();
                }
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Actor 1",
                            Bio = "This is the Bio of the first actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/actor-1.jpeg")

                        },
                        new Actor()
                        {
                            FullName = "Actor 2",
                            Bio = "This is the Bio of the second actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/actor-2.jpeg")
                        },
                        new Actor()
                        {
                            FullName = "Actor 3",
                            Bio = "This is the Bio of the third actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/actor-3.jpeg")
                        },
                        new Actor()
                        {
                            FullName = "Actor 4",
                            Bio = "This is the Bio of the fourth actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/actor-4.jpeg")
                        },
                        new Actor()
                        {
                            FullName = "Actor 5",
                            Bio = "This is the Bio of the fifth actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/actor-5.jpeg")
                        }
                    });
                    await context.SaveChangesAsync();
                }
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Producer 1",
                            Bio = "This is the Bio of the first actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/producer-1.jpeg")

                        },
                        new Producer()
                        {
                            FullName = "Producer 2",
                            Bio = "This is the Bio of the second actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/producer-2.jpeg")
                        },
                        new Producer()
                        {
                            FullName = "Producer 3",
                            Bio = "This is the Bio of the third actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/producer-3.jpeg")
                        },
                        new Producer()
                        {
                            FullName = "Producer 4",
                            Bio = "This is the Bio of the fourth actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/producer-4.jpeg")
                        },
                        new Producer()
                        {
                            FullName = "Producer 5",
                            Bio = "This is the Bio of the fifth actor",
                            PictureData = Helper.FromImgToBytes("wwwroot/img/producer-5.jpeg")
                        }
                    });
                    await context.SaveChangesAsync();
                }
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Life",
                            Description = "This is the Life movie description",
                            Price = 39.50,
                            PictureData = Helper.FromImgToBytes("wwwroot/img/movie-1.jpeg"),
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            CinemaId = 3,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "The Shawshank Redemption",
                            Description = "This is the Shawshank Redemption description",
                            Price = 29.50,
                            PictureData = Helper.FromImgToBytes("wwwroot/img/movie-2.jpeg"),
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            Name = "Ghost",
                            Description = "This is the Ghost movie description",
                            Price = 39.50,
                            PictureData = Helper.FromImgToBytes("wwwroot/img/movie-3.jpeg"),
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            CinemaId = 4,
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Horror
                        },
                        new Movie()
                        {
                            Name = "Race",
                            Description = "This is the Race movie description",
                            Price = 39.50,
                            PictureData = Helper.FromImgToBytes("wwwroot/img/movie-4.jpeg"),
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            CinemaId = 1,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "Scoob",
                            Description = "This is the Scoob movie description",
                            Price = 39.50,
                            PictureData = Helper.FromImgToBytes("wwwroot/img/movie-5.jpeg"),
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            CinemaId = 1,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name = "Cold Soles",
                            Description = "This is the Cold Soles movie description",
                            Price = 39.50,
                            PictureData = Helper.FromImgToBytes("wwwroot/img/movie-6.jpeg"),
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            CinemaId = 1,
                            ProducerId = 5,
                            MovieCategory = MovieCategory.Drama
                        }
                    });
                    await context.SaveChangesAsync();
                }
                if (!context.ActorsMovies.Any())
                {
                    context.ActorsMovies.AddRange(new List<ActorMovie>()
                    {
                        new ActorMovie()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new ActorMovie()
                        {
                            ActorId = 3,
                            MovieId = 1
                        },
                         new ActorMovie()
                        {
                            ActorId = 1,
                             MovieId = 2
                        },
                         new ActorMovie()
                        {
                            ActorId = 4,
                             MovieId = 2
                        },
                        new ActorMovie()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                        new ActorMovie()
                        {
                            ActorId = 2,
                            MovieId = 3
                        },
                        new ActorMovie()
                        {
                            ActorId = 5,
                            MovieId = 3
                        },
                        new ActorMovie()
                        {
                            ActorId = 2,
                            MovieId = 4
                        },
                        new ActorMovie()
                        {
                            ActorId = 3,
                            MovieId = 4
                        },
                        new ActorMovie()
                        {
                            ActorId = 4,
                            MovieId = 4
                        },
                        new ActorMovie()
                        {
                            ActorId = 2,
                            MovieId = 5
                        },
                        new ActorMovie()
                        {
                            ActorId = 3,
                            MovieId = 5
                        },
                        new ActorMovie()
                        {
                            ActorId = 4,
                            MovieId = 5
                        },
                        new ActorMovie()
                        {
                            ActorId = 5,
                            MovieId = 5
                        },
                        new ActorMovie()
                        {
                            ActorId = 3,
                            MovieId = 6
                        },
                        new ActorMovie()
                        {
                            ActorId = 4,
                            MovieId = 6
                        },
                        new ActorMovie()
                        {
                            ActorId = 5,
                            MovieId = 6
                        },
                    });
                    await context.SaveChangesAsync();
                }
                await context.SaveChangesAsync();
            }
        }
    }
}

