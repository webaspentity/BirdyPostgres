using Birdy.Shared;
using Birdy.Shared.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Server.AppData;

public class ApplicationDatabaseContext : DbContext
{
    public ApplicationDatabaseContext() : base()
    {
    }

    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    #region Таблицы

    public DbSet<User> Users => Set<User>();
    public DbSet<LoginData> Logins => Set<LoginData>();
    public DbSet<Birdy.Shared.Profile> Profiles => Set<Birdy.Shared.Profile>();
    public DbSet<Birdy.Shared.Cart> Carts => Set<Birdy.Shared.Cart>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
    public DbSet<Raiting> Raitings => Set<Raiting>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Bird> Birds => Set<Bird>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<ProductIngredient> ProductIngredients => Set<ProductIngredient>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<ProductOrder> ProductOrders => Set<ProductOrder>();
    public DbSet<BirdProduct> BirdProducts => Set<BirdProduct>();

    #endregion Таблицы

    #region Строка подключения

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Server=81.200.151.143;Port=5432;Database=default_db;User Id=gen_user;Password=hXigb4MpThLd;");
        //options.UseNpgsql("Host=localhost;Port=5000;Database=birdydb;Username=postgres;Password=12345678");
        //options.UseNpgsql("Server=localhost;Port=32768;Database=birdy;User Id=postgres;Password=postgrespw;");
        options.EnableSensitiveDataLogging(true);
    }

    #endregion Строка подключения

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Пользователи

        modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("int");
        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(u => u.Id);
            u.Property(u => u.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<User>().Property(u => u.Role).HasColumnType("varchar").HasMaxLength(20);

        #endregion Пользователи

        #region Логины пользователей

        modelBuilder.Entity<LoginData>().Property(l => l.Id).HasColumnType("int");
        modelBuilder.Entity<LoginData>(l =>
        {
            l.HasKey(l => l.Id);
            l.Property(l => l.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<LoginData>().Property(l => l.Email).HasColumnType("varchar").HasMaxLength(70).IsRequired();
        modelBuilder.Entity<LoginData>().HasIndex(l => l.Email).IsUnique(true);
        modelBuilder.Entity<LoginData>().Property(l => l.Password).HasColumnType("varchar").HasMaxLength(60).IsRequired();

        #endregion Логины пользователей

        #region Профили пользователей

        modelBuilder.Entity<Birdy.Shared.Profile>().Property(p => p.Id).HasColumnType("int");
        modelBuilder.Entity<Birdy.Shared.Profile>(p =>
        {
            p.HasKey(p => p.Id);
            p.Property(p => p.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Birdy.Shared.Profile>().Property(p => p.UserName).HasColumnType("varchar").HasMaxLength(60);
        modelBuilder.Entity<Birdy.Shared.Profile>().Property(p => p.UserTelephone).HasColumnType("varchar").HasMaxLength(25);
        modelBuilder.Entity<Birdy.Shared.Profile>().HasIndex(p => p.UserTelephone).IsUnique(true);
        modelBuilder.Entity<Birdy.Shared.Profile>().Property(p => p.UserAddress).HasColumnType("varchar").HasMaxLength(150);

        #endregion Профили пользователей

        #region Категории товаров

        modelBuilder.Entity<Category>().Property(c => c.Id).HasColumnType("int");
        modelBuilder.Entity<Category>(c =>
        {
            c.HasKey(c => c.Id);
            c.Property(c => c.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnType("varchar").HasMaxLength(60).IsRequired();
        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Category>().Property(c => c.Url).HasColumnType("varchar").HasMaxLength(60).IsRequired();

        #endregion Категории товаров

        #region Товары

        modelBuilder.Entity<Product>().Property(p => p.Id).HasColumnType("int");
        modelBuilder.Entity<Product>(p =>
        {
            p.HasKey(p => p.Id);
            p.Property(p => p.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Description).HasColumnType("varchar");
        modelBuilder.Entity<Product>().Property(p => p.Manufacturer).HasColumnType("varchar").HasMaxLength(100);

        #endregion Товары

        #region Ингредиент

        modelBuilder.Entity<Ingredient>().Property(i => i.Id).HasColumnType("int");
        modelBuilder.Entity<Ingredient>(i =>
        {
            i.HasKey(i => i.Id);
            i.Property(i => i.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Ingredient>().Property(i => i.Name).HasColumnType("varchar").HasMaxLength(60).IsRequired();
        modelBuilder.Entity<Ingredient>().Property(i => i.Description).HasColumnType("varchar");

        #endregion Ингредиент

        #region Товары и Ингредиенты

        modelBuilder.Entity<ProductIngredient>().Property(pi => pi.Id).HasColumnType("int");
        modelBuilder.Entity<ProductIngredient>(pi =>
        {
            pi.HasKey(pi => pi.Id);
            pi.Property(pi => pi.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<ProductIngredient>().HasKey(pi => new { pi.ProductId, pi.IngredientId });

        #endregion Товары и Ингредиенты

        #region Товары и Цены

        modelBuilder.Entity<ProductPrice>().Property(pp => pp.Id).HasColumnType("int");
        modelBuilder.Entity<ProductPrice>(epp =>
        {
            epp.HasKey(pp => pp.Id);
            epp.Property(pp => pp.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<ProductPrice>().Property(pp => pp.Weight).HasColumnType("int");
        modelBuilder.Entity<ProductPrice>().Property(pp => pp.Price).HasColumnType("money");
        modelBuilder.Entity<ProductPrice>().Property(pp => pp.InStock).HasColumnType("int");

        #endregion Товары и Цены

        #region Товар в Корзине

        modelBuilder.Entity<CartItem>().Property(ci => ci.Id).HasColumnType("int");
        modelBuilder.Entity<CartItem>(eci =>
        {
            eci.HasKey(ci => ci.Id);
            eci.Property(ci => ci.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<CartItem>().Property(ci => ci.Quantity).HasColumnType("int");
        modelBuilder.Entity<CartItem>().Property(ci => ci.Weight).HasColumnType("int");
        modelBuilder.Entity<CartItem>().Property(ci => ci.Price).HasColumnType("money");

        #endregion Товар в Корзине

        #region Корзина

        modelBuilder.Entity<Birdy.Shared.Cart>().Property(c => c.Id).HasColumnType("int");
        modelBuilder.Entity<Birdy.Shared.Cart>(ec =>
        {
            ec.HasKey(c => c.Id);
            ec.Property(c => c.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Birdy.Shared.Cart>().Property(c => c.TotalCost).HasColumnType("money");

        #endregion Корзина

        #region Рейтинг товара

        modelBuilder.Entity<Raiting>().Property(r => r.Id).HasColumnType("int");
        modelBuilder.Entity<Raiting>(r =>
        {
            r.HasKey(r => r.Id);
            r.Property(r => r.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Raiting>().Property(r => r.Value).HasColumnType("smallint");
        modelBuilder.Entity<Raiting>().HasKey(r => new { r.ProductId, r.UserId });

        #endregion Рейтинг товара

        #region Отзывы пользователей

        modelBuilder.Entity<Review>().Property(r => r.Id).HasColumnType("int");
        modelBuilder.Entity<Review>(r =>
        {
            r.HasKey(r => r.Id);
            r.Property(r => r.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Review>().Property(r => r.Comment).HasColumnType("varchar");
        modelBuilder.Entity<Review>().Property(r => r.ReviewDate).HasColumnType("date");
        modelBuilder.Entity<Review>().HasKey(r => new { r.ProductId, r.UserId });

        #endregion Отзывы пользователей

        #region Птица

        modelBuilder.Entity<Bird>().Property(b => b.Id).HasColumnType("int");
        modelBuilder.Entity<Bird>(eb =>
        {
            eb.HasKey(b => b.Id);
            eb.Property(b => b.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Bird>().Property(b => b.Name).HasColumnType("varchar").HasMaxLength(50);
        modelBuilder.Entity<Bird>().Property(b => b.Description).HasColumnType("varchar");
        modelBuilder.Entity<Bird>().Property(b => b.NutritionType).HasColumnType("varchar").HasMaxLength(25);
        modelBuilder.Entity<Bird>().Property(b => b.Size).HasColumnType("varchar").HasMaxLength(25);
        modelBuilder.Entity<Bird>().Property(b => b.ImageSource).HasColumnType("varchar").HasMaxLength(100);

        #endregion Птица

        #region Птицы и Товары

        modelBuilder.Entity<BirdProduct>().Property(bp => bp.Id).HasColumnType("int");
        modelBuilder.Entity<BirdProduct>(ebp =>
        {
            ebp.HasKey(bp => bp.Id);
            ebp.Property(bp => bp.Id).ValueGeneratedOnAdd();
        });

        #endregion

        #region Заказ

        modelBuilder.Entity<Order>().Property(o => o.Id).HasColumnType("int");
        modelBuilder.Entity<Order>(eo =>
        {
            eo.HasKey(o => o.Id);
            eo.Property(o => o.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Order>().Property(o => o.OrderDate).HasColumnType("date");
        modelBuilder.Entity<Order>().Property(o => o.DeliveryDate).HasColumnType("date");
        modelBuilder.Entity<Order>().Property(o => o.Comment).HasColumnType("varchar").HasMaxLength(250);
        modelBuilder.Entity<Order>().Property(o => o.Price).HasColumnType("money");
        modelBuilder.Entity<Order>().Property(o => o.IsPaid).HasColumnType("boolean").IsRequired();
        modelBuilder.Entity<Order>().Property(o => o.Status).HasColumnType("varchar(25)");

        #endregion Заказ

        #region Продукты и Заказы

        modelBuilder.Entity<ProductOrder>().Property(o => o.Id).HasColumnType("int");
        modelBuilder.Entity<ProductOrder>(epo =>
        {
            epo.HasKey(po => po.Id);
            epo.Property(po => po.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<ProductOrder>().Property(o => o.Quantity).HasColumnType("int");

        #endregion Продукты и Заказы

        #region Скидка

        modelBuilder.Entity<Discount>().Property(d => d.Id).HasColumnType("int");
        modelBuilder.Entity<Discount>(ed =>
        {
            ed.HasKey(d => d.Id);
            ed.Property(d => d.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Discount>().Property(d => d.Value).HasColumnType("int");
        modelBuilder.Entity<Discount>().Property(d => d.StartDate).HasColumnType("date");
        modelBuilder.Entity<Discount>().Property(d => d.EndDate).HasColumnType("date");

        #endregion Скидка

        #region Отношения

        modelBuilder.Entity<User>().HasOne(u => u.Profile).WithOne(p => p.User).HasForeignKey<Birdy.Shared.Profile>(p => p.UserId);
        modelBuilder.Entity<User>().HasOne(u => u.LoginData).WithOne(l => l.User).HasForeignKey<LoginData>(l => l.UserId);
        modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
        modelBuilder.Entity<ProductIngredient>().HasOne(pi => pi.Product).WithMany(p => p.Ingredients).HasForeignKey(pi => pi.ProductId);
        modelBuilder.Entity<ProductIngredient>().HasOne(pi => pi.Ingredient).WithMany(i => i.Products).HasForeignKey(pi => pi.IngredientId);
        modelBuilder.Entity<ProductPrice>().HasOne(pp => pp.Product).WithMany(p => p.ProductsPrices).HasForeignKey(pp => pp.ProductId);
        modelBuilder.Entity<Raiting>().HasOne(r => r.Product).WithMany(p => p.Raitings).HasForeignKey(r => r.ProductId);
        modelBuilder.Entity<Raiting>().HasOne(r => r.User).WithMany(u => u.Raitings).HasForeignKey(r => r.UserId);
        modelBuilder.Entity<Review>().HasOne(r => r.Product).WithMany(p => p.Reviews).HasForeignKey(r => r.ProductId);
        modelBuilder.Entity<Review>().HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId);
        modelBuilder.Entity<User>().HasOne(u => u.Cart).WithOne(c => c.User).HasForeignKey<Birdy.Shared.Cart>(c => c.UserId);
        modelBuilder.Entity<Order>().HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);
        modelBuilder.Entity<CartItem>().HasOne(ci => ci.Cart).WithMany(c => c.Items).HasForeignKey(ci => ci.CartId);
        modelBuilder.Entity<CartItem>().HasOne(ci => ci.Product).WithMany(p => p.CartItems).HasForeignKey(ci => ci.ProductId);
        modelBuilder.Entity<Discount>().HasOne(d => d.ProductPrice).WithMany(pp => pp.Discounts).HasForeignKey(d => d.ProductPriceId);
        modelBuilder.Entity<ProductOrder>().HasOne(po => po.Product).WithMany(p => p.ProductOrders).HasForeignKey(po => po.ProductId);
        modelBuilder.Entity<ProductOrder>().HasOne(po => po.Order).WithMany(o => o.ProductOrders).HasForeignKey(po => po.OrderId);
        modelBuilder.Entity<BirdProduct>().HasOne(bp => bp.Bird).WithMany(b => b.BirdProducts).HasForeignKey(bp => bp.BirdId);
        modelBuilder.Entity<BirdProduct>().HasOne(bp => bp.Product).WithMany(p => p.BirdProducts).HasForeignKey(bp => bp.ProductId);

        #endregion Отношения

        #region Заполнение таблиц

        #region Пользователи

        modelBuilder.Entity<User>().HasData(
             new User { Id = 1, Role = UserRole.Admin },
             new User { Id = 2, Role = UserRole.User },
             new User { Id = 3, Role = UserRole.User },
             new User { Id = 4, Role = UserRole.User },
             new User { Id = 5, Role = UserRole.User }, 
             new User { Id = 6, Role = UserRole.User }, 
             new User { Id = 7, Role = UserRole.User }
             );

        modelBuilder.Entity<LoginData>().HasData(
            new LoginData { Id = 1, Email = "admin@email.com", Password = BCrypt.Net.BCrypt.HashPassword("12345678"), UserId = 1 },
            new LoginData { Id = 2, Email = "user1@email.com", Password = BCrypt.Net.BCrypt.HashPassword("11111111"), UserId = 2 },
            new LoginData { Id = 3, Email = "user2@email.com", Password = BCrypt.Net.BCrypt.HashPassword("22222222"), UserId = 3 },
            new LoginData { Id = 4, Email = "user3@email.com", Password = BCrypt.Net.BCrypt.HashPassword("22222222"), UserId = 4 },
            new LoginData { Id = 5, Email = "user4@email.com", Password = BCrypt.Net.BCrypt.HashPassword("22222222"), UserId = 5 },
            new LoginData { Id = 6, Email = "user5@email.com", Password = BCrypt.Net.BCrypt.HashPassword("22222222"), UserId = 6 },
            new LoginData { Id = 7, Email = "user6@email.com", Password = BCrypt.Net.BCrypt.HashPassword("22222222"), UserId = 7 }
            );

        modelBuilder.Entity<Birdy.Shared.Profile>().HasData(
            new Birdy.Shared.Profile { Id = 1, UserId = 1, UserName = "Администратор" },
            new Birdy.Shared.Profile { Id = 2, UserId = 2, UserName = "Пользователь 1", UserTelephone = "+7 (926) 111-11-11", UserAddress = "г. Москва, ул. Пользователя 1" },
            new Birdy.Shared.Profile { Id = 3, UserId = 3, UserName = "Пользователь 2", UserTelephone = "+7 (926) 222-22-22", UserAddress = "г. Москва, ул. Пользователя 2" },
            new Birdy.Shared.Profile { Id = 4, UserId = 4, UserName = "Пользователь 3", UserTelephone = "+7 (926) 333-33-33", UserAddress = "г. Москва, ул. Пользователя 3" },
            new Birdy.Shared.Profile { Id = 5, UserId = 5, UserName = "Пользователь 4", UserTelephone = "+7 (926) 444-44-44", UserAddress = "г. Москва, ул. Пользователя 4" }, 
            new Birdy.Shared.Profile { Id = 6, UserId = 6, UserName = "Пользователь 5", UserTelephone = "+7 (926) 555-55-55", UserAddress = "г. Москва, ул. Пользователя 5" }, 
            new Birdy.Shared.Profile { Id = 7, UserId = 7, UserName = "Пользователь 6", UserTelephone = "+7 (926) 666-66-66", UserAddress = "г. Москва, ул. Пользователя 6" }
            );

        modelBuilder.Entity<Birdy.Shared.Cart>().HasData(
            new Birdy.Shared.Cart { Id = 1, UserId = 1 },
            new Birdy.Shared.Cart { Id = 2, UserId = 2 },
            new Birdy.Shared.Cart { Id = 3, UserId = 3 },
            new Birdy.Shared.Cart { Id = 4, UserId = 4 },
            new Birdy.Shared.Cart { Id = 5, UserId = 5 },
            new Birdy.Shared.Cart { Id = 6, UserId = 6 },
            new Birdy.Shared.Cart { Id = 7, UserId = 7 }
            );

        #endregion Пользователи

        #region Категории

        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = ProductCategory.Carriers, Url = "carriers" },
            new { Id = 2, Name = ProductCategory.Food, Url = "food" },
            new { Id = 3, Name = ProductCategory.Feeders, Url = "feeders" },
            new { Id = 4, Name = ProductCategory.Goodies, Url = "goodies" },
            new { Id = 5, Name = ProductCategory.CareProducts, Url = "careproducts" },
            new { Id = 6, Name = ProductCategory.Cells, Url = "cells" },
            new { Id = 7, Name = ProductCategory.Toys, Url = "toys" },
            new { Id = 8, Name = ProductCategory.Fillers, Url = "fillers" },
            new { Id = 9, Name = ProductCategory.Accessories, Url = "accessories" }
            );

        #endregion Категории

        #region Птицы

        modelBuilder.Entity<Bird>().HasData(
            new Bird
            {
                Id = 1,
                Name = BirdSpecies.LiitleParrot,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Little,
                Description = "Среди множества видов декоративных птиц маленькие попугаи – самые востребованные. Такая популярность объясняется не только простотой " +
                "содержания, но и количеством позитива, исходящего от этих пернатых. Миниатюрные птички забавны, миловидны и умны. Они прекрасные собеседники: всегда выслушают " +
                "хозяина и даже «поддержат» разговор. Если вы хотите завести экзотическую птичку, обратите внимание на представителей маленьких видов: дятловых, воробьиных, фиговых, " +
                "волнистых попугаев или неразлучников. За ними легко ухаживать. Малые размеры птиц – не единственное их положительное качество. Они легко приспосабливаются к " +
                "жизни рядом с человеком.",
                ImageSource = "/images/birds/littleparrot.jpg"
            },
            new Bird
            {
                Id = 2,
                Name = BirdSpecies.MediumParrot,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Medium,
                Description = "Многие люди в качестве домашних питомцев выбирают средних попугаев. Эти птички отличаются относительно небольшими размерами и красотой, благодаря " +
                "чему их можно успешно содержать в городской малогабаритной квартире. Попугаи средних размеров насчитывают множество родов и видов. Самыми популярными экземплярами, " +
                "живущими рядом с человеком, являются корелла, лори, розелла, кольчатый и сенегальский попугаи.",
                ImageSource = "/images/birds/mediumparrot.jpg"
            },
            new Bird
            {
                Id = 3,
                Name = BirdSpecies.LargeParrot,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Large,
                Description = "На сегодняшний день на планете живет около 350 видов попугаев. Из них крупные породы представлены в незначительном количестве. Некоторые из них " +
                "находятся под угрозой вымирания и занесены в Красную книгу. Цены на таких питомцев высоки, но и спрос на них не угасает. Среди крупных попугаев наиболее известны: ара, " +
                "амазонский попугай, какаду, какаду маффин.",
                ImageSource = "/images/birds/largeparrot.jpg"
            },
            new Bird
            {
                Id = 4,
                Name = BirdSpecies.Canary,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Little,
                Description = "Канарейка птица маленькая. Благодаря своему умению красиво разливать трели, даже несмотря на скромное оперение, она завоевала популярность и любовь " +
                "многих людей. Певунья не приносит неудобств, а только удовольствие от одного своего неприхотливого вида, незамысловатого щебетания, которое сменяется удивительным " +
                "по звучанию пением. Клетка не занимает много места в квартире. За ней легко ухаживать и не нужно тратить время на прогулки с питомцем.",
                ImageSource = "/images/birds/canary.jpg"
            },
            new Bird
            {
                Id = 5,
                Name = BirdSpecies.Amadina,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Little,
                Description = " Амадины - это общительные птицы, которые любят взаимодействовать с людьми. Для содержания не требуют большие финансовые вложения и " +
                "физические усилия. Благодаря такой неприхотливости в сочетании с миролюбивым характером, яркие певчие птички завоевали широкую популярность. «Пение» амадин специфичное и " +
                "порой бывает неприятным: они издают тихое щебетание, свист, жужжание, бурчание, шипение. У некоторых амадин приятный голос, но поют только самцы. " +
                "Привлекательные, подвижные, красивые и очень доверчивые, они моментально завоевывают сердца любителей птиц.",
                ImageSource = "/images/birds/amadina.jpg"
            },
            new Bird
            {
                Id = 6,
                Name = BirdSpecies.Pigeon,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Medium,
                Description = "Голубь – птица мира, которая на протяжении сотни лет служит человеку и выступает в качестве друга. Еще в давние времена, когда не было почты, " +
                "в качестве почтальонов выступали именно эти замечательные птицы. Они доставляли письма адресату и возвращались домой к хозяину. Голубей любят и ценят в нынешние " +
                "дни, некоторые люди мечтают завести крылатого ангела у себя дома, в квартире в том числе. Домашние голуби способны приносить эстетическое удовлетворение своим " +
                "ярким внешним видом или необычным стилем полета.",
                ImageSource = "/images/birds/pigeon.jpg"
            },
            new Bird
            {
                Id = 7,
                Name = BirdSpecies.Raven,
                NutritionType = $"{NutritionTypes.Omnivorous}",
                Size = BirdSize.Large,
                Description = "Ворон – это символичный, для некоторых даже эпичный представитель пернатых. Многие орнитологи считают врановых самыми умными из птиц: они способны " +
                "различать цвета и формы, запоминают запахи, реагируют на тембр голоса, имеют навыки к подражанию звуков. Ручной ворон отличается от сородичей невероятным интеллектом, " +
                "нетипичным для птиц. Он кардинально отличается от попугаев и других пернатых, а еще обладает невероятной привязанностью, как верный пес.",
                ImageSource = "/images/birds/raven.jpeg"
            },
            new Bird
            {
                Id = 8,
                Name = BirdSpecies.Nightingale,
                NutritionType = $"{NutritionTypes.Insectivorous}",
                Size = BirdSize.Little,
                Description = "Пожалуй, ни одной птице не посвящено столько стихов и произведений, как соловью. И ведь неспроста. Его пение поражает разнообразием репертуара, " +
                "силой звука и голоса. При этом совершенно удивительно после услышанного восхитительного исполнения увидеть этого невзрачного артиста. Соловей не привлекает " +
                "внешними данными, зато изумляет его певческое мастерство. К тому же, соловьиные песни возвещают о приходе весны и цветении природы вокруг, от чего на душе " +
                "становится теплей.",
                ImageSource = "/images/birds/nightingale.jpg"
            },
            new Bird
            {
                Id = 9,
                Name = BirdSpecies.Blackbird,
                NutritionType = $"{NutritionTypes.Insectivorous}",
                Size = BirdSize.Medium,
                Description = "Дрозды – недоверчивые, нервные и пугливые птицы, хотя часто можно встретить и спокойных особей с уравновешенным характером. Они также непоседливы, " +
                "умны и активны. В домашних условиях любители птиц содержат, как правило, певчего дрозда, хотя среди лучших певцов, обитающих в лесах России, можно назвать чёрного " +
                "и сизого дрозда, а также очень нарядных и голосистых птиц из группы каменных дроздов.",
                ImageSource = "/images/birds/blackbird.jpeg"
            },
            new Bird
            {
                Id = 10,
                Name = BirdSpecies.Starling,
                NutritionType = $"{NutritionTypes.Omnivorous}",
                Size = BirdSize.Medium,
                Description = "Скворец – это искусный звукоподражатель, он может имитировать самые разные звуки: мяуканье котов, кваканье жаб, крик ласточек, петухов, куриц, уток и " +
                "многое другое. Он прекрасно поёт, легко приручается и охотно общается с человеком. Если вы видите скворца возле вашего дома, то это не редкость, так как скворцы обитают " +
                "в городских парках, садах и около домов.",
                ImageSource = "/images/birds/starling.jpg"
            },
            new Bird
            {
                Id = 11,
                Name = BirdSpecies.Owl,
                NutritionType = $"{NutritionTypes.Carnivorous}",
                Size = BirdSize.Medium,
                Description = "Сова, даже домашняя, – хищная птица, которая имеет острый клюв и когти. Это милый пушистик под покровом ночи насквозь протыкает свою жертву " +
                "когтями и раздирает клювом. Совы по природе очень умные и хитрые, взмах их крыла совершенно бесшумен, что позволяет максимально приблизиться к жертве не " +
                "вызывая подозрений. Мощные сильные лапы способны разорвать добычу, которая не превышает размера самой птицы. Поэтому, если в вашем доме имеется хомяк, кот " +
                "или маленькая собака, не рекомендуется заводить хищную птицу, так как любимцы легко могут стать ее жертвой.",
                ImageSource = "/images/birds/owl.jpg"
            },
            new Bird
            {
                Id = 12,
                Name = BirdSpecies.Finch,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Little,
                Description = "Зяблики - одна из самых распространенных певчих птиц в средней полосе России, и неудивительно, что при этом они являются одними из " +
                "самых популярных в качестве домашних любимцев. Однако стоит отметить, что хоть зяблики и приживаются в неволе, по-настоящему ручными они все-таки " +
                "не становятся. И это требует от владельца определенного опыта в содержании птиц, что в итоге делает зябликов птицей не для новичков.",
                ImageSource = "/images/birds/finch.jpg"
            },
            new Bird
            {
                Id = 13,
                Name = BirdSpecies.Titmouse,
                NutritionType = $"{NutritionTypes.Insectivorous}",
                Size = BirdSize.Little,
                Description = "Синица в домашних условиях может жить долго и счастливо. Главное, подобрать для нее правильный рацион. Она не боится человека, легко приучается " +
                "к жизни в неволе, красиво поёт и неприхотлива в содержании. Они не боятся проживать по соседству с человеком и отлично себя чувствуют на приусадебных участках, " +
                "в садах, городских скверах и лесопарковых зонах.",
                ImageSource = "/images/birds/titmouse.jpg"
            },
            new Bird
            {
                Id = 14,
                Name = BirdSpecies.Skylark,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Little,
                Description = "Жаворонки пугливые, настороженные птицы, плохо поддающиеся приручению. Жаворонки, привыкшие к дому и владельцам, будут пугаться незнакомых людей. " +
                "Эти птицы подходят для опытных любителей певчих птиц. При содержании в домашних условиях необходимо использовать несколько клеток разного типа. Жаворонки " +
                "не только отличные певцы, но и превосходные «летуны». Такое свойство птицам данного семейства подарила сама природа - п0ри относительно небольшом туловище " +
                "обладают огромными крыльями, коротким хвостом.",
                ImageSource = "/images/birds/skylark.jpg"
            },
            new Bird
            {
                Id = 15,
                Name = BirdSpecies.Linnet,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Little,
                Description = "В домашних условиях большинство коноплянок остаются пугливыми птицами, часто бьются о прутья клетки при резких движениях человека или домашних животных. " +
                "Тем не менее их часто содержат в клетках из-за красивого пения. При вольерном содержании иногда дают потомство, получены гибриды с канарейками, щеглами и зеленушками. " +
                "Гибриды с канарейками отличаются неплохими голосовыми данными и даже во взрослом состоянии способны совершенствовать песню, перенимая её у поющих кенаров.",
                ImageSource = "/images/birds/linnet.jpg"
            },
            new Bird
            {
                Id = 16,
                Name = BirdSpecies.Jackdaw,
                NutritionType = $"{NutritionTypes.Omnivorous}",
                Size = BirdSize.Medium,
                Description = "Галка неприхотлива в неволе, как и другие врановые, мирится с вегетарианским столом. Кроме того, характер у нее относительно спокойный. " +
                "Галка — единственная из врановых птиц, голос которой не лишен мелодичности. Жаль, что галка несколько «глупее» вороны и даже сороки и грача. " +
                "Она плохо разбирает врагов и друзей, идет иногда на зов к незнакомому человеку. ",
                ImageSource = "/images/birds/jackdaw.jpeg"
            },
            new Bird
            {
                Id = 17,
                Name = BirdSpecies.Pheasant,
                NutritionType = $"{NutritionTypes.Omnivorous}",
                Size = BirdSize.Large,
                Description = "Фазаны в домашнем хозяйстве встречаются не часто. Однако в последние годы в России к этому направлению значительно активизировался интерес. " +
                "Птиц используют не только для получения диетического мяса и яйца. Многие состоятельные люди держат этих великолепных по красоте птиц в качестве декоративных. " +
                "Фазаны горделиво разгуливают по саду, словно маленькие павлины. От их нарядного оперения невозможно оторвать взгляд.",
                ImageSource = "/images/birds/pheasant.jpg"
            },
            new Bird
            {
                Id = 18,
                Name = BirdSpecies.Bullfinch,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Little,
                Description = "Снегири – это не только красивые птицы, но и удивительные создания, способные легко запоминать и воспроизводить мелодии и звуки. " +
                "Что касается характера, то они отличаются уравновешенностью. Эти птицы спокойны, они никуда не торопятся, аккуратны и осмотрительны. Снегири неплохо " +
                "живут в соседстве с людьми, неволю они переносят хорошо. Только из-за снижения количества полетов начинают полнеть. ",
                ImageSource = "/images/birds/bullfinch.jpg"
            },
            new Bird
            {
                Id = 19,
                Name = BirdSpecies.Sparrow,
                NutritionType = $"{NutritionTypes.Herbivorous}",
                Size = BirdSize.Little,
                Description = "Воробьи – общительные, дружелюбные птички. Живут в тесном соседстве с животными, другими пернатыми и человеком. Правда шумные и суетливые " +
                "по природе, не всегда уживаются со стрижами и синицами. Известно, что у воробьев развита память. Доказано, что они могут составлять логическую цепочку из " +
                "событий. Боятся кошек, но любят дразнить их, прыгая на коротких лапках у ее миски.",
                ImageSource = "/images/birds/sparrow.jpg"
            }
            );

        #endregion Птицы

        #region Ингредиенты

        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient
            {
                Id = 1,
                Name = IngredientName.Oats,
                Description = "Наиболее широко распространенный вид корма для большинства растительноядных птиц и всех видов грызунов. Как основной компонент входит " +
                "в состав большинства питательных смесей. Обладает хорошей всхожестью и может быть использован для подкормки животных зеленью, особенно в осенне-зимний период. " +
                "Для полноценного питания птиц в неволе необходим пророщенный овес.",
                ImageSource = "/images/ingredients/oats.jpg"
            },
            new Ingredient
            {
                Id = 2,
                Name = IngredientName.Millet,
                Description = "Просо - питательный злак, широко используемый для всех декоративных зерноядных птиц и грызунов, ценный корм для птицы, богатый каротином и " +
                "аминокислотами. Просо служит основой для составления различных питательных зерновых смесей, его доля в рационе птиц достигает 60-70%. Содержит много " +
                "клетчатки и белка, при этом не содержит глютена и является самым щелочным злаковым - легче переносится желудочно-кишечным трактом. Сухой корм для волнистых " +
                "попугайчиков, средних попугаев и канареек обязательно должен содержать просо.",
                ImageSource = "/images/ingredients/millet.jpg"
            },
            new Ingredient
            {
                Id = 3,
                Name = IngredientName.CanarySeed,
                Description = "Канареечное семя наиболее подходящий корм для канареек. Канареечник в диком виде растет на Канарских островах. Семена этого злакового растения " +
                "завезены в Европу вместе с канарейками. Сейчас их возделывают в некоторых странах на юге Европы, где широко используют в качестве корма для птиц. " +
                "Канареечное семя включает триптофан и помогает птицам справляться со стрессом. Его количество в составе кормов может доходить до 15%.",
                ImageSource = "/images/ingredients/canaryseed.jpg"
            },
            new Ingredient
            {
                Id = 4,
                Name = IngredientName.HempSeed,
                Description = "Конопляное семя — любимый корм всех зерноядных птиц. Зерна конопли содержат полиненасыщенные жирные кислоты. В небольшом количестве они " +
                "очень полезны для иммунитета, пищеварения, оперения. Но если давать их птице слишком много, то вскоре придется вводить ограничения в рацион питания из-за " +
                "развившегося ожирения. Более того, чрезмерное употребление конопляного семени может привести к опухоли век, слепоте и даже к гибели птицы.",
                ImageSource = "/images/ingredients/hempseed.jpg"
            },
            new Ingredient
            {
                Id = 5,
                Name = IngredientName.Rapeseed,
                Description = "Рапс может быть полезным и питательным источником пищи для птиц, если используется правильно. Белки рапса хорошо сбалансированы по аминокислотному " +
                "составу, по содержанию лизина приближаются к сое, а по метионину и цистину, кальцию и фосфору значительно превосходят ее. Однако, перед тем как использовать " +
                "рапс необходимо учитывать некоторые факторы. Во-первых, рапс может содержать глюкозинолаты, которые могут быть вредными для некоторых видов птиц, если они " +
                "употребляются в больших количествах. Во-вторых, рапс может быть токсичным, если он загнивает или становится жирным. Это может произойти, если рапс не хранится " +
                "должным образом или если он используется в слишком больших количествах. Поэтому необходимо следить за качеством и количеством рапса в рационе птиц.",
                ImageSource = "/images/ingredients/rapeseed.jpg"
            },
            new Ingredient
            {
                Id = 6,
                Name = IngredientName.Sesame,
                Description = "Кунжут — одно из лучших масличных растений, культивируемое от западных берегов Африки вплоть до Китая и Японии, а также в Америке. " +
                "Семена кунжута являются лидером по содержанию кальция. Кроме того, кунжутное семя богато витаминами В1 и Е, минералами и полиненасыщенными жирными " +
                "линолевыми кислотами. Кунжут незаменим для дополнительного поддержания сил организма в период стресса, линьки у птиц. Однако, не следует давать птицам " +
                "слишком много кунжута, так как это может привести к дисбалансу питательных веществ в рационе. Также следует учитывать, что кунжут может быть довольно жирным, " +
                "поэтому его нужно вводить постепенно в рацион птиц, начиная с небольшого количества.",
                ImageSource = "/images/ingredients/sesame.jpeg"
            },
            new Ingredient
            {
                Id = 7,
                Name = IngredientName.Sunflower,
                Description = "Подсолнечник — очень питательный вид корма. Однако подсолнечник не следует включать в больших количествах, так как избыток маслянистых " +
                "веществ может вызвать нарушения обмена веществ. Очень полезны для птиц белые семена подсолнечника, так как они содержат меньше жиров. Полосатые семена " +
                "богаты ненасыщенными жирными кислотами, которые благотворно влияют на оперение птиц.",
                ImageSource = "/images/ingredients/sunflower.jpg"
            },
            new Ingredient
            {
                Id = 8,
                Name = IngredientName.Wheat,
                Description = "Пшеница является богатым источником углеводов и белков, что делает ее хорошим источником " +
                "энергии и питательных веществ для птиц. Однако, необходимо учитывать, что пшеница может быть довольно бедной по содержанию " +
                "аминокислот, витаминов и минералов. Поэтому, если пшеница является основным источником пищи, то необходимо дополнительно добавлять " +
                "другие виды кормов и/или дополнительные пищевые добавки, чтобы обеспечить птицам все необходимые питательные вещества. Кроме того, необходимо учитывать, " +
                "что пшеница может быть довольно трудноусвояемой для молодых птиц и некоторых пород. Поэтому, если у вас есть молодые птицы или " +
                "птицы с проблемами пищеварения, то необходимо убедиться, что они могут правильно усваивать пшеницу. " +
                "В целом, пшеница может быть полезным источником пищи для птиц, если она используется в сочетании с другими видами кормов и дополнительными пищевыми добавками.",
                ImageSource = "/images/ingredients/wheat.jpg"
            },
            new Ingredient
            {
                Id = 9,
                Name = IngredientName.Barley,
                Description = "Ячмень является одной из наиболее распространенных зерновых культур, используемых в качестве корма для птиц. В зависимости от вида птицы и ее возраста, " +
                "ячмень может использоваться как основной источник питательных веществ или в комбинации с другими зерновыми культурами в кормовых смесях. Он содержит богатый набор " +
                "белков, углеводов, клетчатки, минералов, витаминов и аминокислот, таких как лизин и треонин, которые важны для роста и развития птиц. Однако ячмень содержит " +
                "фитиновую кислоту, которая может затруднять усвоение некоторых питательных веществ, таких как кальций, железо и цинк. Чтобы уменьшить воздействие фитиновой кислоты, " +
                "ячмень может быть обработан термически или использован в сочетании с другими кормовыми ингредиентами, такими как мясо-костная мука или фосфаты.",
                ImageSource = "/images/ingredients/barley.jpg"
            },
            new Ingredient
            {
                Id = 10,
                Name = IngredientName.Rye,
                Description = "Рожь – корм, используемый, когда недостает другого злакового зерна. Если переборщить с рожью, у пернатых начинаются проблемы с пищеварением. " +
                "Это объясняется сильным набуханием крахмала ржи. Нельзя кормить пернатых недавно убранным зерном – провоцирует недуги ЖКТ. Зерно выдерживается в течение трех месяцев. " +
                "Как и с другими зерновыми культурами, рожь может быть обработана термически или использована в сочетании с другими кормовыми ингредиентами, чтобы снизить уровень " +
                "антипитательных веществ.",
                ImageSource = "/images/ingredients/rye.jpg"
            },
            new Ingredient
            {
                Id = 11,
                Name = IngredientName.Corn,
                Description = "Кукуруза - один из самых лучших и широко используемых видов кормов для декоративных зерноядных птиц. По энергетической ценности кукуруза " +
                "превосходит многие другие зерновые корма, содержит много белка (9-12%), углеводов (65-70%) и мало - клетчатки. Богата железом и каротином (провитамином А). " +
                "Особенно много провитамина А в желтозерных сортах. Кукуруза является одним из основных компонентов для составления различных зерновых смесей. Многие птицы " +
                "неохотно едят сухую кукурузу, выклевывая лишь сердцевину. В этом случае рекомендуется зерно замачивать или запаривать.",
                ImageSource = "/images/ingredients/corn.jpg"
            },
            new Ingredient
            {
                Id = 12,
                Name = IngredientName.Sorghum,
                Description = "Сорго - это питательная пища, не проигрывающая по калорийности кукурузе и ячменю. В зерне почти в 10 раз выше содержание кальция, натрия и магния " +
                "в сравнении с другими культурами, без вреда может занимать до 50% рациона несушек. Это способствует насыщению организма птицы минералами и формированию яичной " +
                "скорлупы. Злаковые корма — основа калорийного питания уток и голубей, сорго охотно поедается ими и хорошо переваривается. Питательная ценность выше, чем у овса. " +
                "Сорго - разновидность проса, поэтому попугаи с удовольствием его едят.",
                ImageSource = "/images/ingredients/sorghum.jpg"
            },
            new Ingredient
            {
                Id = 13,
                Name = IngredientName.Flaxseed,
                Description = "Льняное семечко может использоваться в качестве дополнительного источника питательных веществ в корму для птиц. В семечках содержится большое " +
                "количество лигнинов, которые отвечают за здоровье гормональной системы. Также семя содержит несколько важных для работы иммунитета и общего состояния организма " +
                "жирных кислот и белок, который обладает особым строением и очень быстро усваивается организмом. Альфа-линолиевая кислота предупреждает развитие опухолей, " +
                "стимулирует рост оперения и укрепляет иммунную систему.",
                ImageSource = "/images/ingredients/flaxseed.jpg"
            },
            new Ingredient
            {
                Id = 14,
                Name = IngredientName.Soy,
                Description = "Соевые корма высоко ценятся из-за своего состава, большую часть которого занимает протеин, остальную – жиры, аминокислоты, витамины группы В, " +
                "минералы, фосфор. Питательный продукт улучшает здоровье птиц, увеличивает продуктивность, несушки откладывают крупные яйца с крепкой скорлупой, мясные породы " +
                "быстрее набирают вес. Наличие аминокислот ускоряет метаболизм в тканях. Но в свежих соевых продуктах имеются антипитательные (токсичные) вещества, поэтому не " +
                "рекомендуется давать птице цельное зерно в большом объеме и зеленую часть бобовых. Накапливаясь в организме, токсины замедляют рост, отрицательно влияют на " +
                "продуктивность и даже провоцируют летальный исход.",
                ImageSource = "/images/ingredients/soy.jpeg"
            },
            new Ingredient
            {
                Id = 15,
                Name = IngredientName.Buckwheat,
                Description = "Гречка, гречиха, или греча – достаточно широко распространенный на территории России продукт. Она богата белками, микроэлементами и аминокислотами, поэтому " +
                "гречневая крупа и ее производные активно применяются во всех сферах пищевой промышленности: начиная от производства детского питания и заканчивая изготовлением " +
                "кормов для животных. Для корма скота и домашней птицы чаще всего приобретают отруби и мучную пыль, которые отсеиваются в процессе обработки зерен.",
                ImageSource = "/images/ingredients/buckwheat.jpg"
            },
            new Ingredient
            {
                Id = 16,
                Name = IngredientName.Nuts,
                Description = "Орехи особенно популярны в качестве зимнего корма для птиц, потому что они долговечны и их легко спрятать для последующего использования. " +
                "Многие птицы, которые питаются орехами, проводят недели осенью, запасая орехи в полостях коры, небольших углублениях в земле или в других укрытиях. " +
                "Когда других источников пищи не хватает или плохая погода не позволяет правильно добывать пищу, птицы возвращаются в эти укрытия, чтобы съесть припасенные орехи. " +
                "Но следует обращать внимание на их использование, так как не все виды орехов одинаково полезны для птиц, а некоторые могут быть вредными. Самыми распространенными " +
                "орехами, которые могут использоваться в корму для птиц, являются грецкий орех, арахис, миндаль, кешью, фисташки.",
                ImageSource = "/images/ingredients/nuts.jpg"
            },
            new Ingredient
            {
                Id = 17,
                Name = IngredientName.Vegetables,
                Description = "Овощи могут быть важным источником питательных веществ в рационе птиц. Наиболее распространены: морковь (богата каротином, который может помочь " +
                "поддерживать здоровье глаз и кожи у птиц), капуста (содержит много витамина С, кальция, витаминов и минералов), огурцы (богаты водой и могут помочь увлажнить корм птиц), " +
                "брокколи (содержит много витаминов и минералов, включая витамины А, С и К, а также кальций и железо), тыква (содержит витамин А, калий и другие минералы). Кроме того, " +
                "овощи, такие как листовые зеленые, помидоры, перец, редис, свекла и многие другие, могут также использоваться в корму для птиц. Важно помнить, что некоторые овощи могут " +
                "быть токсичными для птиц, поэтому необходимо обращаться к опытным специалистам, чтобы определить, какие овощи лучше всего использовать в рационе для конкретного вида.",
                ImageSource = "/images/ingredients/vegetables.jpg"
            },
            new Ingredient
            {
                Id = 18,
                Name = IngredientName.Fruits,
                Description = "Фрукты могут быть хорошим источником питательных веществ для птиц, но должны использоваться осторожно и только в умеренных количествах. " +
                "Наиболее популярны: яблоки (источник витамина C, клетчатки, калия и фолиевой кислоты), бананы (богаты калием, витамином С, витамином B6, клетчаткой), " +
                "груши (источником витамина C, калия, клетчатки), ягоды (черника, малина и клубника богаты антиоксидантами и витаминами C и К), киви (богаты витамином C, " +
                "клетчаткой и калием), апельсины (содержат много витамина C и клетчатки, фолиевую кислоту и калий). Однако, некоторые фрукты могут быть опасными для птиц, " +
                "поэтому необходимо быть осторожным. Например, абрикосы, персики, сливы и черешня могут содержать косточки, которые могут быть опасны для птиц. Кроме того, " +
                "некоторые фрукты могут быть кислыми и вызывать проблемы с желудком у птиц.",
                ImageSource = "/images/ingredients/fruits.jpg"
            },
            new Ingredient
            {
                Id = 19,
                Name = IngredientName.Insects,
                Description = "Насекомые содержат много важных питательных веществ, таких как белок, жир, витамины и минералы. Чаще всего в кормах используются: сверчки, " +
                "мухи, мотыльки, черви, тараканы. При выборе насекомых следует обратить внимание на их размер: более крупные птицы могут легко справляться с крупными сверчками, " +
                "тогда как маленьким птицам лучше подойдут мелкие насекомые, например, мухи.",
                ImageSource = "/images/ingredients/insects.jpg"
            });

        #endregion Ингредиенты

        #region Товары

        var carriers = CreateCarriers();
        modelBuilder.Entity<Product>().HasData(carriers);

        var food = CreateFood();
        modelBuilder.Entity<Product>().HasData(food);

        var feeders = CreateFeeders();
        modelBuilder.Entity<Product>().HasData(feeders);

        var goodies = CreateGoodies();
        modelBuilder.Entity<Product>().HasData(goodies);

        var care = CreateCareProducts();
        modelBuilder.Entity<Product>().HasData(care);

        var cells = CreateCells();
        modelBuilder.Entity<Product>().HasData(cells);

        var toys = CreateToys();
        modelBuilder.Entity<Product>().HasData(toys);

        var fillers = CreateFillers();
        modelBuilder.Entity<Product>().HasData(fillers);

        var accessories = CreateAccessories();
        modelBuilder.Entity<Product>().HasData(accessories);

        //modelBuilder.Entity<Product>().HasData(carriers, food, feeders, goodies, care, cells, toys, fillers, accessories);

        #endregion Товары

        #region Товары и Цены

        var prices = CreatePrices();
        modelBuilder.Entity<ProductPrice>().HasData(prices);

        #endregion Товары и Цены

        #region Продукты-Ингредиенты

        var pi = CreateProductIngredients();
        modelBuilder.Entity<ProductIngredient>().HasData(pi);

        #endregion Продукты-Ингредиенты

        #region Скидки

        var discounts = CreateDiscounts(prices.Count);
        modelBuilder.Entity<Discount>().HasData(discounts);

        #endregion Скидки

        #region Отзывы

        var reviews = CreateReviews();
        modelBuilder.Entity<Review>().HasData(reviews);

        #endregion Отзывы

        #endregion Заполнение таблиц
    }

    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(d => d.ToDateTime(TimeOnly.MinValue), d => DateOnly.FromDateTime(d))
        {
        }
    }

    #region Генерация Переносок для птиц (id = {1,20})

    private List<Product> CreateCarriers()
    {
        var carriers = new List<Product>();
        Random rnd = new Random();

        for (int i = 1; i <= 20; i++)
        {
            int size = rnd.Next(1, 4);
            int brand = rnd.Next(5, 8);
            string birdSize = (size == 1) ? BirdSize.Little : (size == 2) ? BirdSize.Medium : BirdSize.Large;

            carriers.Add(
                new()
                {
                    Id = i,
                    CategoryId = 1,
                    Name = $"Переноска для птиц {i}",
                    ImageSource =
                    (size == 1) ? "/images/catalog/carriers/malinki.jpg" :
                    (size == 2) ? "/images/catalog/carriers/travellor.jpeg" :
                    "/images/catalog/carriers/vitakraft.jpg",
                    Manufacturer = (brand == 5) ? "Malinki" : (brand == 6) ? "Travellor" : "Vitakraft",
                    BirdSize = birdSize,
                    Description = $"Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: {birdSize}"
                }
                );
        }
        return carriers;
    }

    #endregion Генерация Переносок для птиц (id = {1,20})

    #region Генерация Корма для птиц (id = {21,40})

    private List<Product> CreateFood()
    {
        var foods = new List<Product>();
        Random rnd = new Random();

        for (int i = 21; i <= 40; i++)
        {
            int random = rnd.Next(1, 5);
            string birdSize = (random == 1) ? BirdSize.Large : (random == 2) ? BirdSize.Medium : BirdSize.Little;
            string size = (random == 1) ? "крупных" : (random == 2) ? "средних" : "маленьких";
            string src =
                (random == 1) ? "/images/catalog/food/rio.jpg" :
                (random == 2) ? "/images/catalog/food/padovan.jpg" :
                (random == 3) ? "/images/catalog/food/fiory.jpg" : "/images/catalog/food/prestige.jpeg";

            foods.Add(
                new()
                {
                    Id = i,
                    CategoryId = 2,
                    Manufacturer = (random == 1) ? "Rio" : (random == 2) ? "Padovan" : (random == 3) ? "Fiory" : "Prestige",
                    Name = $"Корм для {size} птиц №{i - 20}",
                    ImageSource = $"{src}",
                    BirdSize = birdSize,
                    Description = $"Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: {birdSize}"
                }
                );
        }
        foods[0].NutritionType = NutritionTypes.Carnivorous; //21
        foods[1].NutritionType = NutritionTypes.Omnivorous; //22
        foods[2].NutritionType = NutritionTypes.Herbivorous; //23
        foods[3].NutritionType = NutritionTypes.Insectivorous; //24

        foods[4].NutritionType = NutritionTypes.Carnivorous; //25
        foods[5].NutritionType = NutritionTypes.Omnivorous; //26
        foods[6].NutritionType = NutritionTypes.Herbivorous; //27
        foods[7].NutritionType = NutritionTypes.Insectivorous; //28

        foods[8].NutritionType = NutritionTypes.Carnivorous; //29
        foods[9].NutritionType = NutritionTypes.Omnivorous; //30
        foods[10].NutritionType = NutritionTypes.Herbivorous; //31
        foods[11].NutritionType = NutritionTypes.Insectivorous; //32

        foods[12].NutritionType = NutritionTypes.Carnivorous; //33
        foods[13].NutritionType = NutritionTypes.Omnivorous; //34
        foods[14].NutritionType = NutritionTypes.Herbivorous; //35
        foods[15].NutritionType = NutritionTypes.Insectivorous; //36

        foods[16].NutritionType = NutritionTypes.Carnivorous; //37
        foods[17].NutritionType = NutritionTypes.Omnivorous; //38
        foods[18].NutritionType = NutritionTypes.Herbivorous; //39
        foods[19].NutritionType = NutritionTypes.Insectivorous; //40

        return foods;
    }

    #endregion Генерация Корма для птиц (id = {21,40})

    #region Генерация Кормушек для птиц (id = {41,60})

    private List<Product> CreateFeeders()
    {
        var feeders = new List<Product>();
        Random rnd = new Random();

        for (int i = 41; i <= 60; i++)
        {
            int random = rnd.Next(1, 4);
            string src =
                (random == 1) ? "/images/catalog/feeders/darell.jpg" :
                (random == 2) ? "/images/catalog/feeders/doradowood.jpg" : "/images/catalog/feeders/trixie.jpg";
            string manufacturer = (random == 1) ? "Darell" : (random == 2) ? "DoradoWood" : "Trixie";

            feeders.Add(
                new()
                {
                    Id = i,
                    CategoryId = 3,
                    Manufacturer = manufacturer,
                    Name = $"Кормушка {manufacturer} для птиц уличная  №{i - 40}",
                    ImageSource = $"{src}",
                    Description = "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. " +
                    "Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны " +
                    "к естественным, поэтому птицы станут частыми гостями \"застолья\"!"
                }
                );
        }
        return feeders;
    }

    #endregion Генерация Кормушек для птиц (id = {41,60})

    #region Генерация Лакомств для птиц (id = {61,80})

    private List<Product> CreateGoodies()
    {
        var goodies = new List<Product>();
        Random rnd = new Random();

        for (int i = 61; i <= 80; i++)
        {
            int random = rnd.Next(1, 4);
            string src =
                (random == 1) ? "/images/catalog/goodies/triol.jpg" :
                (random == 2) ? "/images/catalog/goodies/rio.jpg" : "/images/catalog/goodies/vitakraft.jpeg";
            string manufacturer = (random == 1) ? "Triol" : (random == 2) ? "Rio" : "Vitakraft";
            string birdSize = (random == 1) ? BirdSize.Large : (random == 2) ? BirdSize.Medium : BirdSize.Little;

            goodies.Add(
                new()
                {
                    Id = i,
                    CategoryId = 4,
                    Manufacturer = manufacturer,
                    Name = $"Лакомство {manufacturer} для птиц  №{i - 60}",
                    ImageSource = $"{src}",
                    Description = "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, " +
                    $"необходимыми для правильного развития пернатых питомцев. Размер птиц: {birdSize}",
                    BirdSize = birdSize
                }
                );
        }
        return goodies;
    }

    #endregion Генерация Лакомств для птиц (id = {61,80})

    #region Генерация Средств ухода за птицами (id = {81,100})

    private List<Product> CreateCareProducts()
    {
        var careProducts = new List<Product>();
        Random rnd = new Random();

        for (int i = 81; i <= 100; i++)
        {
            int random = rnd.Next(1, 4);
            string src =
                (random == 1) ? "/images/catalog/careproducts/beaphar.jpg" :
                (random == 2) ? "/images/catalog/careproducts/baka.jpg" : "/images/catalog/careproducts/doctoranimal.jpg";
            string manufacturer = (random == 1) ? "Beaphar" : (random == 2) ? "BAKA" : "Doctor Animal";
            string description =
                (random == 1) ? "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, " +
                "в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения." :
                (random == 2) ? "Капли против паразитов, для птиц, 50мл." :
                "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. " +
                "Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную " +
                "линьку и поддерживает птицу в нормальном физиологическом состоянии.";
            string name =
                (random == 1) ? $"Спрей для птиц {manufacturer} Papick Spray против выдёргивания перьев №{i - 80}" :
                (random == 2) ? $"Капли против паразитов {manufacturer} №{i - 80}" : $"Спрей \"{manufacturer}\" для ухода за оперением птиц №{i - 80}";

            careProducts.Add(
                new()
                {
                    Id = i,
                    CategoryId = 5,
                    Manufacturer = manufacturer,
                    Name = name,
                    ImageSource = $"{src}",
                    Description = $"{description}"
                }
                );
        }
        return careProducts;
    }

    #endregion Генерация Средств ухода за птицами (id = {81,100})

    #region Генерация Клеток для птиц (id = {101,120})

    private List<Product> CreateCells()
    {
        var cells = new List<Product>();
        Random rnd = new Random();

        for (int i = 101; i <= 120; i++)
        {
            int random = rnd.Next(1, 4);
            string src =
                (random == 1) ? "/images/catalog/cells/ferplast.jpg" :
                (random == 2) ? "/images/catalog/cells/skyrainforest.jpg" : "/images/catalog/cells/vision.jpg";
            string manufacturer = (random == 1) ? "Ferplast" : (random == 2) ? "Sky Rainforest" : "Vision";
            string birdSize = (random == 1) ? BirdSize.Large : (random == 2) ? BirdSize.Medium : BirdSize.Little;

            cells.Add(
                new()
                {
                    Id = i,
                    CategoryId = 6,
                    Manufacturer = manufacturer,
                    Name = $"Клетка для птиц {manufacturer} №{i - 100}",
                    ImageSource = $"{src}",
                    Description = $"Клетка для птиц {manufacturer} станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. " +
                    $"Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. " +
                    $"Размер птиц: {birdSize}{(birdSize.Equals(BirdSize.Little) ? null : " и менее")}.",
                    BirdSize = birdSize
                }
                );
        }
        return cells;
    }

    #endregion Генерация Клеток для птиц (id = {101,120})

    #region Генерация Игрушек для птиц (id = {121,140})

    private List<Product> CreateToys()
    {
        var toys = new List<Product>();
        Random rnd = new Random();

        for (int i = 121; i <= 140; i++)
        {
            int random = rnd.Next(1, 4);
            string src =
                (random == 1) ? "/images/catalog/toys/doradowood.jpg" :
                (random == 2) ? "/images/catalog/toys/ferplast.jpg" : "/images/catalog/toys/triol.jpg";
            string manufacturer = (random == 1) ? "DoradoWood" : (random == 2) ? "Ferplast" : "Triol";
            string birdSize = (random == 1) ? BirdSize.Large : (random == 2) ? BirdSize.Medium : BirdSize.Little;

            toys.Add(
                new()
                {
                    Id = i,
                    CategoryId = 7,
                    Manufacturer = manufacturer,
                    Name = $"Игрушка для птиц {manufacturer} №{i - 120}",
                    ImageSource = $"{src}",
                    Description = $"Забавная игрушка для птиц. Размер птиц: {birdSize}.",
                    BirdSize = birdSize
                }
                );
        }
        return toys;
    }

    #endregion Генерация Игрушек для птиц (id = {121,140})

    #region Генерация Наполнителей для птиц (id = {141,160})

    private List<Product> CreateFillers()
    {
        var fillers = new List<Product>();
        Random rnd = new Random();

        for (int i = 141; i <= 160; i++)
        {
            int random = rnd.Next(1, 4);
            string src =
                (random == 1) ? "/images/catalog/fillers/fiory.jpg" :
                (random == 2) ? "/images/catalog/fillers/padovan.jpg" : "/images/catalog/fillers/vitaline.jpg";
            string manufacturer = (random == 1) ? "Fiory" : (random == 2) ? "Padovan" : "Vitaline";
            string name =
                (random == 1) ? "Песок для птиц Grit Mint Fiory" :
                (random == 2) ? $"Наполнитель для дна птичьих клеток PADOVAN Natural Sand" :
                "Опилки гипоаллергенные из лиственных пород древесины Vitaline";
            string description =
                (random == 1) ? "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не " +
                "содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью." :
                (random == 2) ? "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей " +
                "клетки для обеспечения лучшей гигиены и облегчения чистки дна." :
                "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.";

            fillers.Add(
                new()
                {
                    Id = i,
                    CategoryId = 8,
                    Manufacturer = manufacturer,
                    Name = $"{name} №{i - 140}",
                    ImageSource = $"{src}",
                    Description = $"{description}"
                }
                );
        }
        return fillers;
    }

    #endregion Генерация Наполнителей для птиц (id = {141,160})

    #region Генерация Игрушек для птиц (id = {161,180})

    private List<Product> CreateAccessories()
    {
        var accessories = new List<Product>();
        Random rnd = new Random();

        for (int i = 161; i <= 180; i++)
        {
            int random = rnd.Next(1, 4);
            string src =
                (random == 1) ? "/images/catalog/accessories/baka.jpg" :
                (random == 2) ? "/images/catalog/accessories/crystal.jpg" : "/images/catalog/accessories/trixie.jpg";
            string manufacturer = (random == 1) ? "BAKA" : (random == 2) ? "Crystal" : "Trixie";
            string name =
                (random == 1) ? "Поилка для птиц ВАКА" :
                (random == 2) ? "Качели Crystal" :
                "Жердочки для клетки Trixie";
            string description =
                (random == 1) ? "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. " +
                "Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм." :
                (random == 2) ? "Качели для Вашего питомца. Изготовлены из древесины." :
                "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.";

            accessories.Add(
                new()
                {
                    Id = i,
                    CategoryId = 9,
                    Manufacturer = manufacturer,
                    Name = $"{name} №{i - 160}",
                    ImageSource = $"{src}",
                    Description = $"{description}"
                }
                );
        }
        return accessories;
    }

    #endregion Генерация Игрушек для птиц (id = {161,180})

    #region Генерация для Продукты-Ингредиенты

    private List<ProductIngredient> CreateProductIngredients()
    {
        var pi = new List<ProductIngredient>();

        //Плотоядные, насекомоядные, всеядные
        pi.Add(new ProductIngredient { ProductId = 21, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 24, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 25, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 28, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 29, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 32, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 33, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 36, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 37, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 40, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 22, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 26, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 30, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 34, IngredientId = 19 });
        pi.Add(new ProductIngredient { ProductId = 38, IngredientId = 19 });

        //Растительноядные
        pi.Add(new ProductIngredient { ProductId = 23, IngredientId = 1 });
        pi.Add(new ProductIngredient { ProductId = 23, IngredientId = 2 });
        pi.Add(new ProductIngredient { ProductId = 23, IngredientId = 3 });
        pi.Add(new ProductIngredient { ProductId = 23, IngredientId = 4 });

        pi.Add(new ProductIngredient { ProductId = 27, IngredientId = 5 });
        pi.Add(new ProductIngredient { ProductId = 27, IngredientId = 6 });
        pi.Add(new ProductIngredient { ProductId = 27, IngredientId = 7 });
        pi.Add(new ProductIngredient { ProductId = 27, IngredientId = 8 });

        pi.Add(new ProductIngredient { ProductId = 31, IngredientId = 9 });
        pi.Add(new ProductIngredient { ProductId = 31, IngredientId = 10 });
        pi.Add(new ProductIngredient { ProductId = 31, IngredientId = 11 });
        pi.Add(new ProductIngredient { ProductId = 31, IngredientId = 12 });

        pi.Add(new ProductIngredient { ProductId = 35, IngredientId = 13 });
        pi.Add(new ProductIngredient { ProductId = 35, IngredientId = 14 });
        pi.Add(new ProductIngredient { ProductId = 35, IngredientId = 15 });
        pi.Add(new ProductIngredient { ProductId = 35, IngredientId = 16 });

        pi.Add(new ProductIngredient { ProductId = 39, IngredientId = 17 });
        pi.Add(new ProductIngredient { ProductId = 39, IngredientId = 18 });
        pi.Add(new ProductIngredient { ProductId = 39, IngredientId = 1 });
        pi.Add(new ProductIngredient { ProductId = 39, IngredientId = 2 });

        //Всеядные
        pi.Add(new ProductIngredient { ProductId = 22, IngredientId = 3 });
        pi.Add(new ProductIngredient { ProductId = 22, IngredientId = 4 });
        pi.Add(new ProductIngredient { ProductId = 22, IngredientId = 5 });
        pi.Add(new ProductIngredient { ProductId = 22, IngredientId = 6 });

        pi.Add(new ProductIngredient { ProductId = 26, IngredientId = 7 });
        pi.Add(new ProductIngredient { ProductId = 26, IngredientId = 8 });
        pi.Add(new ProductIngredient { ProductId = 26, IngredientId = 9 });
        pi.Add(new ProductIngredient { ProductId = 26, IngredientId = 10 });

        pi.Add(new ProductIngredient { ProductId = 30, IngredientId = 11 });
        pi.Add(new ProductIngredient { ProductId = 30, IngredientId = 12 });
        pi.Add(new ProductIngredient { ProductId = 30, IngredientId = 13 });
        pi.Add(new ProductIngredient { ProductId = 30, IngredientId = 14 });

        pi.Add(new ProductIngredient { ProductId = 34, IngredientId = 15 });
        pi.Add(new ProductIngredient { ProductId = 34, IngredientId = 16 });
        pi.Add(new ProductIngredient { ProductId = 34, IngredientId = 17 });
        pi.Add(new ProductIngredient { ProductId = 34, IngredientId = 18 });

        pi.Add(new ProductIngredient { ProductId = 38, IngredientId = 1 });
        pi.Add(new ProductIngredient { ProductId = 38, IngredientId = 2 });
        pi.Add(new ProductIngredient { ProductId = 38, IngredientId = 3 });
        pi.Add(new ProductIngredient { ProductId = 38, IngredientId = 4 });

        for (int i = 0; i < pi.Count; i++)
        {
            pi[i].Id = (i + 1);
        }

        return pi;
    }

    #endregion Генерация для Продукты-Ингредиенты

    #region Генерация Товары и Цены

    private List<ProductPrice> CreatePrices()
    {
        List<ProductPrice> result = new();
        Random rnd = new Random();
        int id = 1;
        ProductPrice? productPrice;

        for (int i = 1; i <= 180; i++)
        {
            if (i >= 21 && i <= 40)
            {
                productPrice = new ProductPrice() { ProductId = i };

                productPrice.Id = id;
                id++;
                productPrice.Weight = 200;
                productPrice.Price = Convert.ToDecimal(rnd.Next(200, 400));
                productPrice.InStock = rnd.Next(0, 100);
                result.Add(productPrice);

                productPrice = new ProductPrice() { ProductId = i };

                productPrice.Id = id;
                id++;
                productPrice.Weight = 400;
                productPrice.Price = Convert.ToDecimal(rnd.Next(400, 600));
                productPrice.InStock = rnd.Next(0, 100);
                result.Add(productPrice);

                productPrice = new ProductPrice() { ProductId = i };

                productPrice.Id = id;
                id++;
                productPrice.Weight = 800;
                productPrice.Price = Convert.ToDecimal(rnd.Next(800, 1000));
                productPrice.InStock = rnd.Next(0, 100);
                result.Add(productPrice);

                productPrice = new ProductPrice() { ProductId = i };

                productPrice.Id = id;
                id++;
                productPrice.Weight = 1000;
                productPrice.Price = Convert.ToDecimal(rnd.Next(1000, 1500));
                productPrice.InStock = rnd.Next(0, 100);
                result.Add(productPrice);
            }
            else
            {
                productPrice = new ProductPrice() { ProductId = i };

                productPrice.Id = id;
                id++;
                productPrice.Price = Convert.ToDecimal(rnd.Next(200, 2000));
                productPrice.InStock = rnd.Next(0, 100);
                result.Add(productPrice);
            }
        }
        return result;
    }

    #endregion Генерация Товары и Цены

    #region Генерация Скидки

    private List<Discount> CreateDiscounts(int count)
    {
        var discounts = new List<Discount>();
        Random rnd = new Random();
        DateTime currentDate = DateTime.Now;
        int id = 1;
        int randomValue;

        for (int i = 1; i <= count; i++)
        {
            randomValue = rnd.Next(1, 10);

            if (randomValue == 7)
            {
                discounts.Add(
                    new Discount
                    {
                        Id = id,
                        ProductPriceId = i,
                        Value = rnd.Next(10, 25),
                        StartDate = currentDate,
                        EndDate = currentDate.AddDays(rnd.Next(7, 30))
                    }
                    );
                id++;
            }
        }
        return discounts;
    }

    #endregion Генерация Скидки

    #region Генерация Отзывов

    private List<Review> CreateReviews()
    {
        List<Review> reviews = new();
        Random rnd = new Random();
        int randomComment;
        string comment1 = "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.";
        string comment2 = "Отличное качество, синицы оценили!";
        string comment3 = "Производителю рекомендовал бы применять более эластичный материал.";
        string comment4 = "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!";
        Dictionary<int,int> ProductUser = new Dictionary<int,int>();

        for (int i = 1; i <= 50; i++)
        {
            Review review = new Review();
            randomComment = rnd.Next(1, 5);

            review.Id = i;
            review.ProductId = rnd.Next(1, 181);
            review.Comment = randomComment == 1 ? comment1 : randomComment == 2 ? comment2 : randomComment == 3 ? comment3 : comment4;
            review.ReviewDate = new DateTime(2017, 1, 1).AddDays(rnd.Next(1, 1826));
            review.UserId = rnd.Next(2, 8);

            while(ProductUser.ContainsKey(review.ProductId) && ProductUser[review.ProductId] == review.UserId)
            {
                review.UserId = rnd.Next(2, 8);
            }
            ProductUser[review.ProductId] = review.UserId;

            reviews.Add(review);
        }

        return reviews;
    }

    #endregion Генерация Отзывов
}