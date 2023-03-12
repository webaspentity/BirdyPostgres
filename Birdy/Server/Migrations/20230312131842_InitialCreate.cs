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
                    { 1, "admin@email.com", "$2a$11$6//kuIeDaFgQN1/Kqu6LleHY9xntscbZS8tz2DsR4ENAWArT3UpK2", 1 },
                    { 2, "user1@email.com", "$2a$11$N9iNePIsptgaY7e2NnU1ceMdbe0qBvAwg9R4PnS8KvzcF/iwJl3fW", 2 },
                    { 3, "user2@email.com", "$2a$11$68Zf5vVTJigFSvkVowpdtugofxjveQq20kWo2U.WxCxVOt/7yyydq", 3 },
                    { 4, "user3@email.com", "$2a$11$DNs4dBFCIHZnsi3pihZu7ef6bcm4yUtzpVSkWam9Birb/ZedXvyDe", 4 },
                    { 5, "user4@email.com", "$2a$11$DgDv7ds/k.qCx3zsUW0INeLC.NsfMsdIRDrjI4oYHwyBiPT0r2jFS", 5 },
                    { 6, "user5@email.com", "$2a$11$HgWi6Ccl/UChTGyLWY59VetQBtlR0G5wpm7QbhiggunuFznHFrME2", 6 },
                    { 7, "user6@email.com", "$2a$11$ThlNfcRr/1E8SnHrV42eB.AF.EwD9ZCUK1HJrDQhth/yHlZ.EUcwG", 7 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BirdSize", "CategoryId", "Description", "ImageSource", "Manufacturer", "Name", "NutritionType" },
                values: new object[,]
                {
                    { 1, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Malinki", "Переноска для птиц 1", null },
                    { 2, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Vitakraft", "Переноска для птиц 2", null },
                    { 3, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Vitakraft", "Переноска для птиц 3", null },
                    { 4, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 4", null },
                    { 5, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Vitakraft", "Переноска для птиц 5", null },
                    { 6, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 6", null },
                    { 7, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Malinki", "Переноска для птиц 7", null },
                    { 8, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Travellor", "Переноска для птиц 8", null },
                    { 9, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Malinki", "Переноска для птиц 9", null },
                    { 10, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Vitakraft", "Переноска для птиц 10", null },
                    { 11, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Travellor", "Переноска для птиц 11", null },
                    { 12, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 12", null },
                    { 13, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 13", null },
                    { 14, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Malinki", "Переноска для птиц 14", null },
                    { 15, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 15", null },
                    { 16, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Travellor", "Переноска для птиц 16", null },
                    { 17, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Malinki", "Переноска для птиц 17", null },
                    { 18, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Vitakraft", "Переноска для птиц 18", null },
                    { 19, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Vitakraft", "Переноска для птиц 19", null },
                    { 20, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Vitakraft", "Переноска для птиц 20", null },
                    { 21, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №1", "Плотоядные" },
                    { 22, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №2", "Всеядные" },
                    { 23, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/prestige.jpeg", "Prestige", "Корм для маленьких птиц №3", "Растительноядные" },
                    { 24, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №4", "Насекомоядные" },
                    { 25, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №5", "Плотоядные" },
                    { 26, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №6", "Всеядные" },
                    { 27, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №7", "Растительноядные" },
                    { 28, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №8", "Насекомоядные" },
                    { 29, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №9", "Плотоядные" },
                    { 30, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №10", "Всеядные" },
                    { 31, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №11", "Растительноядные" },
                    { 32, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №12", "Насекомоядные" },
                    { 33, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №13", "Плотоядные" },
                    { 34, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №14", "Всеядные" },
                    { 35, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/prestige.jpeg", "Prestige", "Корм для маленьких птиц №15", "Растительноядные" },
                    { 36, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №16", "Насекомоядные" },
                    { 37, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №17", "Плотоядные" },
                    { 38, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/prestige.jpeg", "Prestige", "Корм для маленьких птиц №18", "Всеядные" },
                    { 39, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №19", "Растительноядные" },
                    { 40, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №20", "Насекомоядные" },
                    { 41, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №1", null },
                    { 42, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №2", null },
                    { 43, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №3", null },
                    { 44, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №4", null },
                    { 45, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №5", null },
                    { 46, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №6", null },
                    { 47, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №7", null },
                    { 48, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №8", null },
                    { 49, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №9", null },
                    { 50, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №10", null },
                    { 51, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №11", null },
                    { 52, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №12", null },
                    { 53, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №13", null },
                    { 54, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №14", null },
                    { 55, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №15", null },
                    { 56, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №16", null },
                    { 57, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №17", null },
                    { 58, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №18", null },
                    { 59, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №19", null },
                    { 60, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №20", null },
                    { 61, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №1", null },
                    { 62, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №2", null },
                    { 63, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №3", null },
                    { 64, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №4", null },
                    { 65, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №5", null },
                    { 66, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №6", null },
                    { 67, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №7", null },
                    { 68, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №8", null },
                    { 69, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №9", null },
                    { 70, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №10", null },
                    { 71, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №11", null },
                    { 72, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №12", null },
                    { 73, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №13", null },
                    { 74, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №14", null },
                    { 75, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №15", null },
                    { 76, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №16", null },
                    { 77, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №17", null },
                    { 78, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №18", null },
                    { 79, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №19", null },
                    { 80, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №20", null },
                    { 81, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №1", null },
                    { 82, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №2", null },
                    { 83, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №3", null },
                    { 84, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №4", null },
                    { 85, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №5", null },
                    { 86, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №6", null },
                    { 87, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №7", null },
                    { 88, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №8", null },
                    { 89, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №9", null },
                    { 90, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №10", null },
                    { 91, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №11", null },
                    { 92, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №12", null },
                    { 93, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №13", null },
                    { 94, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №14", null },
                    { 95, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №15", null },
                    { 96, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №16", null },
                    { 97, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №17", null },
                    { 98, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №18", null },
                    { 99, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №19", null },
                    { 100, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №20", null },
                    { 101, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №1", null },
                    { 102, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №2", null },
                    { 103, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №3", null },
                    { 104, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №4", null },
                    { 105, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №5", null },
                    { 106, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №6", null },
                    { 107, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №7", null },
                    { 108, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №8", null },
                    { 109, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №9", null },
                    { 110, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №10", null },
                    { 111, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №11", null },
                    { 112, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №12", null },
                    { 113, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №13", null },
                    { 114, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №14", null },
                    { 115, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №15", null },
                    { 116, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №16", null },
                    { 117, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №17", null },
                    { 118, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №18", null },
                    { 119, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №19", null },
                    { 120, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №20", null },
                    { 121, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №1", null },
                    { 122, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №2", null },
                    { 123, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №3", null },
                    { 124, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №4", null },
                    { 125, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №5", null },
                    { 126, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №6", null },
                    { 127, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №7", null },
                    { 128, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №8", null },
                    { 129, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №9", null },
                    { 130, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №10", null },
                    { 131, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №11", null },
                    { 132, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №12", null },
                    { 133, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №13", null },
                    { 134, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №14", null },
                    { 135, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №15", null },
                    { 136, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №16", null },
                    { 137, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №17", null },
                    { 138, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №18", null },
                    { 139, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №19", null },
                    { 140, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №20", null },
                    { 141, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №1", null },
                    { 142, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №2", null },
                    { 143, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №3", null },
                    { 144, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №4", null },
                    { 145, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №5", null },
                    { 146, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №6", null },
                    { 147, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №7", null },
                    { 148, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №8", null },
                    { 149, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №9", null },
                    { 150, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №10", null },
                    { 151, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №11", null },
                    { 152, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №12", null },
                    { 153, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №13", null },
                    { 154, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №14", null },
                    { 155, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №15", null },
                    { 156, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №16", null },
                    { 157, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №17", null },
                    { 158, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №18", null },
                    { 159, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №19", null },
                    { 160, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №20", null },
                    { 161, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №1", null },
                    { 162, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №2", null },
                    { 163, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №3", null },
                    { 164, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №4", null },
                    { 165, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №5", null },
                    { 166, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №6", null },
                    { 167, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №7", null },
                    { 168, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №8", null },
                    { 169, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №9", null },
                    { 170, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №10", null },
                    { 171, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №11", null },
                    { 172, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №12", null },
                    { 173, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №13", null },
                    { 174, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №14", null },
                    { 175, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №15", null },
                    { 176, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №16", null },
                    { 177, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №17", null },
                    { 178, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №18", null },
                    { 179, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №19", null },
                    { 180, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №20", null }
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
                    { 1, 47, 1145m, 1, 0 },
                    { 2, 0, 1925m, 2, 0 },
                    { 3, 60, 1147m, 3, 0 },
                    { 4, 38, 1032m, 4, 0 },
                    { 5, 31, 779m, 5, 0 },
                    { 6, 98, 781m, 6, 0 },
                    { 7, 34, 1209m, 7, 0 },
                    { 8, 38, 1668m, 8, 0 },
                    { 9, 21, 440m, 9, 0 },
                    { 10, 57, 1009m, 10, 0 },
                    { 11, 52, 399m, 11, 0 },
                    { 12, 75, 375m, 12, 0 },
                    { 13, 77, 1418m, 13, 0 },
                    { 14, 83, 1356m, 14, 0 },
                    { 15, 54, 1927m, 15, 0 },
                    { 16, 82, 1660m, 16, 0 },
                    { 17, 1, 1381m, 17, 0 },
                    { 18, 89, 1890m, 18, 0 },
                    { 19, 20, 1902m, 19, 0 },
                    { 20, 50, 335m, 20, 0 },
                    { 21, 4, 263m, 21, 200 },
                    { 22, 87, 574m, 21, 400 },
                    { 23, 99, 979m, 21, 800 },
                    { 24, 21, 1001m, 21, 1000 },
                    { 25, 49, 372m, 22, 200 },
                    { 26, 64, 571m, 22, 400 },
                    { 27, 33, 916m, 22, 800 },
                    { 28, 96, 1313m, 22, 1000 },
                    { 29, 7, 229m, 23, 200 },
                    { 30, 82, 486m, 23, 400 },
                    { 31, 67, 857m, 23, 800 },
                    { 32, 3, 1381m, 23, 1000 },
                    { 33, 82, 395m, 24, 200 },
                    { 34, 74, 453m, 24, 400 },
                    { 35, 1, 884m, 24, 800 },
                    { 36, 12, 1070m, 24, 1000 },
                    { 37, 48, 381m, 25, 200 },
                    { 38, 77, 573m, 25, 400 },
                    { 39, 78, 823m, 25, 800 },
                    { 40, 92, 1178m, 25, 1000 },
                    { 41, 40, 224m, 26, 200 },
                    { 42, 19, 574m, 26, 400 },
                    { 43, 72, 939m, 26, 800 },
                    { 44, 46, 1347m, 26, 1000 },
                    { 45, 92, 236m, 27, 200 },
                    { 46, 86, 565m, 27, 400 },
                    { 47, 97, 895m, 27, 800 },
                    { 48, 46, 1416m, 27, 1000 },
                    { 49, 10, 339m, 28, 200 },
                    { 50, 74, 423m, 28, 400 },
                    { 51, 67, 835m, 28, 800 },
                    { 52, 8, 1377m, 28, 1000 },
                    { 53, 39, 287m, 29, 200 },
                    { 54, 43, 471m, 29, 400 },
                    { 55, 14, 911m, 29, 800 },
                    { 56, 39, 1377m, 29, 1000 },
                    { 57, 72, 230m, 30, 200 },
                    { 58, 86, 438m, 30, 400 },
                    { 59, 14, 834m, 30, 800 },
                    { 60, 86, 1292m, 30, 1000 },
                    { 61, 29, 330m, 31, 200 },
                    { 62, 80, 564m, 31, 400 },
                    { 63, 47, 924m, 31, 800 },
                    { 64, 42, 1354m, 31, 1000 },
                    { 65, 94, 254m, 32, 200 },
                    { 66, 3, 462m, 32, 400 },
                    { 67, 48, 801m, 32, 800 },
                    { 68, 96, 1143m, 32, 1000 },
                    { 69, 62, 203m, 33, 200 },
                    { 70, 45, 571m, 33, 400 },
                    { 71, 60, 909m, 33, 800 },
                    { 72, 38, 1193m, 33, 1000 },
                    { 73, 56, 259m, 34, 200 },
                    { 74, 67, 535m, 34, 400 },
                    { 75, 28, 948m, 34, 800 },
                    { 76, 96, 1461m, 34, 1000 },
                    { 77, 17, 272m, 35, 200 },
                    { 78, 15, 473m, 35, 400 },
                    { 79, 56, 972m, 35, 800 },
                    { 80, 84, 1457m, 35, 1000 },
                    { 81, 79, 339m, 36, 200 },
                    { 82, 9, 413m, 36, 400 },
                    { 83, 80, 920m, 36, 800 },
                    { 84, 7, 1170m, 36, 1000 },
                    { 85, 37, 225m, 37, 200 },
                    { 86, 50, 572m, 37, 400 },
                    { 87, 66, 973m, 37, 800 },
                    { 88, 50, 1133m, 37, 1000 },
                    { 89, 65, 278m, 38, 200 },
                    { 90, 25, 464m, 38, 400 },
                    { 91, 60, 976m, 38, 800 },
                    { 92, 61, 1275m, 38, 1000 },
                    { 93, 21, 248m, 39, 200 },
                    { 94, 15, 542m, 39, 400 },
                    { 95, 28, 881m, 39, 800 },
                    { 96, 57, 1369m, 39, 1000 },
                    { 97, 0, 305m, 40, 200 },
                    { 98, 36, 439m, 40, 400 },
                    { 99, 19, 982m, 40, 800 },
                    { 100, 52, 1139m, 40, 1000 },
                    { 101, 2, 715m, 41, 0 },
                    { 102, 7, 790m, 42, 0 },
                    { 103, 83, 939m, 43, 0 },
                    { 104, 54, 1213m, 44, 0 },
                    { 105, 65, 1245m, 45, 0 },
                    { 106, 34, 1707m, 46, 0 },
                    { 107, 7, 970m, 47, 0 },
                    { 108, 93, 1043m, 48, 0 },
                    { 109, 94, 1351m, 49, 0 },
                    { 110, 22, 729m, 50, 0 },
                    { 111, 22, 310m, 51, 0 },
                    { 112, 60, 314m, 52, 0 },
                    { 113, 53, 1059m, 53, 0 },
                    { 114, 77, 598m, 54, 0 },
                    { 115, 37, 1239m, 55, 0 },
                    { 116, 54, 553m, 56, 0 },
                    { 117, 50, 791m, 57, 0 },
                    { 118, 57, 1728m, 58, 0 },
                    { 119, 8, 853m, 59, 0 },
                    { 120, 33, 320m, 60, 0 },
                    { 121, 79, 691m, 61, 0 },
                    { 122, 54, 866m, 62, 0 },
                    { 123, 3, 1503m, 63, 0 },
                    { 124, 35, 654m, 64, 0 },
                    { 125, 72, 316m, 65, 0 },
                    { 126, 98, 709m, 66, 0 },
                    { 127, 63, 1221m, 67, 0 },
                    { 128, 76, 631m, 68, 0 },
                    { 129, 89, 662m, 69, 0 },
                    { 130, 0, 1967m, 70, 0 },
                    { 131, 81, 1121m, 71, 0 },
                    { 132, 89, 1391m, 72, 0 },
                    { 133, 28, 1916m, 73, 0 },
                    { 134, 11, 1198m, 74, 0 },
                    { 135, 61, 964m, 75, 0 },
                    { 136, 28, 392m, 76, 0 },
                    { 137, 21, 346m, 77, 0 },
                    { 138, 16, 556m, 78, 0 },
                    { 139, 36, 1644m, 79, 0 },
                    { 140, 51, 1282m, 80, 0 },
                    { 141, 43, 215m, 81, 0 },
                    { 142, 55, 1116m, 82, 0 },
                    { 143, 32, 1802m, 83, 0 },
                    { 144, 5, 1715m, 84, 0 },
                    { 145, 27, 350m, 85, 0 },
                    { 146, 30, 1755m, 86, 0 },
                    { 147, 98, 377m, 87, 0 },
                    { 148, 55, 287m, 88, 0 },
                    { 149, 40, 1452m, 89, 0 },
                    { 150, 65, 222m, 90, 0 },
                    { 151, 47, 1628m, 91, 0 },
                    { 152, 34, 1672m, 92, 0 },
                    { 153, 8, 604m, 93, 0 },
                    { 154, 93, 744m, 94, 0 },
                    { 155, 73, 839m, 95, 0 },
                    { 156, 93, 883m, 96, 0 },
                    { 157, 52, 1445m, 97, 0 },
                    { 158, 86, 975m, 98, 0 },
                    { 159, 71, 425m, 99, 0 },
                    { 160, 94, 1134m, 100, 0 },
                    { 161, 38, 475m, 101, 0 },
                    { 162, 34, 1724m, 102, 0 },
                    { 163, 7, 1024m, 103, 0 },
                    { 164, 85, 330m, 104, 0 },
                    { 165, 97, 460m, 105, 0 },
                    { 166, 67, 1165m, 106, 0 },
                    { 167, 59, 284m, 107, 0 },
                    { 168, 72, 585m, 108, 0 },
                    { 169, 26, 895m, 109, 0 },
                    { 170, 17, 1537m, 110, 0 },
                    { 171, 14, 695m, 111, 0 },
                    { 172, 1, 890m, 112, 0 },
                    { 173, 55, 602m, 113, 0 },
                    { 174, 37, 404m, 114, 0 },
                    { 175, 9, 1401m, 115, 0 },
                    { 176, 67, 1770m, 116, 0 },
                    { 177, 31, 849m, 117, 0 },
                    { 178, 80, 1630m, 118, 0 },
                    { 179, 67, 314m, 119, 0 },
                    { 180, 36, 267m, 120, 0 },
                    { 181, 92, 530m, 121, 0 },
                    { 182, 58, 1990m, 122, 0 },
                    { 183, 14, 308m, 123, 0 },
                    { 184, 86, 791m, 124, 0 },
                    { 185, 14, 1167m, 125, 0 },
                    { 186, 41, 1904m, 126, 0 },
                    { 187, 15, 1780m, 127, 0 },
                    { 188, 35, 429m, 128, 0 },
                    { 189, 14, 737m, 129, 0 },
                    { 190, 51, 1485m, 130, 0 },
                    { 191, 94, 623m, 131, 0 },
                    { 192, 41, 515m, 132, 0 },
                    { 193, 79, 1131m, 133, 0 },
                    { 194, 39, 566m, 134, 0 },
                    { 195, 64, 1164m, 135, 0 },
                    { 196, 6, 939m, 136, 0 },
                    { 197, 80, 1742m, 137, 0 },
                    { 198, 65, 840m, 138, 0 },
                    { 199, 3, 918m, 139, 0 },
                    { 200, 91, 1645m, 140, 0 },
                    { 201, 53, 557m, 141, 0 },
                    { 202, 40, 288m, 142, 0 },
                    { 203, 88, 1832m, 143, 0 },
                    { 204, 25, 953m, 144, 0 },
                    { 205, 93, 1477m, 145, 0 },
                    { 206, 55, 906m, 146, 0 },
                    { 207, 71, 1296m, 147, 0 },
                    { 208, 35, 586m, 148, 0 },
                    { 209, 42, 588m, 149, 0 },
                    { 210, 39, 1231m, 150, 0 },
                    { 211, 99, 260m, 151, 0 },
                    { 212, 33, 1382m, 152, 0 },
                    { 213, 36, 1844m, 153, 0 },
                    { 214, 62, 1056m, 154, 0 },
                    { 215, 31, 258m, 155, 0 },
                    { 216, 9, 318m, 156, 0 },
                    { 217, 6, 307m, 157, 0 },
                    { 218, 60, 1046m, 158, 0 },
                    { 219, 4, 1027m, 159, 0 },
                    { 220, 95, 983m, 160, 0 },
                    { 221, 98, 912m, 161, 0 },
                    { 222, 19, 1091m, 162, 0 },
                    { 223, 15, 498m, 163, 0 },
                    { 224, 18, 688m, 164, 0 },
                    { 225, 10, 1996m, 165, 0 },
                    { 226, 71, 235m, 166, 0 },
                    { 227, 11, 1638m, 167, 0 },
                    { 228, 77, 1854m, 168, 0 },
                    { 229, 41, 979m, 169, 0 },
                    { 230, 0, 305m, 170, 0 },
                    { 231, 48, 590m, 171, 0 },
                    { 232, 53, 1295m, 172, 0 },
                    { 233, 6, 501m, 173, 0 },
                    { 234, 96, 1788m, 174, 0 },
                    { 235, 46, 366m, 175, 0 },
                    { 236, 13, 452m, 176, 0 },
                    { 237, 48, 736m, 177, 0 },
                    { 238, 96, 794m, 178, 0 },
                    { 239, 24, 1724m, 179, 0 },
                    { 240, 48, 1530m, 180, 0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ProductId", "UserId", "Comment", "Id", "ReviewDate" },
                values: new object[,]
                {
                    { 1, 5, "Производителю рекомендовал бы применять более эластичный материал.", 18, new DateTime(2020, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 19, new DateTime(2019, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 4, "Отличное качество, синицы оценили!", 36, new DateTime(2018, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, 5, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 22, new DateTime(2018, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, 5, "Отличное качество, синицы оценили!", 17, new DateTime(2018, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, 6, "Отличное качество, синицы оценили!", 10, new DateTime(2017, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, 2, "Производителю рекомендовал бы применять более эластичный материал.", 37, new DateTime(2017, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 32, new DateTime(2021, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, 3, "Отличное качество, синицы оценили!", 28, new DateTime(2020, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, 2, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 46, new DateTime(2021, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, 3, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 21, new DateTime(2019, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62, 6, "Производителю рекомендовал бы применять более эластичный материал.", 33, new DateTime(2017, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64, 3, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 14, new DateTime(2018, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77, 6, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 16, new DateTime(2019, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80, 7, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 24, new DateTime(2017, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 45, new DateTime(2019, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 89, 5, "Производителю рекомендовал бы применять более эластичный материал.", 8, new DateTime(2021, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 91, 2, "Производителю рекомендовал бы применять более эластичный материал.", 11, new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 49, new DateTime(2020, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 106, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 26, new DateTime(2020, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 107, 3, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 25, new DateTime(2020, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 112, 4, "Производителю рекомендовал бы применять более эластичный материал.", 1, new DateTime(2018, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 114, 2, "Производителю рекомендовал бы применять более эластичный материал.", 6, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 114, 6, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 23, new DateTime(2021, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 115, 3, "Отличное качество, синицы оценили!", 5, new DateTime(2020, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 116, 3, "Отличное качество, синицы оценили!", 20, new DateTime(2018, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 116, 5, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 40, new DateTime(2021, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 117, 4, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 41, new DateTime(2017, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 117, 5, "Производителю рекомендовал бы применять более эластичный материал.", 4, new DateTime(2017, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 119, 2, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 30, new DateTime(2020, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 125, 7, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 38, new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 126, 2, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 42, new DateTime(2017, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 133, 2, "Производителю рекомендовал бы применять более эластичный материал.", 35, new DateTime(2021, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 138, 2, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 15, new DateTime(2021, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 140, 5, "Производителю рекомендовал бы применять более эластичный материал.", 29, new DateTime(2020, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 140, 6, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 31, new DateTime(2017, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 148, 2, "Отличное качество, синицы оценили!", 7, new DateTime(2018, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 149, 5, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 3, new DateTime(2021, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 149, 7, "Производителю рекомендовал бы применять более эластичный материал.", 43, new DateTime(2017, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 155, 3, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 50, new DateTime(2019, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 155, 4, "Отличное качество, синицы оценили!", 47, new DateTime(2017, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 162, 7, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 27, new DateTime(2021, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 165, 2, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 48, new DateTime(2021, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 166, 6, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 2, new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 168, 6, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 34, new DateTime(2017, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 171, 5, "Производителю рекомендовал бы применять более эластичный материал.", 9, new DateTime(2021, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 173, 2, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 44, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 177, 4, "Производителю рекомендовал бы применять более эластичный материал.", 12, new DateTime(2018, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 180, 2, "Отличное качество, синицы оценили!", 13, new DateTime(2017, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 180, 5, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 39, new DateTime(2018, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EndDate", "ProductPriceId", "StartDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 6, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 18, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 18 },
                    { 2, new DateTime(2023, 3, 23, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 24, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 16 },
                    { 3, new DateTime(2023, 4, 5, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 29, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 12 },
                    { 4, new DateTime(2023, 4, 8, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 32, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 14 },
                    { 5, new DateTime(2023, 3, 19, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 36, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 16 },
                    { 6, new DateTime(2023, 4, 6, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 40, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 16 },
                    { 7, new DateTime(2023, 4, 5, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 45, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 13 },
                    { 8, new DateTime(2023, 4, 5, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 46, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 23 },
                    { 9, new DateTime(2023, 3, 31, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 53, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 15 },
                    { 10, new DateTime(2023, 4, 2, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 57, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 17 },
                    { 11, new DateTime(2023, 3, 27, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 65, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 22 },
                    { 12, new DateTime(2023, 4, 8, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 126, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 11 },
                    { 13, new DateTime(2023, 4, 5, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 127, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 14 },
                    { 14, new DateTime(2023, 3, 21, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 128, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 12 },
                    { 15, new DateTime(2023, 3, 30, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 132, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 10 },
                    { 16, new DateTime(2023, 3, 23, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 137, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 18 },
                    { 17, new DateTime(2023, 4, 4, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 149, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 24 },
                    { 18, new DateTime(2023, 3, 27, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 165, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 10 },
                    { 19, new DateTime(2023, 4, 4, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 182, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 16 },
                    { 20, new DateTime(2023, 3, 31, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 189, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 15 },
                    { 21, new DateTime(2023, 4, 8, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 194, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 21 },
                    { 22, new DateTime(2023, 3, 23, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 198, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 21 },
                    { 23, new DateTime(2023, 3, 27, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 225, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 19 },
                    { 24, new DateTime(2023, 3, 19, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 230, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 17 },
                    { 25, new DateTime(2023, 3, 26, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 237, new DateTime(2023, 3, 12, 16, 18, 42, 503, DateTimeKind.Local).AddTicks(5018), 22 }
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
