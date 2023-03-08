using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Birdy.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Birds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar", nullable: true),
                    NutritionType = table.Column<string>(type: "varchar", maxLength: 25, nullable: true),
                    Size = table.Column<string>(type: "varchar", maxLength: 25, nullable: true),
                    ImageSource = table.Column<string>(type: "varchar", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 60, nullable: false),
                    Url = table.Column<string>(type: "varchar", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "varchar", nullable: true),
                    ImageSource = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<string>(type: "varchar", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar", nullable: true),
                    Manufacturer = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageSource = table.Column<string>(type: "text", nullable: true),
                    NutritionType = table.Column<string>(type: "text", nullable: true),
                    BirdSize = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "varchar", maxLength: 70, nullable: false),
                    Password = table.Column<string>(type: "varchar", maxLength: 60, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "date", nullable: false),
                    Comment = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "varchar(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "varchar", maxLength: 60, nullable: true),
                    UserAddress = table.Column<string>(type: "varchar", maxLength: 150, nullable: true),
                    UserTelephone = table.Column<string>(type: "varchar", maxLength: 25, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BirdProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BirdId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BirdProducts_Birds_BirdId",
                        column: x => x.BirdId,
                        principalTable: "Birds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BirdProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductIngredients",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIngredients", x => new { x.ProductId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    InStock = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Raitings",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raitings", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Raitings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Raitings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "varchar", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductPriceId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_ProductPrices_ProductPriceId",
                        column: x => x.ProductPriceId,
                        principalTable: "ProductPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Birds",
                columns: new[] { "Id", "Description", "ImageSource", "Name", "NutritionType", "Size" },
                values: new object[,]
                {
                    { 1, "Среди множества видов декоративных птиц маленькие попугаи – самые востребованные. Такая популярность объясняется не только простотой содержания, но и количеством позитива, исходящего от этих пернатых. Миниатюрные птички забавны, миловидны и умны. Они прекрасные собеседники: всегда выслушают хозяина и даже «поддержат» разговор. Если вы хотите завести экзотическую птичку, обратите внимание на представителей маленьких видов: дятловых, воробьиных, фиговых, волнистых попугаев или неразлучников. За ними легко ухаживать. Малые размеры птиц – не единственное их положительное качество. Они легко приспосабливаются к жизни рядом с человеком.", "/images/birds/littleparrot.jpg", "Маленький попугай", "Растительноядные", "Маленький" },
                    { 2, "Многие люди в качестве домашних питомцев выбирают средних попугаев. Эти птички отличаются относительно небольшими размерами и красотой, благодаря чему их можно успешно содержать в городской малогабаритной квартире. Попугаи средних размеров насчитывают множество родов и видов. Самыми популярными экземплярами, живущими рядом с человеком, являются корелла, лори, розелла, кольчатый и сенегальский попугаи.", "/images/birds/mediumparrot.jpg", "Средний попугай", "Растительноядные", "Средний" },
                    { 3, "На сегодняшний день на планете живет около 350 видов попугаев. Из них крупные породы представлены в незначительном количестве. Некоторые из них находятся под угрозой вымирания и занесены в Красную книгу. Цены на таких питомцев высоки, но и спрос на них не угасает. Среди крупных попугаев наиболее известны: ара, амазонский попугай, какаду, какаду маффин.", "/images/birds/largeparrot.jpg", "Крупный попугай", "Растительноядные", "Крупный" },
                    { 4, "Канарейка птица маленькая. Благодаря своему умению красиво разливать трели, даже несмотря на скромное оперение, она завоевала популярность и любовь многих людей. Певунья не приносит неудобств, а только удовольствие от одного своего неприхотливого вида, незамысловатого щебетания, которое сменяется удивительным по звучанию пением. Клетка не занимает много места в квартире. За ней легко ухаживать и не нужно тратить время на прогулки с питомцем.", "/images/birds/canary.jpg", "Канарейка", "Растительноядные", "Маленький" },
                    { 5, " Амадины - это общительные птицы, которые любят взаимодействовать с людьми. Для содержания не требуют большие финансовые вложения и физические усилия. Благодаря такой неприхотливости в сочетании с миролюбивым характером, яркие певчие птички завоевали широкую популярность. «Пение» амадин специфичное и порой бывает неприятным: они издают тихое щебетание, свист, жужжание, бурчание, шипение. У некоторых амадин приятный голос, но поют только самцы. Привлекательные, подвижные, красивые и очень доверчивые, они моментально завоевывают сердца любителей птиц.", "/images/birds/amadina.jpg", "Амадина", "Растительноядные", "Маленький" },
                    { 6, "Голубь – птица мира, которая на протяжении сотни лет служит человеку и выступает в качестве друга. Еще в давние времена, когда не было почты, в качестве почтальонов выступали именно эти замечательные птицы. Они доставляли письма адресату и возвращались домой к хозяину. Голубей любят и ценят в нынешние дни, некоторые люди мечтают завести крылатого ангела у себя дома, в квартире в том числе. Домашние голуби способны приносить эстетическое удовлетворение своим ярким внешним видом или необычным стилем полета.", "/images/birds/pigeon.jpg", "Голубь", "Растительноядные", "Средний" },
                    { 7, "Ворон – это символичный, для некоторых даже эпичный представитель пернатых. Многие орнитологи считают врановых самыми умными из птиц: они способны различать цвета и формы, запоминают запахи, реагируют на тембр голоса, имеют навыки к подражанию звуков. Ручной ворон отличается от сородичей невероятным интеллектом, нетипичным для птиц. Он кардинально отличается от попугаев и других пернатых, а еще обладает невероятной привязанностью, как верный пес.", "/images/birds/raven.jpeg", "Ворон", "Всеядные", "Крупный" },
                    { 8, "Пожалуй, ни одной птице не посвящено столько стихов и произведений, как соловью. И ведь неспроста. Его пение поражает разнообразием репертуара, силой звука и голоса. При этом совершенно удивительно после услышанного восхитительного исполнения увидеть этого невзрачного артиста. Соловей не привлекает внешними данными, зато изумляет его певческое мастерство. К тому же, соловьиные песни возвещают о приходе весны и цветении природы вокруг, от чего на душе становится теплей.", "/images/birds/nightingale.jpg", "Соловей", "Насекомоядные", "Маленький" },
                    { 9, "Дрозды – недоверчивые, нервные и пугливые птицы, хотя часто можно встретить и спокойных особей с уравновешенным характером. Они также непоседливы, умны и активны. В домашних условиях любители птиц содержат, как правило, певчего дрозда, хотя среди лучших певцов, обитающих в лесах России, можно назвать чёрного и сизого дрозда, а также очень нарядных и голосистых птиц из группы каменных дроздов.", "/images/birds/blackbird.jpeg", "Дрозд", "Насекомоядные", "Средний" },
                    { 10, "Скворец – это искусный звукоподражатель, он может имитировать самые разные звуки: мяуканье котов, кваканье жаб, крик ласточек, петухов, куриц, уток и многое другое. Он прекрасно поёт, легко приручается и охотно общается с человеком. Если вы видите скворца возле вашего дома, то это не редкость, так как скворцы обитают в городских парках, садах и около домов.", "/images/birds/starling.jpg", "Скворец", "Всеядные", "Средний" },
                    { 11, "Сова, даже домашняя, – хищная птица, которая имеет острый клюв и когти. Это милый пушистик под покровом ночи насквозь протыкает свою жертву когтями и раздирает клювом. Совы по природе очень умные и хитрые, взмах их крыла совершенно бесшумен, что позволяет максимально приблизиться к жертве не вызывая подозрений. Мощные сильные лапы способны разорвать добычу, которая не превышает размера самой птицы. Поэтому, если в вашем доме имеется хомяк, кот или маленькая собака, не рекомендуется заводить хищную птицу, так как любимцы легко могут стать ее жертвой.", "/images/birds/owl.jpg", "Сова", "Плотоядные", "Средний" },
                    { 12, "Зяблики - одна из самых распространенных певчих птиц в средней полосе России, и неудивительно, что при этом они являются одними из самых популярных в качестве домашних любимцев. Однако стоит отметить, что хоть зяблики и приживаются в неволе, по-настоящему ручными они все-таки не становятся. И это требует от владельца определенного опыта в содержании птиц, что в итоге делает зябликов птицей не для новичков.", "/images/birds/finch.jpg", "Зяблик", "Растительноядные", "Маленький" },
                    { 13, "Синица в домашних условиях может жить долго и счастливо. Главное, подобрать для нее правильный рацион. Она не боится человека, легко приучается к жизни в неволе, красиво поёт и неприхотлива в содержании. Они не боятся проживать по соседству с человеком и отлично себя чувствуют на приусадебных участках, в садах, городских скверах и лесопарковых зонах.", "/images/birds/titmouse.jpg", "Синица", "Насекомоядные", "Маленький" },
                    { 14, "Жаворонки пугливые, настороженные птицы, плохо поддающиеся приручению. Жаворонки, привыкшие к дому и владельцам, будут пугаться незнакомых людей. Эти птицы подходят для опытных любителей певчих птиц. При содержании в домашних условиях необходимо использовать несколько клеток разного типа. Жаворонки не только отличные певцы, но и превосходные «летуны». Такое свойство птицам данного семейства подарила сама природа - п0ри относительно небольшом туловище обладают огромными крыльями, коротким хвостом.", "/images/birds/skylark.jpg", "Жаворонок", "Растительноядные", "Маленький" },
                    { 15, "В домашних условиях большинство коноплянок остаются пугливыми птицами, часто бьются о прутья клетки при резких движениях человека или домашних животных. Тем не менее их часто содержат в клетках из-за красивого пения. При вольерном содержании иногда дают потомство, получены гибриды с канарейками, щеглами и зеленушками. Гибриды с канарейками отличаются неплохими голосовыми данными и даже во взрослом состоянии способны совершенствовать песню, перенимая её у поющих кенаров.", "/images/birds/linnet.jpg", "Коноплянка", "Растительноядные", "Маленький" },
                    { 16, "Галка неприхотлива в неволе, как и другие врановые, мирится с вегетарианским столом. Кроме того, характер у нее относительно спокойный. Галка — единственная из врановых птиц, голос которой не лишен мелодичности. Жаль, что галка несколько «глупее» вороны и даже сороки и грача. Она плохо разбирает врагов и друзей, идет иногда на зов к незнакомому человеку. ", "/images/birds/jackdaw.jpeg", "Галка", "Всеядные", "Средний" },
                    { 17, "Фазаны в домашнем хозяйстве встречаются не часто. Однако в последние годы в России к этому направлению значительно активизировался интерес. Птиц используют не только для получения диетического мяса и яйца. Многие состоятельные люди держат этих великолепных по красоте птиц в качестве декоративных. Фазаны горделиво разгуливают по саду, словно маленькие павлины. От их нарядного оперения невозможно оторвать взгляд.", "/images/birds/pheasant.jpg", "Фазан", "Всеядные", "Крупный" },
                    { 18, "Снегири – это не только красивые птицы, но и удивительные создания, способные легко запоминать и воспроизводить мелодии и звуки. Что касается характера, то они отличаются уравновешенностью. Эти птицы спокойны, они никуда не торопятся, аккуратны и осмотрительны. Снегири неплохо живут в соседстве с людьми, неволю они переносят хорошо. Только из-за снижения количества полетов начинают полнеть. ", "/images/birds/bullfinch.jpg", "Снегирь", "Растительноядные", "Маленький" },
                    { 19, "Воробьи – общительные, дружелюбные птички. Живут в тесном соседстве с животными, другими пернатыми и человеком. Правда шумные и суетливые по природе, не всегда уживаются со стрижами и синицами. Известно, что у воробьев развита память. Доказано, что они могут составлять логическую цепочку из событий. Боятся кошек, но любят дразнить их, прыгая на коротких лапках у ее миски.", "/images/birds/sparrow.jpg", "Воробей", "Растительноядные", "Маленький" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Переноски для птиц", "carriers" },
                    { 2, "Корм", "food" },
                    { 3, "Кормушки", "feeders" },
                    { 4, "Лакомства", "goodies" },
                    { 5, "Средства по уходу", "careproducts" },
                    { 6, "Клетки", "cells" },
                    { 7, "Игрушки", "toys" },
                    { 8, "Наполнители", "fillers" },
                    { 9, "Аксессуары", "accessories" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Description", "ImageSource", "Name" },
                values: new object[,]
                {
                    { 1, "Наиболее широко распространенный вид корма для большинства растительноядных птиц и всех видов грызунов. Как основной компонент входит в состав большинства питательных смесей. Обладает хорошей всхожестью и может быть использован для подкормки животных зеленью, особенно в осенне-зимний период. Для полноценного питания птиц в неволе необходим пророщенный овес.", "/images/ingredients/oats.jpg", "Овес" },
                    { 2, "Просо - питательный злак, широко используемый для всех декоративных зерноядных птиц и грызунов, ценный корм для птицы, богатый каротином и аминокислотами. Просо служит основой для составления различных питательных зерновых смесей, его доля в рационе птиц достигает 60-70%. Содержит много клетчатки и белка, при этом не содержит глютена и является самым щелочным злаковым - легче переносится желудочно-кишечным трактом. Сухой корм для волнистых попугайчиков, средних попугаев и канареек обязательно должен содержать просо.", "/images/ingredients/millet.jpg", "Просо" },
                    { 3, "Канареечное семя наиболее подходящий корм для канареек. Канареечник в диком виде растет на Канарских островах. Семена этого злакового растения завезены в Европу вместе с канарейками. Сейчас их возделывают в некоторых странах на юге Европы, где широко используют в качестве корма для птиц. Канареечное семя включает триптофан и помогает птицам справляться со стрессом. Его количество в составе кормов может доходить до 15%.", "/images/ingredients/canaryseed.jpg", "Канареечное семя" },
                    { 4, "Конопляное семя — любимый корм всех зерноядных птиц. Зерна конопли содержат полиненасыщенные жирные кислоты. В небольшом количестве они очень полезны для иммунитета, пищеварения, оперения. Но если давать их птице слишком много, то вскоре придется вводить ограничения в рацион питания из-за развившегося ожирения. Более того, чрезмерное употребление конопляного семени может привести к опухоли век, слепоте и даже к гибели птицы.", "/images/ingredients/hempseed.jpg", "Конопляное семя" },
                    { 5, "Рапс может быть полезным и питательным источником пищи для птиц, если используется правильно. Белки рапса хорошо сбалансированы по аминокислотному составу, по содержанию лизина приближаются к сое, а по метионину и цистину, кальцию и фосфору значительно превосходят ее. Однако, перед тем как использовать рапс необходимо учитывать некоторые факторы. Во-первых, рапс может содержать глюкозинолаты, которые могут быть вредными для некоторых видов птиц, если они употребляются в больших количествах. Во-вторых, рапс может быть токсичным, если он загнивает или становится жирным. Это может произойти, если рапс не хранится должным образом или если он используется в слишком больших количествах. Поэтому необходимо следить за качеством и количеством рапса в рационе птиц.", "/images/ingredients/rapeseed.jpg", "Рапс" },
                    { 6, "Кунжут — одно из лучших масличных растений, культивируемое от западных берегов Африки вплоть до Китая и Японии, а также в Америке. Семена кунжута являются лидером по содержанию кальция. Кроме того, кунжутное семя богато витаминами В1 и Е, минералами и полиненасыщенными жирными линолевыми кислотами. Кунжут незаменим для дополнительного поддержания сил организма в период стресса, линьки у птиц. Однако, не следует давать птицам слишком много кунжута, так как это может привести к дисбалансу питательных веществ в рационе. Также следует учитывать, что кунжут может быть довольно жирным, поэтому его нужно вводить постепенно в рацион птиц, начиная с небольшого количества.", "/images/ingredients/sesame.jpeg", "Кунжут" },
                    { 7, "Подсолнечник — очень питательный вид корма. Однако подсолнечник не следует включать в больших количествах, так как избыток маслянистых веществ может вызвать нарушения обмена веществ. Очень полезны для птиц белые семена подсолнечника, так как они содержат меньше жиров. Полосатые семена богаты ненасыщенными жирными кислотами, которые благотворно влияют на оперение птиц.", "/images/ingredients/sunflower.jpg", "Подсолнечник" },
                    { 8, "Пшеница является богатым источником углеводов и белков, что делает ее хорошим источником энергии и питательных веществ для птиц. Однако, необходимо учитывать, что пшеница может быть довольно бедной по содержанию аминокислот, витаминов и минералов. Поэтому, если пшеница является основным источником пищи, то необходимо дополнительно добавлять другие виды кормов и/или дополнительные пищевые добавки, чтобы обеспечить птицам все необходимые питательные вещества. Кроме того, необходимо учитывать, что пшеница может быть довольно трудноусвояемой для молодых птиц и некоторых пород. Поэтому, если у вас есть молодые птицы или птицы с проблемами пищеварения, то необходимо убедиться, что они могут правильно усваивать пшеницу. В целом, пшеница может быть полезным источником пищи для птиц, если она используется в сочетании с другими видами кормов и дополнительными пищевыми добавками.", "/images/ingredients/wheat.jpg", "Пшеница" },
                    { 9, "Ячмень является одной из наиболее распространенных зерновых культур, используемых в качестве корма для птиц. В зависимости от вида птицы и ее возраста, ячмень может использоваться как основной источник питательных веществ или в комбинации с другими зерновыми культурами в кормовых смесях. Он содержит богатый набор белков, углеводов, клетчатки, минералов, витаминов и аминокислот, таких как лизин и треонин, которые важны для роста и развития птиц. Однако ячмень содержит фитиновую кислоту, которая может затруднять усвоение некоторых питательных веществ, таких как кальций, железо и цинк. Чтобы уменьшить воздействие фитиновой кислоты, ячмень может быть обработан термически или использован в сочетании с другими кормовыми ингредиентами, такими как мясо-костная мука или фосфаты.", "/images/ingredients/barley.jpg", "Ячмень" },
                    { 10, "Рожь – корм, используемый, когда недостает другого злакового зерна. Если переборщить с рожью, у пернатых начинаются проблемы с пищеварением. Это объясняется сильным набуханием крахмала ржи. Нельзя кормить пернатых недавно убранным зерном – провоцирует недуги ЖКТ. Зерно выдерживается в течение трех месяцев. Как и с другими зерновыми культурами, рожь может быть обработана термически или использована в сочетании с другими кормовыми ингредиентами, чтобы снизить уровень антипитательных веществ.", "/images/ingredients/rye.jpg", "Рожь" },
                    { 11, "Кукуруза - один из самых лучших и широко используемых видов кормов для декоративных зерноядных птиц. По энергетической ценности кукуруза превосходит многие другие зерновые корма, содержит много белка (9-12%), углеводов (65-70%) и мало - клетчатки. Богата железом и каротином (провитамином А). Особенно много провитамина А в желтозерных сортах. Кукуруза является одним из основных компонентов для составления различных зерновых смесей. Многие птицы неохотно едят сухую кукурузу, выклевывая лишь сердцевину. В этом случае рекомендуется зерно замачивать или запаривать.", "/images/ingredients/corn.jpg", "Кукуруза" },
                    { 12, "Сорго - это питательная пища, не проигрывающая по калорийности кукурузе и ячменю. В зерне почти в 10 раз выше содержание кальция, натрия и магния в сравнении с другими культурами, без вреда может занимать до 50% рациона несушек. Это способствует насыщению организма птицы минералами и формированию яичной скорлупы. Злаковые корма — основа калорийного питания уток и голубей, сорго охотно поедается ими и хорошо переваривается. Питательная ценность выше, чем у овса. Сорго - разновидность проса, поэтому попугаи с удовольствием его едят.", "/images/ingredients/sorghum.jpg", "Сорго" },
                    { 13, "Льняное семечко может использоваться в качестве дополнительного источника питательных веществ в корму для птиц. В семечках содержится большое количество лигнинов, которые отвечают за здоровье гормональной системы. Также семя содержит несколько важных для работы иммунитета и общего состояния организма жирных кислот и белок, который обладает особым строением и очень быстро усваивается организмом. Альфа-линолиевая кислота предупреждает развитие опухолей, стимулирует рост оперения и укрепляет иммунную систему.", "/images/ingredients/flaxseed.jpg", "Льняное семечко" },
                    { 14, "Соевые корма высоко ценятся из-за своего состава, большую часть которого занимает протеин, остальную – жиры, аминокислоты, витамины группы В, минералы, фосфор. Питательный продукт улучшает здоровье птиц, увеличивает продуктивность, несушки откладывают крупные яйца с крепкой скорлупой, мясные породы быстрее набирают вес. Наличие аминокислот ускоряет метаболизм в тканях. Но в свежих соевых продуктах имеются антипитательные (токсичные) вещества, поэтому не рекомендуется давать птице цельное зерно в большом объеме и зеленую часть бобовых. Накапливаясь в организме, токсины замедляют рост, отрицательно влияют на продуктивность и даже провоцируют летальный исход.", "/images/ingredients/soy.jpeg", "Соя" },
                    { 15, "Гречка, гречиха, или греча – достаточно широко распространенный на территории России продукт. Она богата белками, микроэлементами и аминокислотами, поэтому гречневая крупа и ее производные активно применяются во всех сферах пищевой промышленности: начиная от производства детского питания и заканчивая изготовлением кормов для животных. Для корма скота и домашней птицы чаще всего приобретают отруби и мучную пыль, которые отсеиваются в процессе обработки зерен.", "/images/ingredients/buckwheat.jpg", "Гречиха" },
                    { 16, "Орехи особенно популярны в качестве зимнего корма для птиц, потому что они долговечны и их легко спрятать для последующего использования. Многие птицы, которые питаются орехами, проводят недели осенью, запасая орехи в полостях коры, небольших углублениях в земле или в других укрытиях. Когда других источников пищи не хватает или плохая погода не позволяет правильно добывать пищу, птицы возвращаются в эти укрытия, чтобы съесть припасенные орехи. Но следует обращать внимание на их использование, так как не все виды орехов одинаково полезны для птиц, а некоторые могут быть вредными. Самыми распространенными орехами, которые могут использоваться в корму для птиц, являются грецкий орех, арахис, миндаль, кешью, фисташки.", "/images/ingredients/nuts.jpg", "Орехи" },
                    { 17, "Овощи могут быть важным источником питательных веществ в рационе птиц. Наиболее распространены: морковь (богата каротином, который может помочь поддерживать здоровье глаз и кожи у птиц), капуста (содержит много витамина С, кальция, витаминов и минералов), огурцы (богаты водой и могут помочь увлажнить корм птиц), брокколи (содержит много витаминов и минералов, включая витамины А, С и К, а также кальций и железо), тыква (содержит витамин А, калий и другие минералы). Кроме того, овощи, такие как листовые зеленые, помидоры, перец, редис, свекла и многие другие, могут также использоваться в корму для птиц. Важно помнить, что некоторые овощи могут быть токсичными для птиц, поэтому необходимо обращаться к опытным специалистам, чтобы определить, какие овощи лучше всего использовать в рационе для конкретного вида.", "/images/ingredients/vegetables.jpg", "Овощи" },
                    { 18, "Фрукты могут быть хорошим источником питательных веществ для птиц, но должны использоваться осторожно и только в умеренных количествах. Наиболее популярны: яблоки (источник витамина C, клетчатки, калия и фолиевой кислоты), бананы (богаты калием, витамином С, витамином B6, клетчаткой), груши (источником витамина C, калия, клетчатки), ягоды (черника, малина и клубника богаты антиоксидантами и витаминами C и К), киви (богаты витамином C, клетчаткой и калием), апельсины (содержат много витамина C и клетчатки, фолиевую кислоту и калий). Однако, некоторые фрукты могут быть опасными для птиц, поэтому необходимо быть осторожным. Например, абрикосы, персики, сливы и черешня могут содержать косточки, которые могут быть опасны для птиц. Кроме того, некоторые фрукты могут быть кислыми и вызывать проблемы с желудком у птиц.", "/images/ingredients/fruits.jpg", "Фрукты" },
                    { 19, "Насекомые содержат много важных питательных веществ, таких как белок, жир, витамины и минералы. Чаще всего в кормах используются: сверчки, мухи, мотыльки, черви, тараканы. При выборе насекомых следует обратить внимание на их размер: более крупные птицы могут легко справляться с крупными сверчками, тогда как маленьким птицам лучше подойдут мелкие насекомые, например, мухи.", "/images/ingredients/insects.jpg", "Насекомые" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "CommonUser" },
                    { 3, "CommonUser" },
                    { 4, "CommonUser" },
                    { 5, "CommonUser" },
                    { 6, "CommonUser" },
                    { 7, "CommonUser" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "TotalCost", "UserId" },
                values: new object[,]
                {
                    { 1, 0m, 1 },
                    { 2, 0m, 2 },
                    { 3, 0m, 3 },
                    { 4, 0m, 4 },
                    { 5, 0m, 5 },
                    { 6, 0m, 6 },
                    { 7, 0m, 7 }
                });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "Email", "Password", "UserId" },
                values: new object[,]
                {
                    { 1, "admin@email.com", "$2a$11$gZIWVCIZIUg2bFZbDJeCiuELcQhSpjFBVjv3UdRXDH5rKpYrPHzXC", 1 },
                    { 2, "user1@email.com", "$2a$11$qO0E1f55kP/s0ZCji0APGuVSBdhshgLGhdtynYPd/Kv0C8HGMZ4NK", 2 },
                    { 3, "user2@email.com", "$2a$11$kKKcWkmabI.KpR8oFnnPSeaUP29/D1S54M9xSt/xRFTnIjtpAdn5u", 3 },
                    { 4, "user3@email.com", "$2a$11$jAfIVsTycQWrJVIIZBaCIOu5VR9xpA/Np7QAbW6l40U.EkzH0NIUa", 4 },
                    { 5, "user4@email.com", "$2a$11$Rom4Ia5TUmdDyxuOFNo3relfQ/vbp3VlOH1WPFDK9lIxxPQqM7ObC", 5 },
                    { 6, "user5@email.com", "$2a$11$5/fg8qK92gAyyBkzBSpu3O4fwMGEJRiaKpQHXsLrcYUK7x.f4Yysu", 6 },
                    { 7, "user6@email.com", "$2a$11$Eg1jCWsRuQotB5YS7AQ0buQ0tt7DRA/d9WGTJrZE6i9oVZXu0dvbm", 7 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BirdSize", "CategoryId", "Description", "ImageSource", "Manufacturer", "Name", "NutritionType" },
                values: new object[,]
                {
                    { 1, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Malinki", "Переноска для птиц 1", null },
                    { 2, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Malinki", "Переноска для птиц 2", null },
                    { 3, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Travellor", "Переноска для птиц 3", null },
                    { 4, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Malinki", "Переноска для птиц 4", null },
                    { 5, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Malinki", "Переноска для птиц 5", null },
                    { 6, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Vitakraft", "Переноска для птиц 6", null },
                    { 7, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 7", null },
                    { 8, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Vitakraft", "Переноска для птиц 8", null },
                    { 9, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 9", null },
                    { 10, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Travellor", "Переноска для птиц 10", null },
                    { 11, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Vitakraft", "Переноска для птиц 11", null },
                    { 12, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Vitakraft", "Переноска для птиц 12", null },
                    { 13, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 13", null },
                    { 14, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Vitakraft", "Переноска для птиц 14", null },
                    { 15, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Malinki", "Переноска для птиц 15", null },
                    { 16, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Vitakraft", "Переноска для птиц 16", null },
                    { 17, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Travellor", "Переноска для птиц 17", null },
                    { 18, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Travellor", "Переноска для птиц 18", null },
                    { 19, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Travellor", "Переноска для птиц 19", null },
                    { 20, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Travellor", "Переноска для птиц 20", null },
                    { 21, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №1", "Плотоядные" },
                    { 22, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №2", "Всеядные" },
                    { 23, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №3", "Растительноядные" },
                    { 24, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/prestige.jpeg", "Prestige", "Корм для маленьких птиц №4", "Насекомоядные" },
                    { 25, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №5", "Плотоядные" },
                    { 26, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №6", "Всеядные" },
                    { 27, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №7", "Растительноядные" },
                    { 28, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №8", "Насекомоядные" },
                    { 29, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №9", "Плотоядные" },
                    { 30, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №10", "Всеядные" },
                    { 31, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №11", "Растительноядные" },
                    { 32, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №12", "Насекомоядные" },
                    { 33, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №13", "Плотоядные" },
                    { 34, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/prestige.jpeg", "Prestige", "Корм для маленьких птиц №14", "Всеядные" },
                    { 35, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №15", "Растительноядные" },
                    { 36, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №16", "Насекомоядные" },
                    { 37, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №17", "Плотоядные" },
                    { 38, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/prestige.jpeg", "Prestige", "Корм для маленьких птиц №18", "Всеядные" },
                    { 39, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №19", "Растительноядные" },
                    { 40, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №20", "Насекомоядные" },
                    { 41, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №1", null },
                    { 42, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №2", null },
                    { 43, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №3", null },
                    { 44, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №4", null },
                    { 45, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №5", null },
                    { 46, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №6", null },
                    { 47, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №7", null },
                    { 48, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №8", null },
                    { 49, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №9", null },
                    { 50, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №10", null },
                    { 51, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №11", null },
                    { 52, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №12", null },
                    { 53, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №13", null },
                    { 54, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №14", null },
                    { 55, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №15", null },
                    { 56, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №16", null },
                    { 57, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №17", null },
                    { 58, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №18", null },
                    { 59, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №19", null },
                    { 60, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №20", null },
                    { 61, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №1", null },
                    { 62, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №2", null },
                    { 63, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №3", null },
                    { 64, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №4", null },
                    { 65, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №5", null },
                    { 66, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №6", null },
                    { 67, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №7", null },
                    { 68, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №8", null },
                    { 69, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №9", null },
                    { 70, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №10", null },
                    { 71, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №11", null },
                    { 72, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №12", null },
                    { 73, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №13", null },
                    { 74, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №14", null },
                    { 75, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №15", null },
                    { 76, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №16", null },
                    { 77, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №17", null },
                    { 78, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №18", null },
                    { 79, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №19", null },
                    { 80, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №20", null },
                    { 81, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №1", null },
                    { 82, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №2", null },
                    { 83, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №3", null },
                    { 84, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №4", null },
                    { 85, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №5", null },
                    { 86, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №6", null },
                    { 87, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №7", null },
                    { 88, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №8", null },
                    { 89, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №9", null },
                    { 90, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №10", null },
                    { 91, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №11", null },
                    { 92, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №12", null },
                    { 93, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №13", null },
                    { 94, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №14", null },
                    { 95, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №15", null },
                    { 96, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №16", null },
                    { 97, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №17", null },
                    { 98, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №18", null },
                    { 99, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №19", null },
                    { 100, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №20", null },
                    { 101, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №1", null },
                    { 102, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №2", null },
                    { 103, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №3", null },
                    { 104, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №4", null },
                    { 105, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №5", null },
                    { 106, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №6", null },
                    { 107, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №7", null },
                    { 108, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №8", null },
                    { 109, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №9", null },
                    { 110, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №10", null },
                    { 111, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №11", null },
                    { 112, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №12", null },
                    { 113, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №13", null },
                    { 114, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №14", null },
                    { 115, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №15", null },
                    { 116, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №16", null },
                    { 117, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №17", null },
                    { 118, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №18", null },
                    { 119, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №19", null },
                    { 120, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №20", null },
                    { 121, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №1", null },
                    { 122, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №2", null },
                    { 123, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №3", null },
                    { 124, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №4", null },
                    { 125, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №5", null },
                    { 126, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №6", null },
                    { 127, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №7", null },
                    { 128, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №8", null },
                    { 129, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №9", null },
                    { 130, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №10", null },
                    { 131, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №11", null },
                    { 132, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №12", null },
                    { 133, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №13", null },
                    { 134, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №14", null },
                    { 135, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №15", null },
                    { 136, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №16", null },
                    { 137, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №17", null },
                    { 138, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №18", null },
                    { 139, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №19", null },
                    { 140, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №20", null },
                    { 141, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №1", null },
                    { 142, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №2", null },
                    { 143, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №3", null },
                    { 144, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №4", null },
                    { 145, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №5", null },
                    { 146, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №6", null },
                    { 147, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №7", null },
                    { 148, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №8", null },
                    { 149, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №9", null },
                    { 150, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №10", null },
                    { 151, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №11", null },
                    { 152, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №12", null },
                    { 153, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №13", null },
                    { 154, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №14", null },
                    { 155, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №15", null },
                    { 156, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №16", null },
                    { 157, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №17", null },
                    { 158, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №18", null },
                    { 159, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №19", null },
                    { 160, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №20", null },
                    { 161, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №1", null },
                    { 162, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №2", null },
                    { 163, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №3", null },
                    { 164, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №4", null },
                    { 165, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №5", null },
                    { 166, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №6", null },
                    { 167, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №7", null },
                    { 168, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №8", null },
                    { 169, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №9", null },
                    { 170, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №10", null },
                    { 171, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №11", null },
                    { 172, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №12", null },
                    { 173, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №13", null },
                    { 174, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №14", null },
                    { 175, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №15", null },
                    { 176, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №16", null },
                    { 177, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №17", null },
                    { 178, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №18", null },
                    { 179, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №19", null },
                    { 180, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №20", null }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "UserAddress", "UserId", "UserName", "UserTelephone" },
                values: new object[,]
                {
                    { 1, null, 1, "Администратор", null },
                    { 2, "г. Москва, ул. Пользователя 1", 2, "Пользователь 1", "+7 (926) 111-11-11" },
                    { 3, "г. Москва, ул. Пользователя 2", 3, "Пользователь 2", "+7 (926) 222-22-22" },
                    { 4, "г. Москва, ул. Пользователя 3", 4, "Пользователь 3", "+7 (926) 333-33-33" },
                    { 5, "г. Москва, ул. Пользователя 4", 5, "Пользователь 4", "+7 (926) 444-44-44" },
                    { 6, "г. Москва, ул. Пользователя 5", 6, "Пользователь 5", "+7 (926) 555-55-55" },
                    { 7, "г. Москва, ул. Пользователя 6", 7, "Пользователь 6", "+7 (926) 666-66-66" }
                });

            migrationBuilder.InsertData(
                table: "ProductIngredients",
                columns: new[] { "IngredientId", "ProductId", "Id" },
                values: new object[,]
                {
                    { 19, 21, 1 },
                    { 3, 22, 36 },
                    { 4, 22, 37 },
                    { 5, 22, 38 },
                    { 6, 22, 39 },
                    { 19, 22, 11 },
                    { 1, 23, 16 },
                    { 2, 23, 17 },
                    { 3, 23, 18 },
                    { 4, 23, 19 },
                    { 19, 24, 2 },
                    { 19, 25, 3 },
                    { 7, 26, 40 },
                    { 8, 26, 41 },
                    { 9, 26, 42 },
                    { 10, 26, 43 },
                    { 19, 26, 12 },
                    { 5, 27, 20 },
                    { 6, 27, 21 },
                    { 7, 27, 22 },
                    { 8, 27, 23 },
                    { 19, 28, 4 },
                    { 19, 29, 5 },
                    { 11, 30, 44 },
                    { 12, 30, 45 },
                    { 13, 30, 46 },
                    { 14, 30, 47 },
                    { 19, 30, 13 },
                    { 9, 31, 24 },
                    { 10, 31, 25 },
                    { 11, 31, 26 },
                    { 12, 31, 27 },
                    { 19, 32, 6 },
                    { 19, 33, 7 },
                    { 15, 34, 48 },
                    { 16, 34, 49 },
                    { 17, 34, 50 },
                    { 18, 34, 51 },
                    { 19, 34, 14 },
                    { 13, 35, 28 },
                    { 14, 35, 29 },
                    { 15, 35, 30 },
                    { 16, 35, 31 },
                    { 19, 36, 8 },
                    { 19, 37, 9 },
                    { 1, 38, 52 },
                    { 2, 38, 53 },
                    { 3, 38, 54 },
                    { 4, 38, 55 },
                    { 19, 38, 15 },
                    { 1, 39, 34 },
                    { 2, 39, 35 },
                    { 17, 39, 32 },
                    { 18, 39, 33 },
                    { 19, 40, 10 }
                });

            migrationBuilder.InsertData(
                table: "ProductPrices",
                columns: new[] { "Id", "InStock", "Price", "ProductId", "Weight" },
                values: new object[,]
                {
                    { 1, 61, 1966m, 1, 0 },
                    { 2, 1, 1486m, 2, 0 },
                    { 3, 17, 773m, 3, 0 },
                    { 4, 55, 502m, 4, 0 },
                    { 5, 45, 802m, 5, 0 },
                    { 6, 8, 1195m, 6, 0 },
                    { 7, 78, 434m, 7, 0 },
                    { 8, 24, 1141m, 8, 0 },
                    { 9, 35, 1915m, 9, 0 },
                    { 10, 4, 1775m, 10, 0 },
                    { 11, 21, 1924m, 11, 0 },
                    { 12, 95, 966m, 12, 0 },
                    { 13, 92, 744m, 13, 0 },
                    { 14, 49, 1239m, 14, 0 },
                    { 15, 3, 1845m, 15, 0 },
                    { 16, 39, 1425m, 16, 0 },
                    { 17, 65, 1201m, 17, 0 },
                    { 18, 41, 1034m, 18, 0 },
                    { 19, 14, 258m, 19, 0 },
                    { 20, 53, 1107m, 20, 0 },
                    { 21, 12, 283m, 21, 200 },
                    { 22, 67, 412m, 21, 400 },
                    { 23, 86, 977m, 21, 800 },
                    { 24, 89, 1496m, 21, 1000 },
                    { 25, 5, 353m, 22, 200 },
                    { 26, 42, 494m, 22, 400 },
                    { 27, 47, 826m, 22, 800 },
                    { 28, 18, 1390m, 22, 1000 },
                    { 29, 54, 274m, 23, 200 },
                    { 30, 9, 455m, 23, 400 },
                    { 31, 17, 853m, 23, 800 },
                    { 32, 50, 1134m, 23, 1000 },
                    { 33, 24, 314m, 24, 200 },
                    { 34, 78, 473m, 24, 400 },
                    { 35, 47, 970m, 24, 800 },
                    { 36, 23, 1088m, 24, 1000 },
                    { 37, 57, 348m, 25, 200 },
                    { 38, 27, 428m, 25, 400 },
                    { 39, 51, 962m, 25, 800 },
                    { 40, 46, 1438m, 25, 1000 },
                    { 41, 97, 343m, 26, 200 },
                    { 42, 12, 555m, 26, 400 },
                    { 43, 49, 927m, 26, 800 },
                    { 44, 84, 1166m, 26, 1000 },
                    { 45, 44, 210m, 27, 200 },
                    { 46, 19, 479m, 27, 400 },
                    { 47, 66, 968m, 27, 800 },
                    { 48, 84, 1442m, 27, 1000 },
                    { 49, 44, 217m, 28, 200 },
                    { 50, 40, 576m, 28, 400 },
                    { 51, 56, 998m, 28, 800 },
                    { 52, 16, 1047m, 28, 1000 },
                    { 53, 73, 232m, 29, 200 },
                    { 54, 29, 408m, 29, 400 },
                    { 55, 22, 897m, 29, 800 },
                    { 56, 85, 1284m, 29, 1000 },
                    { 57, 69, 311m, 30, 200 },
                    { 58, 89, 500m, 30, 400 },
                    { 59, 45, 966m, 30, 800 },
                    { 60, 15, 1406m, 30, 1000 },
                    { 61, 24, 294m, 31, 200 },
                    { 62, 50, 498m, 31, 400 },
                    { 63, 26, 992m, 31, 800 },
                    { 64, 13, 1115m, 31, 1000 },
                    { 65, 73, 227m, 32, 200 },
                    { 66, 84, 461m, 32, 400 },
                    { 67, 12, 908m, 32, 800 },
                    { 68, 30, 1147m, 32, 1000 },
                    { 69, 77, 256m, 33, 200 },
                    { 70, 20, 434m, 33, 400 },
                    { 71, 63, 862m, 33, 800 },
                    { 72, 48, 1193m, 33, 1000 },
                    { 73, 80, 285m, 34, 200 },
                    { 74, 93, 436m, 34, 400 },
                    { 75, 69, 919m, 34, 800 },
                    { 76, 45, 1063m, 34, 1000 },
                    { 77, 1, 214m, 35, 200 },
                    { 78, 34, 484m, 35, 400 },
                    { 79, 75, 878m, 35, 800 },
                    { 80, 47, 1100m, 35, 1000 },
                    { 81, 16, 391m, 36, 200 },
                    { 82, 55, 558m, 36, 400 },
                    { 83, 31, 826m, 36, 800 },
                    { 84, 0, 1314m, 36, 1000 },
                    { 85, 67, 327m, 37, 200 },
                    { 86, 80, 573m, 37, 400 },
                    { 87, 1, 856m, 37, 800 },
                    { 88, 72, 1117m, 37, 1000 },
                    { 89, 58, 340m, 38, 200 },
                    { 90, 6, 404m, 38, 400 },
                    { 91, 66, 834m, 38, 800 },
                    { 92, 27, 1488m, 38, 1000 },
                    { 93, 88, 255m, 39, 200 },
                    { 94, 81, 466m, 39, 400 },
                    { 95, 26, 833m, 39, 800 },
                    { 96, 88, 1457m, 39, 1000 },
                    { 97, 70, 292m, 40, 200 },
                    { 98, 89, 465m, 40, 400 },
                    { 99, 90, 860m, 40, 800 },
                    { 100, 44, 1333m, 40, 1000 },
                    { 101, 91, 1092m, 41, 0 },
                    { 102, 98, 282m, 42, 0 },
                    { 103, 25, 1464m, 43, 0 },
                    { 104, 30, 590m, 44, 0 },
                    { 105, 1, 1404m, 45, 0 },
                    { 106, 87, 1126m, 46, 0 },
                    { 107, 51, 1638m, 47, 0 },
                    { 108, 57, 1704m, 48, 0 },
                    { 109, 50, 1479m, 49, 0 },
                    { 110, 63, 1617m, 50, 0 },
                    { 111, 66, 200m, 51, 0 },
                    { 112, 21, 374m, 52, 0 },
                    { 113, 65, 349m, 53, 0 },
                    { 114, 79, 1999m, 54, 0 },
                    { 115, 89, 1469m, 55, 0 },
                    { 116, 35, 1889m, 56, 0 },
                    { 117, 8, 727m, 57, 0 },
                    { 118, 23, 480m, 58, 0 },
                    { 119, 11, 1780m, 59, 0 },
                    { 120, 93, 289m, 60, 0 },
                    { 121, 42, 849m, 61, 0 },
                    { 122, 81, 1359m, 62, 0 },
                    { 123, 96, 336m, 63, 0 },
                    { 124, 48, 1017m, 64, 0 },
                    { 125, 55, 1406m, 65, 0 },
                    { 126, 78, 254m, 66, 0 },
                    { 127, 90, 1682m, 67, 0 },
                    { 128, 2, 744m, 68, 0 },
                    { 129, 46, 587m, 69, 0 },
                    { 130, 38, 1591m, 70, 0 },
                    { 131, 15, 664m, 71, 0 },
                    { 132, 28, 1114m, 72, 0 },
                    { 133, 8, 1145m, 73, 0 },
                    { 134, 62, 1285m, 74, 0 },
                    { 135, 1, 1355m, 75, 0 },
                    { 136, 29, 891m, 76, 0 },
                    { 137, 49, 283m, 77, 0 },
                    { 138, 75, 318m, 78, 0 },
                    { 139, 97, 549m, 79, 0 },
                    { 140, 13, 1243m, 80, 0 },
                    { 141, 85, 505m, 81, 0 },
                    { 142, 45, 1986m, 82, 0 },
                    { 143, 88, 1143m, 83, 0 },
                    { 144, 9, 1850m, 84, 0 },
                    { 145, 76, 1735m, 85, 0 },
                    { 146, 94, 716m, 86, 0 },
                    { 147, 34, 1788m, 87, 0 },
                    { 148, 38, 1054m, 88, 0 },
                    { 149, 56, 1315m, 89, 0 },
                    { 150, 0, 537m, 90, 0 },
                    { 151, 13, 590m, 91, 0 },
                    { 152, 75, 984m, 92, 0 },
                    { 153, 98, 1787m, 93, 0 },
                    { 154, 67, 994m, 94, 0 },
                    { 155, 4, 517m, 95, 0 },
                    { 156, 22, 1680m, 96, 0 },
                    { 157, 20, 627m, 97, 0 },
                    { 158, 33, 900m, 98, 0 },
                    { 159, 17, 1675m, 99, 0 },
                    { 160, 21, 546m, 100, 0 },
                    { 161, 93, 1098m, 101, 0 },
                    { 162, 39, 1164m, 102, 0 },
                    { 163, 98, 1476m, 103, 0 },
                    { 164, 38, 1389m, 104, 0 },
                    { 165, 92, 545m, 105, 0 },
                    { 166, 2, 765m, 106, 0 },
                    { 167, 49, 1292m, 107, 0 },
                    { 168, 90, 1604m, 108, 0 },
                    { 169, 46, 273m, 109, 0 },
                    { 170, 59, 670m, 110, 0 },
                    { 171, 88, 941m, 111, 0 },
                    { 172, 25, 543m, 112, 0 },
                    { 173, 76, 1325m, 113, 0 },
                    { 174, 3, 847m, 114, 0 },
                    { 175, 54, 1690m, 115, 0 },
                    { 176, 49, 1582m, 116, 0 },
                    { 177, 54, 1443m, 117, 0 },
                    { 178, 39, 1852m, 118, 0 },
                    { 179, 4, 1948m, 119, 0 },
                    { 180, 10, 1480m, 120, 0 },
                    { 181, 93, 635m, 121, 0 },
                    { 182, 57, 524m, 122, 0 },
                    { 183, 58, 561m, 123, 0 },
                    { 184, 77, 1685m, 124, 0 },
                    { 185, 74, 860m, 125, 0 },
                    { 186, 9, 743m, 126, 0 },
                    { 187, 58, 774m, 127, 0 },
                    { 188, 41, 391m, 128, 0 },
                    { 189, 69, 1427m, 129, 0 },
                    { 190, 88, 1951m, 130, 0 },
                    { 191, 46, 666m, 131, 0 },
                    { 192, 0, 1340m, 132, 0 },
                    { 193, 11, 1978m, 133, 0 },
                    { 194, 7, 1691m, 134, 0 },
                    { 195, 45, 1289m, 135, 0 },
                    { 196, 68, 1618m, 136, 0 },
                    { 197, 52, 1021m, 137, 0 },
                    { 198, 50, 1347m, 138, 0 },
                    { 199, 41, 298m, 139, 0 },
                    { 200, 49, 744m, 140, 0 },
                    { 201, 79, 1727m, 141, 0 },
                    { 202, 83, 331m, 142, 0 },
                    { 203, 31, 522m, 143, 0 },
                    { 204, 90, 1681m, 144, 0 },
                    { 205, 90, 1346m, 145, 0 },
                    { 206, 21, 856m, 146, 0 },
                    { 207, 79, 1982m, 147, 0 },
                    { 208, 94, 1755m, 148, 0 },
                    { 209, 62, 1186m, 149, 0 },
                    { 210, 12, 208m, 150, 0 },
                    { 211, 33, 542m, 151, 0 },
                    { 212, 46, 659m, 152, 0 },
                    { 213, 35, 456m, 153, 0 },
                    { 214, 19, 669m, 154, 0 },
                    { 215, 8, 476m, 155, 0 },
                    { 216, 73, 384m, 156, 0 },
                    { 217, 6, 1712m, 157, 0 },
                    { 218, 36, 865m, 158, 0 },
                    { 219, 44, 696m, 159, 0 },
                    { 220, 62, 1875m, 160, 0 },
                    { 221, 52, 683m, 161, 0 },
                    { 222, 58, 1862m, 162, 0 },
                    { 223, 71, 640m, 163, 0 },
                    { 224, 88, 1122m, 164, 0 },
                    { 225, 27, 470m, 165, 0 },
                    { 226, 44, 1542m, 166, 0 },
                    { 227, 25, 1764m, 167, 0 },
                    { 228, 45, 872m, 168, 0 },
                    { 229, 26, 1825m, 169, 0 },
                    { 230, 39, 725m, 170, 0 },
                    { 231, 53, 540m, 171, 0 },
                    { 232, 45, 1182m, 172, 0 },
                    { 233, 21, 1361m, 173, 0 },
                    { 234, 9, 349m, 174, 0 },
                    { 235, 56, 683m, 175, 0 },
                    { 236, 52, 571m, 176, 0 },
                    { 237, 47, 937m, 177, 0 },
                    { 238, 19, 1142m, 178, 0 },
                    { 239, 91, 769m, 179, 0 },
                    { 240, 8, 1626m, 180, 0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ProductId", "UserId", "Comment", "Id", "ReviewDate" },
                values: new object[,]
                {
                    { 4, 5, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 20, new DateTime(2018, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 5, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 35, new DateTime(2021, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 5, "Отличное качество, синицы оценили!", 43, new DateTime(2021, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, 4, "Отличное качество, синицы оценили!", 36, new DateTime(2021, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, 6, "Производителю рекомендовал бы применять более эластичный материал.", 21, new DateTime(2018, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, 6, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 34, new DateTime(2018, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, 7, "Производителю рекомендовал бы применять более эластичный материал.", 2, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, 3, "Производителю рекомендовал бы применять более эластичный материал.", 18, new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, 6, "Производителю рекомендовал бы применять более эластичный материал.", 38, new DateTime(2020, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, 5, "Отличное качество, синицы оценили!", 4, new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, 7, "Отличное качество, синицы оценили!", 16, new DateTime(2020, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, 6, "Отличное качество, синицы оценили!", 30, new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54, 3, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 28, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 46, new DateTime(2019, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, 7, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 25, new DateTime(2020, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68, 4, "Производителю рекомендовал бы применять более эластичный материал.", 32, new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, 6, "Отличное качество, синицы оценили!", 23, new DateTime(2021, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74, 7, "Отличное качество, синицы оценили!", 5, new DateTime(2021, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 86, 5, "Производителю рекомендовал бы применять более эластичный материал.", 12, new DateTime(2019, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 87, 7, "Отличное качество, синицы оценили!", 50, new DateTime(2017, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 92, 2, "Отличное качество, синицы оценили!", 33, new DateTime(2019, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 93, 2, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 8, new DateTime(2019, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95, 3, "Отличное качество, синицы оценили!", 49, new DateTime(2018, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 96, 7, "Производителю рекомендовал бы применять более эластичный материал.", 44, new DateTime(2021, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 97, 2, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 47, new DateTime(2018, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 98, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 31, new DateTime(2020, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 105, 7, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 39, new DateTime(2021, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 106, 6, "Производителю рекомендовал бы применять более эластичный материал.", 40, new DateTime(2017, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 106, 7, "Отличное качество, синицы оценили!", 26, new DateTime(2021, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 117, 2, "Производителю рекомендовал бы применять более эластичный материал.", 13, new DateTime(2018, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 129, 3, "Производителю рекомендовал бы применять более эластичный материал.", 15, new DateTime(2019, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 138, 2, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 6, new DateTime(2017, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 138, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 7, new DateTime(2019, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 139, 2, "Отличное качество, синицы оценили!", 10, new DateTime(2017, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 139, 3, "Производителю рекомендовал бы применять более эластичный материал.", 41, new DateTime(2021, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 139, 7, "Отличное качество, синицы оценили!", 29, new DateTime(2017, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 141, 7, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 9, new DateTime(2019, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 144, 6, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 19, new DateTime(2018, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 146, 5, "Производителю рекомендовал бы применять более эластичный материал.", 48, new DateTime(2017, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 148, 6, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 42, new DateTime(2019, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 161, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 37, new DateTime(2019, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 164, 4, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 27, new DateTime(2018, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 164, 6, "Производителю рекомендовал бы применять более эластичный материал.", 45, new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 167, 5, "Отличное качество, синицы оценили!", 11, new DateTime(2019, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 169, 4, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 14, new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 171, 6, "Отличное качество, синицы оценили!", 17, new DateTime(2017, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 175, 2, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 1, new DateTime(2019, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 175, 4, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 24, new DateTime(2021, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 178, 3, "Отличное качество, синицы оценили!", 22, new DateTime(2017, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 178, 6, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 3, new DateTime(2019, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EndDate", "ProductPriceId", "StartDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 26, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 11, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 15 },
                    { 2, new DateTime(2023, 3, 18, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 27, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 24 },
                    { 3, new DateTime(2023, 4, 4, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 35, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 14 },
                    { 4, new DateTime(2023, 3, 28, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 37, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 18 },
                    { 5, new DateTime(2023, 4, 2, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 39, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 10 },
                    { 6, new DateTime(2023, 3, 21, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 41, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 22 },
                    { 7, new DateTime(2023, 3, 22, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 42, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 16 },
                    { 8, new DateTime(2023, 3, 30, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 48, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 17 },
                    { 9, new DateTime(2023, 3, 26, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 51, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 21 },
                    { 10, new DateTime(2023, 3, 22, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 54, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 23 },
                    { 11, new DateTime(2023, 3, 18, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 56, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 20 },
                    { 12, new DateTime(2023, 3, 22, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 58, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 11 },
                    { 13, new DateTime(2023, 4, 6, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 73, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 11 },
                    { 14, new DateTime(2023, 3, 31, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 97, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 17 },
                    { 15, new DateTime(2023, 4, 1, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 109, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 22 },
                    { 16, new DateTime(2023, 3, 21, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 119, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 15 },
                    { 17, new DateTime(2023, 3, 15, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 123, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 15 },
                    { 18, new DateTime(2023, 3, 23, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 135, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 15 },
                    { 19, new DateTime(2023, 3, 17, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 139, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 12 },
                    { 20, new DateTime(2023, 3, 31, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 140, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 23 },
                    { 21, new DateTime(2023, 3, 30, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 144, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 12 },
                    { 22, new DateTime(2023, 4, 5, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 202, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 17 },
                    { 23, new DateTime(2023, 3, 15, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 203, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 14 },
                    { 24, new DateTime(2023, 3, 22, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 211, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 19 },
                    { 25, new DateTime(2023, 4, 6, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 217, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 17 },
                    { 26, new DateTime(2023, 3, 17, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 232, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 13 },
                    { 27, new DateTime(2023, 3, 17, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 233, new DateTime(2023, 3, 8, 19, 35, 14, 722, DateTimeKind.Local).AddTicks(1103), 24 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BirdProducts_BirdId",
                table: "BirdProducts",
                column: "BirdId");

            migrationBuilder.CreateIndex(
                name: "IX_BirdProducts_ProductId",
                table: "BirdProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductPriceId",
                table: "Discounts",
                column: "ProductPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_Email",
                table: "Logins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserId",
                table: "Logins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_OrderId",
                table: "ProductOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserTelephone",
                table: "Profiles",
                column: "UserTelephone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Raitings_UserId",
                table: "Raitings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirdProducts");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "ProductIngredients");

            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Raitings");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Birds");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
