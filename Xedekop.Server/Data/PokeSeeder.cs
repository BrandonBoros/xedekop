using Microsoft.AspNetCore.Identity;
using PokeApiNet;
using System.Text.Json;
using Xedekop.Server.Data.Entities;

namespace Xedekop.Server.Data
{
    public class PokeSeeder
    {
        #region Properties
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        #endregion Properties

        #region Contructors
        public PokeSeeder(AppDbContext context,IWebHostEnvironment hosting, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _db = context;
            _hosting = hosting;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion Constructors

        #region Seeding Method 
        public async Task Seed()
        {
            //Verify that the database exists. Hover over the method and read the documentation. 
            _db.Database.EnsureCreated();

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole() { Name = "Admin"});
                await _roleManager.CreateAsync(new AppRole() { Name = "Normal" });
                await _roleManager.CreateAsync(new AppRole() { Name = "Supervisor" });
            }

            if (!_userManager.Users.Any())
            {
                var user = new AppUser() { UserName = "admin@email.com", Email = "admin@email.com" };
                await _userManager.CreateAsync(user, "VerySecureAdmin45%");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            // If there are no Pokemon, seed.
            if (!_db.Pokemons.Any())
            {
                // get pokeapi client
                PokeApiClient pokeClient = new PokeApiClient();

                var page = await pokeClient.GetNamedResourcePageAsync<PokeApiNet.Pokemon>(100, 0);
                List<Entities.Pokemon> pokemons = new();

                // We were getting rate limited so we needed to change it to use pages.
                while (page != null)
                {
                    foreach (var item in page.Results)
                    {
                        PokeApiNet.Pokemon pokemon;

                        try
                        {
                            pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(item.Name);
                        }
                        catch
                        {
                            await Task.Delay(500);
                            pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(item.Name);
                        }

                        if (!pokemon.IsDefault) continue;

                        var types = GetTypes(pokemon);

                        pokemons.Add(new Entities.Pokemon()
                        {
                            Name = GetName(pokemon),
                            Type1 = types[0].ToString()!,
                            Type2 = types[1]?.ToString(),
                            Price = pokemon.Stats[0].BaseStat,
                            Sprite = pokemon.Sprites.FrontDefault,
                            ShinySprite = pokemon.Sprites.FrontShiny
                        });
                    }

                    // Stop if no more pages
                    if (page.Next == null)
                        break;

                    // Extract offset & limit from "Next" URL
                    var nextUrl = new Uri(page.Next);
                    var query = System.Web.HttpUtility.ParseQueryString(nextUrl.Query);

                    int nextLimit = int.Parse(query["limit"]);
                    int nextOffset = int.Parse(query["offset"]);

                    page = await pokeClient.GetNamedResourcePageAsync<PokeApiNet.Pokemon>(nextLimit, nextOffset);
                }


                //Add the new list of products to the database
                _db.Pokemons.AddRange(pokemons);

                //Create a sample order 
                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Pokemon = pokemons.First(),
                            Quantity = 5,
                            UnitPrice = pokemons.First().Price
                        }
                    }
                };

                _db.Orders.Add(order);

                _db.SaveChanges();  //commit changes to the database (make permanent) 
            }
        }

        #endregion Seeding Method

        #region Helper Methods
        /// <summary>
        /// Get the types for the specific passed pokemon.
        /// </summary>
        /// <param name="pokemon">Pokemon to get the types of.</param>
        /// <returns>The types as the enum.</returns>
        private Entities.Type?[] GetTypes(PokeApiNet.Pokemon pokemon)
        {
            Entities.Type?[] types = new Entities.Type?[2];

            // converts the pokeClient type name to enum
            var typeName1 = pokemon.Types.ToArray()[0].Type.Name;

            // ignore case makes it so capitalization doesn't matter
            if (Enum.TryParse<Entities.Type>(typeName1, ignoreCase: true, out var parsedType1))
            {
                types[0] = parsedType1;
            }

            // only if there is a second type (usually not)
            if (pokemon.Types.Count > 1)
            {
                // converts the pokeClient type name to enum
                var typeName2 = pokemon.Types.ToArray()[1].Type.Name;

                // ignore case makes it so capitalization doesn't matter
                if (Enum.TryParse<Entities.Type>(typeName2, ignoreCase: true, out var parsedType2))
                {
                    types[1] = parsedType2;
                }
            }

            return types;
        }

        private string GetName(PokeApiNet.Pokemon pokemon)
        {
            char firstLetter = char.ToUpper(pokemon.Name[0]);

            // Get the rest of the string.
            string restOfString = pokemon.Name.Substring(1);

            // Concatenate them to form the new string.
            string capitalizedString = firstLetter + restOfString;

           return capitalizedString;
        }
        #endregion Helper Methods
    }
}
